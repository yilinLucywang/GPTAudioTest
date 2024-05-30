using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ChatManager : MonoBehaviour
{

    private const string YourApiKey = "sk-YdWRd0FMQQK3LabMjQ5l0b6kITuJ5HB99NknElx6TmP8Tbza";
    public Button sendButton;
    public Text responseText;
    public InputField inputField;
    private string textToSend = "";

    private void Awake()
    {
        SpeechToText.Initialize("en-US");

        sendButton.onClick.AddListener(inpuText);
    }


    void inpuText(){
        textToSend = inputField.text;
        var json = "{\"model\": \"gpt-3.5-turbo\",\"messages\": [{\"role\": \"user\",\"content\": \"" + textToSend + "\"}],\"temperature\": 0.7}";
        StartCoroutine(FillAndSend(json));
        inputField.text = "";
    }

    void Start()
    {
    }

    public IEnumerator FillAndSend(string json)
    {
        Debug.Log(json);
        using (var request = new UnityWebRequest("https://api.chatanywhere.tech/v1/chat/completions", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {YourApiKey}");
            //request.SetRequestHeader("Accept", " text/plain");

            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer(); // You can also download directly PNG or other stuff easily. Check here for more information: https://docs.unity3d.com/Manual/UnityWebRequest-CreatingDownloadHandlers.html

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError) // Depending on your Unity version, it will tell that these are obsolote. You can try this: request.result != UnityWebRequest.Result.Success
            {
                Debug.LogError(request.error);
                yield break;
            }
            int lIndex = request.downloadHandler.text.IndexOf("content") + 10; 
            int rIndex = request.downloadHandler.text.IndexOf("\"},\"logprobs\"");
            responseText.text = request.downloadHandler.text.Substring(lIndex, rIndex - lIndex);

            
            Debug.Log(request.downloadHandler.text);
            var response = request.downloadHandler.data; // Or you can directly get the raw binary data, if you need.
            
        }
    }
}
