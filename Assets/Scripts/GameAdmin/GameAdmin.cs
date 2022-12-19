using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAdmin : MonoBehaviour {

    public CotohaAccessToken cotohaAccessToken { get; private set; }
    public CotohaEmotionalAnalysis cotohaEmotionalAnalysis { get; private set; }
    private bool oneTime = true;

    private string sentiment = "";

    private void Awake() {
        cotohaAccessToken = GetComponent<CotohaAccessToken>();
        cotohaEmotionalAnalysis = GetComponent<CotohaEmotionalAnalysis>();
    }

    // その他判定はUpdateで行う
    private void Update() {
        // 感情分析結果を格納するクラスから、感情を取得
        ResponceEmotionalAnalysis responceEmotionalAnalysis =
            cotohaEmotionalAnalysis.responceEmotionalAnalysis;
        if (responceEmotionalAnalysis != null) {
            EmotionalAnalysisResult emotionalAnalysisResult = responceEmotionalAnalysis.result;
            sentiment = emotionalAnalysisResult.sentiment;
        }

        SelectSentiment(sentiment);
    }

    // WebAPIの処理はFixedUpdateで行う
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

    private void SelectSentiment(string sentiment) {
        switch (sentiment) {
            case Sentiment.Positive:
                Debug.Log("ポジティブな処理");
                break;
            case Sentiment.Negative:
                Debug.Log("ネガティブな処理");
                break;
            case Sentiment.Neutral:
                Debug.Log("中立な処理");
                break;
            default:
                Debug.Log("何もしない");
                break;
        }
    }

}

public class Sentiment {

    public const string Positive = "Positive";
    public const string Negative = "Negative";
    public const string Neutral = "Neutral";

}