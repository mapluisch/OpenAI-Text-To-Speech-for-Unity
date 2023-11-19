using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExample : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField input;
    [SerializeField] private TTSManager ttsManager;
    
    public void OnTalkButton()
    {
        if (ttsManager) ttsManager.SynthesizeAndPlay(input.text);
    }
}
