using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using System.Drawing;

public class DrawImageOnText : MonoBehaviour {

    private void Start() {
        int width = 500;
        int height = 500;
        using (Bitmap bmp = new Bitmap(width, height)) {
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
    }

/*
class Program
{
    <summary>画像にテキストを描画</summary>
    <param name="fileName">出力する画像ファイルのパス</param>
    <param name="drawText">描画するテキスト</param>
    static void drawTextToImageFile(string fileName, string drawText)
    {
        const int fontSize = 90;
        const string fontFamily = "MS UI Gothic";
        const int width = 800;  // 画像の幅
        const int height = 600; // 画像の高さ
        const int margin = 64; // マージン

        using(Bitmap bmp = new Bitmap(width, height))
        {
            using(Graphics g = Graphics.FromImage(bmp))
            using(Font fnt = new Font(fontFamily, fontSize))
            using(Pen bluePen = new Pen(Color.DeepSkyBlue, margin))
            using(Pen grayPen = new Pen(Color.Gainsboro, margin))
            {
                // 背景
                Rectangle bgRect = new Rectangle(0, 0, width, height);
                g.FillRectangle(Brushes.White, bgRect);

                // 枠
                grayPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round; 
                Rectangle bdsRect = new Rectangle(margin+6, margin+6, width-margin*2, height-margin*2);
                g.DrawRectangle(grayPen, bdsRect);
                bluePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round; 
                Rectangle bdRect = new Rectangle(margin, margin, width-margin*2, height-margin*2);
                g.DrawRectangle(bluePen, bdRect);
                g.FillRectangle(Brushes.DeepSkyBlue, bdRect);

                // テキスト
                g.DrawString(drawText, fnt, Brushes.White, bdRect);
            }

            bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
        }

    }
    <summary>利用法</summary>
    static void Usage()
    {
        Console.WriteLine("icathcgen.exe ファイル名 文字列");
    }
    <summary>エントリーポイント</summary>
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Usage();
            Environment.Exit(0);
        }

        string fileName = args[0];
        string drawText = args[1];

        Console.WriteLine("{0} {1}", fileName, drawText);
        drawTextToImageFile(fileName, drawText);
    } // Main end
} // class end
*/


    private Texture2D ReadTexture2D(string imageName) {
        return (Texture2D)Resources.Load($"Textures/{imageName}") as Texture2D;
    }

    private Mat TextureToMat(Texture2D texture2D) {
        return OpenCvSharp.Unity.TextureToMat(texture2D);
    }

    private Texture2D MatToTexture(Mat mat) {
        return OpenCvSharp.Unity.MatToTexture(mat);
    }

    private Texture2D CreateTexture(int width, int height, UnityEngine.Color defaultColor = default) {
        Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGB24, false);

        for (int y = 0; y < texture2D.height; y++) {
            for (int x = 0; x < texture2D.width; x++) {
                texture2D.SetPixel(x, y, defaultColor);
            }
        }

        return texture2D;
    }

}