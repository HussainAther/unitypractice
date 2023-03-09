using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShareScript : MonoBehaviour
{
    private static string filePath;
    private static int scoreToShare = -1;

    public static IEnumerator TakeSS(int score)
    {
        yield return new WaitForEndOfFrame();

        scoreToShare = score;
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        filePath = Path.Combine(Application.temporaryCachePath, "shared_img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());
        Destroy(ss);
    }

    public static void ShareSS()
    {
        Debug.Log("Sharing your score..");
        if(filePath != null && scoreToShare != -1)
            new NativeShare().AddFile(filePath).SetSubject("Flappy Bird").SetText("I cant believe I scored " + scoreToShare).Share();
    }
}
