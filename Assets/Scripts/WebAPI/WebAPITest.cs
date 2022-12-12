using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebAPITest {

    public bool isSuccess { get; private set; } = false;
    public string responceJson { get; private set; } = "";

    public IEnumerator WebRequest(
        string accessUrl,
        string method,
        string jsonData,
        string headerName,
        string headerValue
    ) {
        using (UnityWebRequest request = new UnityWebRequest(accessUrl, method)) {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader(headerName, headerValue);

            yield return request.SendWebRequest();

            switch (request.result) {
                case UnityWebRequest.Result.InProgress:
                    Debug.Log("リクエスト中");
                    isSuccess = false;
                    responceJson = "";
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("リクエスト成功");
                    isSuccess = true;
                    responceJson = request.downloadHandler.text;
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("サーバとの通信に失敗");
                    isSuccess = false;
                    responceJson = "";
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("サーバがエラー応答を返した");
                    isSuccess = false;
                    responceJson = "";
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log("データの処理中にエラーが発生");
                    isSuccess = false;
                    responceJson = "";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

}