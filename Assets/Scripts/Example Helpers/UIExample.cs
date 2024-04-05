using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIExample : MonoBehaviour
{
    [SerializeField] private Dropdown modelDropdown;
    [SerializeField] private Dropdown voiceDropdown;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Text speedLabel;
    [SerializeField] private InputField input;
    [SerializeField] private TTSManager ttsManager;
    [SerializeField] private Image entityImage;
    private Coroutine talkingEffect;
    public void OnTalkButton()
    {
        if (ttsManager)
        {
            ttsManager.SynthesizeAndPlay(input.text, (TTSModel) modelDropdown.value, (TTSVoice) voiceDropdown.value, speedSlider.value);
            talkingEffect ??= StartCoroutine(TalkingEffect());
        }
    }
    
    // hacky little animation to "mimic" a talking effect of the 2d person sprites
    IEnumerator TalkingEffect()
    {
        yield return new WaitForSeconds(1f);
        
        Vector2 initialPosition = entityImage.rectTransform.anchoredPosition;
        Quaternion initialRotation = entityImage.rectTransform.rotation;
    
        float duration = 1.0f;
        float offset = 50f;
        float time = 0;

        Vector2 lastPosition = initialPosition;
        Quaternion lastRotation = initialRotation;

        while (time < duration)
        {
            time += Time.deltaTime;
            
            float targetOffsetX = UnityEngine.Random.Range(-offset, offset); 
            float targetOffsetY = UnityEngine.Random.Range(-offset, offset); 
            float targetRotationZ = UnityEngine.Random.Range(-offset, offset); 

            Vector2 targetPosition = initialPosition + new Vector2(targetOffsetX, targetOffsetY);
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetRotationZ) * initialRotation;

            entityImage.rectTransform.anchoredPosition = Vector2.Lerp(lastPosition, targetPosition, 0.005f);
            entityImage.rectTransform.rotation = Quaternion.Lerp(lastRotation, targetRotation, 0.005f);

            lastPosition = entityImage.rectTransform.anchoredPosition;
            lastRotation = entityImage.rectTransform.rotation;

            yield return null;
        }

        entityImage.rectTransform.anchoredPosition = initialPosition;
        entityImage.rectTransform.rotation = initialRotation;

        talkingEffect = null;
    }

    public void UpdateSpeedLabel(Single value)
    {
        speedLabel.text = value.ToString("0.00");
    }
}
