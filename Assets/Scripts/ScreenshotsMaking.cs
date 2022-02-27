using UnityEngine;

public class ScreenshotsMaking : MonoBehaviour
{
    private const string FilePath = "C:/Unity/Draw Runner/Screenshots/";
    private int _screenshotNumber = 1;

    public void MakeScreenshot()
    {
        var fileName =  "Screenshot" + _screenshotNumber + ".png";
        ScreenCapture.CaptureScreenshot(FilePath + fileName);
        _screenshotNumber++;
    }
}
