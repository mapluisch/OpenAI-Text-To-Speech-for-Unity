using System;
using UnityEngine;

public class TTSManager : MonoBehaviour
{
    private OpenAIWrapper openAIWrapper;
    [SerializeField] private AudioPlayer audioPlayer;
    [SerializeField] private TTSModel model = TTSModel.TTS_1;
    [SerializeField] private TTSVoice voice = TTSVoice.Alloy;
    [SerializeField, Range(0.25f, 4.0f)] private float speed = 1f;
    
    private void OnEnable()
    {
        if (!openAIWrapper) this.openAIWrapper = FindObjectOfType<OpenAIWrapper>();
        if (!audioPlayer) this.audioPlayer = GetComponentInChildren<AudioPlayer>();
    }

    private void OnValidate() => OnEnable();

    public async void SynthesizeAndPlay(string text)
    {
        Debug.Log("Trying to synthesize " + text);
        byte[] audioData = await openAIWrapper.RequestTextToSpeech(text, model, voice, speed);
        if (audioData != null)
        {
            Debug.Log("Playing audio.");
            audioPlayer.ProcessAudioBytes(audioData);
        }
        else Debug.LogError("Failed to get audio data from OpenAI.");
    }

    public void SynthesizeAndPlay(string text, TTSModel model, TTSVoice voice, float speed)
    {
        this.model = model;
        this.voice = voice;
        this.speed = speed;
        SynthesizeAndPlay(text);
    }
}