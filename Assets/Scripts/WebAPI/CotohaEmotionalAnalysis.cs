using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class CotohaEmotionalAnalysis : MonoBehaviour {

    private const string url = "https://api.ce-cotoha.com/api/dev/nlp/v1/sentiment";

    // 文章の感情分析結果をWebAPIに要求するメソッド
    public void RequestEmotionalAnalysis(
        CotohaAccessToken cotohaAccessToken,
        string textToAnalyze
    ) {
        RequestEmotionalAnalysis requestEmotionalAnalysis =
            new RequestEmotionalAnalysis(textToAnalyze);
        string sendJsonData = JsonUtility.ToJson(requestEmotionalAnalysis);

        string accessToken = cotohaAccessToken.responceAccessToken.access_token;
        StartCoroutine(
            WebAPIHandler.WebRequest(
                url,
                UnityWebRequest.kHttpVerbPOST,
                sendJsonData,
                ResponceEmotionalAnalysis,
                new RequestHeader("Content-Type", "application/json;charset=UTF-8"),
                new RequestHeader("Authorization", "Bearer " + accessToken)
            )
        );
    }

    // 要求した感情分析結果のJsonをプログラムで扱えるようにクラスに変換するメソッド
    private void ResponceEmotionalAnalysis(UnityWebRequest request) {
        ResponceEmotionalAnalysis responceEmotionalAnalysis =
            JsonUtility.FromJson<ResponceEmotionalAnalysis>(request.downloadHandler.text);

        EmotionalAnalysisResult emotionalAnalysisResult = responceEmotionalAnalysis.result;
        Debug.Log(emotionalAnalysisResult.sentiment);
        Debug.Log(emotionalAnalysisResult.score);

        foreach (EmotionalAnalysisPhrase item in emotionalAnalysisResult.emotional_phrase) {
            Debug.Log(item.form);
            Debug.Log(item.emotion);
        }
    }

}
