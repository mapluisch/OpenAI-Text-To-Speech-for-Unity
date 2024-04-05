using UnityEngine;

public class TTSSetup : MonoBehaviour
{
    [SerializeField] private string openAIKey;
    [SerializeField] private string customText;
    // wrapper for TTSSetupEditor script to hook into the inspector.
    // so, no logic to see here :)
}