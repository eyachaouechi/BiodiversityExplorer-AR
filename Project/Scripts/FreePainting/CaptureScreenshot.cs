using System.Collections;
using System.IO;
using UnityEngine;

public class CaptureScreenshot : MonoBehaviour
{
    public Camera captureCamera; // Assign the camera that will capture the object in the Inspector
    public RenderTexture renderTexture; // Assign the Render Texture in the Inspector




    // Capture and save a screenshot
    public void CaptureAndSaveScreenshot(string filePath)
    {
        // Set the camera's target Render Texture
        captureCamera.targetTexture = renderTexture;

        // Render the camera's view into the Render Texture
        captureCamera.Render();

        // Read the Render Texture contents into a Texture2D
        Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height);
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenshot.Apply();
        RenderTexture.active = null;

        // Encode and save the screenshot as a PNG image
        byte[] bytes = screenshot.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Screenshot saved: " + filePath);
    }

    // Example: Call this method to capture and save a screenshot
    public void CaptureAndSaveExample()
    {
        string filePath = Application.dataPath + "/Screenshots/screenshot.png"; // Define the file path
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Ensure the directory exists
        CaptureAndSaveScreenshot(filePath);
    }
}