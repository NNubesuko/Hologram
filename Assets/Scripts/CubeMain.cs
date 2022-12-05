using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using System.Drawing;

public class CubeMain : MonoBehaviour {

    private Material material;

    private void Awake() {
        Texture2D textTexture = CreateTexture.Create(
            1024,
            1024,
            Brushes.Black,
            50,
            new FontFamily("游明朝"),
            Brushes.White,
            "ここに文章が入ります。ここに文章が入ります。ここに文章が入ります。ここに文章が入ります。"
        );

        material = GetComponent<Renderer>().material;
        material.SetTexture("_BaseMap", textTexture);
    }

}
