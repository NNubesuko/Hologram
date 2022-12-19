using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using System.Drawing;
using KataokaLib.System;

public class AsyncTestSc : MonoBehaviour {

    [SerializeField] private string inputText = "";

    private SpecialMethodUtility methodUtility = new SpecialMethodUtility();

    private bool oneTime = false;

    private float rotateX = 0f;
    private float rotateY = 0f;

    private Texture2D texture2D = null;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) oneTime = true;

        if (oneTime) {
            oneTime = false;
            Test();
        }

        rotateX = normalizeAngle(rotateX + 0.1f * 5, -180f, 180f);
        rotateY = normalizeAngle(rotateY + 0.2f * 5, -180f, 180f);
        transform.rotation = Quaternion.Euler(rotateX, rotateY, 0f);
    }

    private void Test() {
        texture2D = CreateTexture.Create(
            2048,
            2048,
            Brushes.Transparent,
            12,
            new FontFamily("游明朝"),
            Brushes.Black,
            inputText
        );

        texture2D.filterMode = FilterMode.Point;
        texture2D.Apply();
        Debug.Log("End");
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
