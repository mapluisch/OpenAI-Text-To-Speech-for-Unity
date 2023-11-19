using UnityEngine;

public enum TTSModel 
{
    TTS_1,
    TTS_1_HD,
}
public static class TTSModelExtensions
{
    public static string EnumToString(this TTSModel model)
    {
        switch(model)
        {
            case TTSModel.TTS_1:
                return "tts-1";
            case TTSModel.TTS_1_HD:
                return "tts-1-hd";
            default: 
                Debug.Log(model + " is not a valid TTSModel.");
                return "tts-1";
        }
    }
}