using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class WebAPIMain : MonoBehaviour {

//     private string apiBaseURL = "https://api.ce-cotoha.com/api/dev/";

//     private bool oneTime = true;

//     private void Update() {
//         if (validAccessToken && oneTime) {
//             oneTime = false;

//             RequestEmotionalAnalysis("私は嬉しい");
//         }
//     }

//     // 文章の感情分析結果をWebAPIに要求するメソッド
//     private void RequestEmotionalAnalysis(string textToAnalyze) {
//         RequestEmotionalAnalysis requestEmotionalAnalysis =
//             new RequestEmotionalAnalysis(textToAnalyze);
//         string sendJsonData = JsonUtility.ToJson(requestEmotionalAnalysis);

//         StartCoroutine(
//             WebAPIHandler.WebRequest(
//                 apiBaseURL + "nlp/v1/sentiment",
//                 UnityWebRequest.kHttpVerbPOST,
//                 sendJsonData,
//                 ResponceEmotionalAnalysis,
//                 new RequestHeader("Content-Type", "application/json;charset=UTF-8"),
//                 new RequestHeader("Authorization", "Bearer " + responceAccessToken.access_token)
//             )
//         );
//     }

//     // 要求した感情分析結果のJsonをプログラムで扱えるようにクラスに変換するメソッド
//     private void ResponceEmotionalAnalysis(UnityWebRequest request) {
//         Debug.Log(request.downloadHandler.text);
//     }

}

public class RequestHeader {

    public string Name { get; private set; }
    public string Value { get; private set; }

    public RequestHeader(string name, string value) {
        Name = name;
        Value = value;
    }

}
