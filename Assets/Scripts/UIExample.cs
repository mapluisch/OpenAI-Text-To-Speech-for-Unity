using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIExample : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown modelDropdown;
    [SerializeField] private TMP_Dropdown voiceDropdown;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private TextMeshProUGUI speedLabel;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TTSManager ttsManager;
    
    public void OnTalkButton()
    {
        if (ttsManager) ttsManager.SynthesizeAndPlay(input.text, (TTSModel) modelDropdown.value, (TTSVoice) voiceDropdown.value, speedSlider.value);
    }

    public void UpdateSpeedLabel(Single value)
    {
        speedLabel.text = value.ToString("0.00");
    }
}
