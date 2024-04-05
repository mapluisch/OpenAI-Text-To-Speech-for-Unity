using System;
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
    
    public void OnTalkButton()
    {
        if (ttsManager) ttsManager.SynthesizeAndPlay(input.text, (TTSModel) modelDropdown.value, (TTSVoice) voiceDropdown.value, speedSlider.value);
    }

    public void UpdateSpeedLabel(Single value)
    {
        speedLabel.text = value.ToString("0.00");
    }
}
