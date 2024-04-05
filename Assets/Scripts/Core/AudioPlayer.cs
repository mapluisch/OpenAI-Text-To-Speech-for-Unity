using System;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private const bool deleteCachedFile = true;

    private void OnEnable()
    {
        if (!audioSource) this.audioSource = GetComponent<AudioSource>();
    }

    private void OnValidate() => OnEnable();

    public void ProcessAudioBytes(byte[] audioData)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "audio.mp3");
        File.WriteAllBytes(filePath, audioData);

        StartCoroutine(LoadAndPlayAudio(filePath));
    }
    
    private IEnumerator LoadAndPlayAudio(string filePath)
    {
        using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.MPEG);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else Debug.LogError("Audio file loading error: " + www.error);
        
        if (deleteCachedFile) File.Delete(filePath);
    }
}