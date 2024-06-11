using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuggingFace.API;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class SentenceSimilarity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField] public InputField option1;
    [SerializeField] public InputField option2;
    [SerializeField] public InputField option3;
    [SerializeField] public InputField option4;
    [SerializeField] public InputField inputText;


    public InputField resultText;
    public UnityEngine.UI.Button sendButton;
    List<string> candidates = new List<string>();
    // Make a call to the API
    void Query()
    {
        candidates.Add(option1.text);
        candidates.Add(option2.text);
        candidates.Add(option3.text);
        candidates.Add(option4.text);
        string[] inputs = {
            option1.text,
            option2.text,
            option3.text,
            option4.text
        };
        HuggingFaceAPI.SentenceSimilarity(inputText.text, OnSuccess, OnError, inputs);
    }

    // If successful, handle the result
    void OnSuccess(float[] result)
    {
        int maxIdx = 0;
        int idx = 0;
        float maxVal = 0.0f;
        for (int i = 0; i < result.Length; i++)
        {
            float value = result[i];
            if (value >= maxVal)
            {
                maxVal = value;
                maxIdx = idx;
            }
            idx++;
        }
        resultText.text = candidates[maxIdx];
        Debug.Log(candidates[maxIdx]);
    }

    // Otherwise, handle the error
    void OnError(string error)
    {
        Debug.LogError(error);
    }

    private void Awake()
    {
        option1.text = "escape";
        option2.text = "fight";
        option3.text = "defense";
        option4.text = "idle";
        sendButton.onClick.AddListener(Query);

    }

}
