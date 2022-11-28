using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class Sample : MonoBehaviour {

    private void Start() {
        Texture2D scrTexture = (Texture2D)Resources.Load("Textures/Image") as Texture2D;
        Mat srcMat = OpenCvSharp.Unity.TextureToMat(scrTexture);
        Mat grayMat = new Mat();
        Cv2.CvtColor(srcMat, grayMat, ColorConversionCodes.RGB2GRAY);
        Texture2D dstTexture = OpenCvSharp.Unity.MatToTexture(grayMat);
        GetComponent<RawImage>().texture = dstTexture;
    }

}
