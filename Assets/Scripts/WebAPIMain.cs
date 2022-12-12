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
            WebRequest(
                accessTokenURL,
                "POST",
                jsonData,
                new RequestHeader("Content-Type", "application/json")
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
                    new RequestHeader("Content-Type", "application/json;charset=UTF-8"),
                    new RequestHeader("Authorization", "Bearer " + responceAccessToken.access_token)
                )
            );
        }
    }

    public IEnumerator WebRequest(
        string url,
        string method,
        string sendJsonData,
        params RequestHeader[] headers
    ) {
        using (UnityWebRequest request = new UnityWebRequest(url, method)) {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(sendJsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            foreach (RequestHeader requestHeader in headers) {
                request.SetRequestHeader(requestHeader.Name, requestHeader.Value);
            }

            yield return request.SendWebRequest();

            Debug.Log(request.downloadHandler.text);
        }
    }

}

public class RequestHeader {

    public string Name { get; private set; }
    public string Value { get; private set; }

    public RequestHeader(string name, string value) {
        Name = name;
        Value = value;
    }

}
