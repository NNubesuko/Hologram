using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAdmin : MonoBehaviour {

    public CotohaAccessToken cotohaAccessToken { get; private set; }
    public CotohaEmotionalAnalysis cotohaEmotionalAnalysis { get; private set; }
    private bool oneTime = true;

    private void Awake() {
        cotohaAccessToken = GetComponent<CotohaAccessToken>();
        cotohaEmotionalAnalysis = GetComponent<CotohaEmotionalAnalysis>();
    }

    private void FixedUpdate() {
        // アクセストークンの要求
        cotohaAccessToken.RequestAccessToken();

        if (cotohaAccessToken.validAccessToken && oneTime) {
            oneTime = false;

            cotohaEmotionalAnalysis.RequestEmotionalAnalysis(
                cotohaAccessToken,
                "どちらの絵も上手くない"
            );
        }
    }

}
