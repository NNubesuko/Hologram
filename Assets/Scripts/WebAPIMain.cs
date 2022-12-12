using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebAPIMain : MonoBehaviour {

    private string accessTokenURL = "https://api.ce-cotoha.com/v1/oauth/accesstokens";
    private string clientId = "3Tu4k9hZoI0m6gHyYkHjJAXpyInTcrxN";
    private string clientSecret = "NoKKCOb72cfOIH9l";
    private string apiBaseURL = "https://api.ce-cotoha.com/api/dev/";
    private string grantType = "client_credentials";

    private WebAPITest webAPITest = new WebAPITest();

    private bool oneTime = true;

    private void Awake() {
        RequestAccessToken accessTokenJson = new RequestAccessToken(
            grantType,
            clientId,
            clientSecret
        );
        string jsonData = JsonUtility.ToJson(accessTokenJson);

        StartCoroutine(
            webAPITest.WebRequest(
                accessTokenURL,
                "POST",
                jsonData,
                "Content-Type",
                "application/json"
            )
        );
    }

    private void Update() {
        if (webAPITest.isSuccess && oneTime) {
            oneTime = false;
            ResponceAccessToken responceAccessToken =
                JsonUtility.FromJson<ResponceAccessToken>(webAPITest.responceJson);
            
            RequestEmotionalAnalysis requestEmotionalAnalysis =
                new RequestEmotionalAnalysis("私は悲しい");
            string jsonData = JsonUtility.ToJson(requestEmotionalAnalysis);
            
            StartCoroutine(
                WebRequest(
                    apiBaseURL + "nlp/v1/sentiment",
                    "POST",
                    jsonData,
                    "Content-Type",
                    "application/json;charset=UTF-8",
                    "Authorization",
                    "Bearer " + responceAccessToken.access_token
                )
            );
        }
    }

    public IEnumerator WebRequest(
        string url,
        string method,
        string jsonData,
        string headerName1,
        string headerValue1,
        string headerName2,
        string headerValue2
    ) {
        using (UnityWebRequest request = new UnityWebRequest(url, method)) {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader(headerName1, headerValue1);
            request.SetRequestHeader(headerName2, headerValue2);

            yield return request.SendWebRequest();

            Debug.Log(request.downloadHandler.text);
        }
    }

}
