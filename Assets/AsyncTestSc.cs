using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using KataokaLib.System;

/*
 TODO: 新しい変換方法での非同期処理のテスト
 * 目標は、画面をフリーズさせることなく自然にテクスチャが変更されること
 ! 悪化するようであれば、テスト前の方法で対処する
 */

public class AsyncTestSc : MonoBehaviour {

    [SerializeField] private int size;

    private Material material;

    private void Awake() {
        material = GetComponent<Renderer>().material;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            Texture2D texture2D = Create(
                size,
                size,
                Brushes.Transparent,
                20,
                new FontFamily("游明朝"),
                Brushes.Black,
                "HelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHello"
            );
            material.SetTexture("_BaseMap", texture2D);
        }
    }

    public Texture2D Create(
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

            // ビットマップでは、オブジェクトに割り当てられないため、テスクチャに変換する
            return BitmapToTexture2D(bitmap);

        }

        throw new Exception("テクスチャを生成できませんでした");
    }

    private Texture2D BitmapToTexture2D(Bitmap bitmap) {
        using (MemoryStream ms = new MemoryStream()) {
            bitmap.Save(ms, ImageFormat.Png);
            byte[] buffer = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(buffer, 0, buffer.Length);
            Texture2D t = new Texture2D(1, 1);
            t.LoadImage(buffer);
            return t;
        }

        throw new Exception("テクスチャに変換できませんでした");
    }

    // private static Texture2D BitmapToTexture2D(Bitmap bitmap) {
    //     Texture2D texture2D = new Texture2D(bitmap.Width, bitmap.Height);

    //     for (int y = 0; y < bitmap.Height; y++) {
    //         for (int x = 0; x < bitmap.Width; x++) {
    //             System.Drawing.Color color = bitmap.GetPixel(x, y);
    //             // bitmap.Width - 1 - x で文字が反転するため、ホログラムで使用できるかの可能性がある
    //             texture2D.SetPixel(
    //                 x,
    //                 bitmap.Height - 1 - y,
    //                 new Color32(color.R, color.G, color.B, color.A)
    //             );
    //         }
    //     }

    //     return texture2D;
    // }

}
