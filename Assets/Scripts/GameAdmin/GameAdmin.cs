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

        // WebAPIに短時間で複数回要求してしまうと、無効なやり取りになってしまうため、一度だけ実行させる
        if (cotohaAccessToken.validAccessToken && oneTime) {
            oneTime = false;

            // 渡された文章の感情分析
            cotohaEmotionalAnalysis.RequestEmotionalAnalysis(
                cotohaAccessToken,
                "どちらの絵も上手くない"
            );
        }
    }

}
