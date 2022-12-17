using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAdmin : MonoBehaviour {

    public CotohaAccessToken cotohaAccessToken { get; private set; }

    private void Awake() {
        cotohaAccessToken = GetComponent<CotohaAccessToken>();
    }

    private void Update() {
        // アクセストークンの要求
        cotohaAccessToken.RequestAccessToken();
    }

}
