using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Text;
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

    [SerializeField] private string text;
    [SerializeField] private int size;

    private Material material;

    private float rotateX = 0f;
    private float rotateY = 0f;

    private void Awake() {
        material = GetComponent<Renderer>().material;
    }

    private async void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            Texture2D texture2D = await CreateTest(
                size,
                size,
                Brushes.Transparent,
                30,
                new FontFamily("游明朝"),
                Brushes.Black,
                FormatText(text, 3500)
            );
            // Texture2D texture2D = Create(
            //     size,
            //     size,
            //     Brushes.Transparent,
            //     20,
            //     new FontFamily("游明朝"),
            //     Brushes.Black,
            //     FormatText(text, 3500)
            // );
            texture2D.filterMode = FilterMode.Point;
            texture2D.Apply();
            await Task.Delay(500);
            material.SetTexture("_BaseMap", texture2D);
        }

        rotateX = NormalizeAngle(rotateX + 0.1f * 30 * Time.deltaTime, -180f, 180f);
        rotateY = NormalizeAngle(rotateY + 0.2f * 30 * Time.deltaTime, -180f, 180f);
        transform.rotation = Quaternion.Euler(rotateX, rotateY, 0f);
    }

    private async Task<Texture2D> CreateTest(
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

                await Task.Delay(500);

                Rectangle backgroundRect = new Rectangle(0, 0, width, height);
                graphics.FillRectangle(backgroundColor, backgroundRect);
                graphics.DrawString(drawText, font, fontColor, backgroundRect);

                await Task.Delay(500);

            }

            // ビットマップでは、オブジェクトに割り当てられないため、テスクチャに変換する
            return await BitmapToTexture2D(bitmap);

        }

        throw new Exception("テクスチャを生成できませんでした");
    }

    // public async Texture2D Create(
    //     int width,
    //     int height,
    //     Brush backgroundColor,
    //     int fontSize,
    //     FontFamily fontFamily,
    //     Brush fontColor,
    //     string drawText
    // ) {
    //     using (Bitmap bitmap = new Bitmap(width, height)) {
            
    //         using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
    //         using (System.Drawing.Font font = new System.Drawing.Font(fontFamily, fontSize)) {

    //             Rectangle backgroundRect = new Rectangle(0, 0, width, height);
    //             graphics.FillRectangle(backgroundColor, backgroundRect);
    //             graphics.DrawString(drawText, font, fontColor, backgroundRect);

    //         }

    //         // ビットマップでは、オブジェクトに割り当てられないため、テスクチャに変換する
    //         return BitmapToTexture2D(bitmap);

    //     }

    //     throw new Exception("テクスチャを生成できませんでした");
    // }

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

    private async Task<Texture2D> BitmapToTexture2D(Bitmap bitmap) {
        using (MemoryStream ms = new MemoryStream()) {
            bitmap.Save(ms, ImageFormat.Png);
            await Task.Delay(500);
            byte[] buffer = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(buffer, 0, buffer.Length);
            await Task.Delay(500);
            Texture2D t = new Texture2D(1, 1);
            t.LoadImage(buffer);
            return t;
        }

        throw new Exception("テクスチャに変換できませんでした");
    }








    // 保管用
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
