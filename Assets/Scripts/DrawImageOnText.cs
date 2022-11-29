using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using System.Drawing;

public class DrawImageOnText : MonoBehaviour {

    /*
     * https://gist.github.com/grimmdev/e19464a7e67aa4ff53cc
     * https://maywork.net/computer/icatchgen/
     */

    private void Start() {
        // 渡した文字列を描画したテクスチャを作成
        Texture2D textTexture = CreateTextureWithTextDrawing(
            1024,
            1024,
            Brushes.Black,
            50,
            new FontFamily("游明朝"),
            Brushes.White,
            "ああああああああああいいいいいいいいいいうううう"
        );

        Mat textTextureToMat = TextureToMat(textTexture);
        GetComponent<RawImage>().texture = textTexture;
    }

    private Texture2D CreateTextureWithTextDrawing(
        int width,
        int height,
        Brush backgroundColor,
        int fontSize,
        FontFamily fontFamily,
        Brush fontColor,
        string drawText
    ) {

        using (Bitmap bitmap = new Bitmap(width, height)) {
            
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
            using (System.Drawing.Font font = new System.Drawing.Font(fontFamily, fontSize)) {

                Rectangle backgroundRect = new Rectangle(0, 0, width, height);
                graphics.FillRectangle(backgroundColor, backgroundRect);
                graphics.DrawString(drawText, font, fontColor, backgroundRect);

            }

            return BitmapToTexture2D(bitmap);

        }

        throw new Exception("テクスチャを生成できませんでした");
    }

    private Texture2D BitmapToTexture2D(Bitmap bitmap) {
        Texture2D texture2D = new Texture2D(bitmap.Width, bitmap.Height);

        for (int y = 0; y < bitmap.Height; y++) {
            for (int x = 0; x < bitmap.Width; x++) {
                System.Drawing.Color color = bitmap.GetPixel(x, y);
                // bitmap.Width - 1 - x で文字が反転するため、ホログラムで使用できるかの可能性がある
                texture2D.SetPixel(
                    x,
                    bitmap.Height - 1 - y,
                    new Color32(color.R, color.G, color.B, color.A)
                );
            }
        }

        texture2D.Apply();
        return texture2D;
    }

    private Mat TextureToMat(Texture2D texture2D) {
        return OpenCvSharp.Unity.TextureToMat(texture2D);
    }



    // private Texture2D ReadTexture2D(string imageName) {
    //     return (Texture2D)Resources.Load($"Textures/{imageName}") as Texture2D;
    // }

    // private Texture2D MatToTexture(Mat mat) {
    //     return OpenCvSharp.Unity.MatToTexture(mat);
    // }

    // private Texture2D CreateTexture(int width, int height, UnityEngine.Color defaultColor = default) {
    //     Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGB24, false);

    //     for (int y = 0; y < texture2D.height; y++) {
    //         for (int x = 0; x < texture2D.width; x++) {
    //             texture2D.SetPixel(x, y, defaultColor);
    //         }
    //     }

    //     return texture2D;
    // }

}

// Texture2D srcTexture = ReadTexture2D("image");
        // Mat srcMat = TextureToMat(srcTexture);

        // // 書き込む文字列
        // string text = "Hello";
        // // 書き込み位置
        // Point point = new Point(0, 30);
        // // フォントスタイル
        // HersheyFonts fontStyle = HersheyFonts.HersheyPlain;
        // // フォントサイズ
        // int fontScale = 1;
        // // フォントカラー
        // Scalar fontColor = new Scalar(255, 255, 255);
        // // フォントの太さ
        // int fontWeight = 1;
        // // フォントの線の種類
        // LineTypes lineTypes = LineTypes.AntiAlias;

        // // 文字列書き込み
        // srcMat.PutText(
        //     text,
        //     point,
        //     fontStyle,
        //     fontScale,
        //     fontColor,
        //     fontWeight,
        //     lineTypes
        // );

        // Texture2D dstTexture = MatToTexture(srcMat);
        // GetComponent<RawImage>().texture = dstTexture;