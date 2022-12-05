using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using System.Drawing;

public class CreateTexture {

    public static Texture2D Create(
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

    private static Texture2D BitmapToTexture2D(Bitmap bitmap) {
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

}