using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class CotohaAccessToken : MonoBehaviour {

    public bool validAccessToken { get; private set; } = false;
    public ResponceAccessToken responceAccessToken { get; private set; } = null;

    private const string accessTokenURL = "https://api.ce-cotoha.com/v1/oauth/accesstokens";
    private const string grantType = "client_credentials";
    private const string clientId = "3Tu4k9hZoI0m6gHyYkHjJAXpyInTcrxN";
    private const string clientSecret = "NoKKCOb72cfOIH9l";

    private void Awake() {
        RequestAccessToken();
    }

    // アクセストークンをWebAPIに要求するメソッド
    private void RequestAccessToken() {
        RequestAccessToken accessTokenJson = new RequestAccessToken(
            grantType,
            clientId,
            clientSecret
        );
        string jsonData = JsonUtility.ToJson(accessTokenJson);

        StartCoroutine(
            WebAPIHandler.WebRequest(
                accessTokenURL,
                UnityWebRequest.kHttpVerbPOST,
                jsonData,
                ResponceAccessToken,
                new RequestHeader("Content-Type", "application/json")
            )
        );
    }

    // 要求したアクセストークンのJsonをプログラムで扱えるように、クラスに変換するメソッド
    private void ResponceAccessToken(UnityWebRequest request) {
        validAccessToken = request.result == UnityWebRequest.Result.Success;

        if (validAccessToken) {
            responceAccessToken =
                JsonUtility.FromJson<ResponceAccessToken>(request.downloadHandler.text);
        }
    }

}
