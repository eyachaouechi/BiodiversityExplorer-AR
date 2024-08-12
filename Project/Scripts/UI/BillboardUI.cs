using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        // Find the main camera in the scene
        mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        // Ensure the UI element is always facing the camera along the Y-axis
        Vector3 lookAtPosition = transform.position + mainCameraTransform.rotation * Vector3.forward;
        Vector3 upDirection = mainCameraTransform.rotation * Vector3.up;

        transform.LookAt(lookAtPosition, upDirection);
    }
}
