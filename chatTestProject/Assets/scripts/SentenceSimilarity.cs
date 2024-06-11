using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuggingFace.API;
using UnityEngine.UI;
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

    [SerializeField] public string option1;
    [SerializeField] public string option2;
    [SerializeField] public string option3;
    [SerializeField] public string option4;
    [SerializeField] public string inputText;


    List<string> candidates = new List<string>();
    // Make a call to the API
    void Query(string inputText)
    {
        candidates.Add(option1);
        candidates.Add(option2);
        candidates.Add(option3);
        candidates.Add(option4);
        string[] inputs = {
            option1,
            option2,
            option3,
            option4
        };
        HuggingFaceAPI.SentenceSimilarity(inputText, OnSuccess, OnError, inputs);
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
        Debug.Log(candidates[maxIdx]);
    }

    // Otherwise, handle the error
    void OnError(string error)
    {
        Debug.LogError(error);
    }

    private void Awake()
    {
        Query(inputText);

    }

}
