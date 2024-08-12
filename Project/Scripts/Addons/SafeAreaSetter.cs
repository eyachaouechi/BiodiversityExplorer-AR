using UnityEngine;


public class SafeAreaSetter : MonoBehaviour
{   
    public Canvas canvas;
    RectTransform panelSafeArea;

    Rect currentSafeArea = new Rect();
    ScreenOrientation currentOrientation = ScreenOrientation.AutoRotation;

    // Start is called before the first frame update
    void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();


        //store current values 
        currentOrientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;
      
        ApplySafeArea();

    }

    private void ApplySafeArea()
    {
        if (panelSafeArea == null)
            return;

        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;

        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;


        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax; 

        currentOrientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;
        

    }

    // Update is called once per frame
    void Update()
    {
        if ((currentOrientation != Screen.orientation) || (currentSafeArea != Screen.safeArea)){
            ApplySafeArea();
        } 
    }
}
