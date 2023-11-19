using System.Reflection;
using System.ComponentModel;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class OpenAIWrapper : MonoBehaviour
{
    [SerializeField] private string openAIKey = "api-key";
    private TTSModel model = TTSModel.TTS_1;
    private TTSVoice voice = TTSVoice.Alloy;
    private float speed = 1f;
    private string outputFormat = "mp3";

    public async Task<byte[]> RequestTextToSpeech(string text)
    {
        Debug.Log("Sending new request to OpenAI TTS.");
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", openAIKey);

            var payload = new
            {
                model = this.model.EnumToString(),
                input = text,
                voice = this.voice.ToString().ToLower(),
                response_format = this.outputFormat,
                speed = this.speed
            };

            string jsonPayload = JsonConvert.SerializeObject(payload);

            var httpResponse = await httpClient.PostAsync(
                "https://api.openai.com/v1/audio/speech", 
                new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

            byte[] jsonResponse = await httpResponse.Content.ReadAsByteArrayAsync();

            if (httpResponse.IsSuccessStatusCode)
            {
                return jsonResponse;
            }
            Debug.Log("Error: " + httpResponse.StatusCode.ToString());
            return null;
        }
    }
    
    
    public async Task<byte[]> RequestTextToSpeech(string text, TTSModel model, TTSVoice voice, float speed)
    {
        this.model = model;
        this.voice = voice;
        this.speed = speed;
        return await RequestTextToSpeech(text);
    }
    
}