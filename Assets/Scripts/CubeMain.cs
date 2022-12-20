using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KataokaLib.System;

public class CubeMain : MonoBehaviour {

    [SerializeField] private GameAdmin gameAdmin;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float magnification;

    private Material material;
    private SpecialMethodUtility methodUtility = new SpecialMethodUtility();

    private float rotateX = 0f;
    private float rotateY = 0f;

    private void Awake() {
        material = GetComponent<Renderer>().material;
    }

    private void Update() {
        // ゲーム開始時に一度だけ実行するメソッド
        // その後は、渡した条件式を通れば一度だけ実行される
        // todo: マイク入力完了に変更
        methodUtility.OneTimeMethod(
            Attach,
            Input.GetKeyDown(KeyCode.Return)
        );

        rotateX = NormalizeAngle(rotateX + 0.1f * magnification * Time.deltaTime, -180f, 180f);
        rotateY = NormalizeAngle(rotateY + 0.2f * magnification * Time.deltaTime, -180f, 180f);
        transform.rotation = Quaternion.Euler(rotateX, rotateY, 0f);
    }

    /*
     * 生成したテクスチャをオブジェクトに付けるメソッド
     */
    private void Attach() {
        Texture2D attachTexture = CreateTexture2D();

        // テスクチャを割り当てる前にフィルターモードをポイントに変更（文字の可読性を上げるため）
        attachTexture.filterMode = FilterMode.Point;
        attachTexture.Apply();

        material.SetTexture("_BaseMap", attachTexture);
    }

    /*
     * 渡された文字を描画したテクスチャを生成するメソッド
     */
    private Texture2D CreateTexture2D() {
        return CreateTexture.Create(
            gameAdmin.textureSize,
            gameAdmin.textureSize,
            Brushes.Transparent,
            gameAdmin.fontSize,
            new FontFamily("游明朝"),
            Brushes.Black,
            FormatText(gameAdmin.inputText, gameAdmin.textLength)
        );
    }

    /*
     * 文字列をテクスチャ用にフォーマットするメソッド
     */
    private string FormatText(string text, int length) {
        StringBuilder sb = new StringBuilder();

        while (sb.Length <= length) {
            sb.Append(text);
        }

        return sb.ToString();
    }

    /*
     * 角度を正規化するメソッド
     */
    private float NormalizeAngle(float x, float min, float max) {
        float cycle = max - min;
        x = (x - min) % cycle + min;
        if (x < min)
            x += cycle;
        return x;
    }

}
