using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using System.Drawing;

public class CubeMain : MonoBehaviour {

    [SerializeField] private RawImage rawImage;
    [SerializeField] private int textureSize;
    [SerializeField] private int fontSize;
    [SerializeField, TextArea] private string text;
    [SerializeField] private float magnification;
    private Material material;

    private float rotateX = 0f;
    private float rotateY = 0f;

    private void Awake() {
        Attach();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            Attach();
        }

        rotateX = normalizeAngle(rotateX + 0.1f * magnification, -180f, 180f);
        rotateY = normalizeAngle(rotateY + 0.2f * magnification, -180f, 180f);
        transform.rotation = Quaternion.Euler(rotateX, rotateY, 0f);
    }

    /*
     * 生成したテクスチャをオブジェクトに付けるメソッド
     */
    private void Attach() {
        Texture2D textTexture = CreateTexture.Create(
            textureSize,
            textureSize,
            Brushes.Transparent,
            fontSize,
            new FontFamily("游明朝"),
            Brushes.Black,
            text
        );

        material = GetComponent<Renderer>().material;
        material.SetTexture("_BaseMap", textTexture);

        if (!rawImage) return;
        rawImage.texture = textTexture;
    }

    /*
     * 角度を正規化するメソッド
     */
    private float normalizeAngle(float x, float min, float max) {
        float cycle = max - min;
        x = (x - min) % cycle + min;
        if (x < min)
            x += cycle;
        return x;
    }

}
