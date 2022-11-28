using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class DrawImageOnText : MonoBehaviour {

    private void Start() {
        Texture2D srcTexture = ReadTexture2D("image");
        Mat srcMat = TextureToMat(srcTexture);

        // 書き込む文字列
        string text = "Hello";
        // 書き込み位置
        Point point = new Point(0, 50);
        // フォントスタイル
        HersheyFonts fontStyle = HersheyFonts.HersheyPlain;
        // フォントサイズ
        int fontScale = 2;
        // フォントカラー
        Scalar fontColor = new Scalar(255, 255, 255);
        // フォントの太さ
        int fontWeight = 1;
        // フォントの線の種類
        LineTypes lineTypes = LineTypes.AntiAlias;


        srcMat.PutText(
            text,
            point,
            fontStyle,
            fontScale,
            fontColor,
            fontWeight,
            lineTypes
        );


        Texture2D dstTexture = MatToTexture(srcMat);
        GetComponent<RawImage>().texture = dstTexture;
    }

    private Texture2D ReadTexture2D(string imageName) {
        return (Texture2D)Resources.Load($"Textures/{imageName}") as Texture2D;
    }

    private Mat TextureToMat(Texture2D texture2D) {
        return OpenCvSharp.Unity.TextureToMat(texture2D);
    }

    private Texture2D MatToTexture(Mat mat) {
        return OpenCvSharp.Unity.MatToTexture(mat);
    }

}
