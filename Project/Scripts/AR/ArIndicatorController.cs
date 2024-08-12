using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARAnchorManager))]

public class ArIndicatorController : MonoBehaviour
{
    public GameObject indicator;
    public GameObject objectToPlacePrefab; // The object you want to place
    public GameObject anchorBtn;
    public GameObject GarbagePickerIndecator;


    private GameObject objectPlaced;


    private ARAnchorManager arAnchorManager;
    private ARAnchor anchor;
    private ARPlane arPlane;


    private Camera arCamera;
    private bool isAllowPlaceObject;




    private void Awake()
    {
        arAnchorManager = GetComponent<ARAnchorManager>();
    }


    private void Start()
    {
        arCamera = Camera.main;

        isAllowPlaceObject = false;
    }
    void Update()
    {

        if (objectPlaced)
            return;


        // Cast a ray from the camera's screen position
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        // Check if the ray hits something within the specified distance
        if (Physics.Raycast(ray, out hit))
        {    
            // Check if the hit object is an AR Plane
             arPlane = hit.collider.GetComponent<ARPlane>();
                
            if (arPlane != null)
            {

                // Enable the indicator when pointing at an AR Plane
                indicator.SetActive(true);
                indicator.transform.position = hit.point;

                // Face the camera
                var cameraForward = arCamera.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, -cameraForward.z);
                //indicator.transform.rotation = Quaternion.LookRotation(cameraBearing).normalized;

                // Allow to place Object
                isAllowPlaceObject = true;
            }
            else
            {


                // Disable the indicator when not pointing at an AR Plane
                indicator.SetActive(false);

                isAllowPlaceObject = false;

            }
        }
        else
        {


            // Disable the indicator when not hitting anything
            indicator.SetActive(false);

            isAllowPlaceObject = false;

        }
    }


    public void PlaceObject()
    {

        if (objectPlaced)
            return;


        if (!isAllowPlaceObject)
            return;


        //if (!IsPlaneSuitable(arPlane))
        //{

        //    textMeshProUGUI.text = "NotSuitable";
        //    return;

        //}


        // Place the object at the hit point with the desired rotation
        objectPlaced = Instantiate(objectToPlacePrefab, indicator.transform.position, indicator.transform.rotation);
        indicator.SetActive(false); // Disable the indicator
        anchorBtn.SetActive(false);
        GarbagePickerIndecator.SetActive(true);
        Gaze.UpdateInfos();

        // Make sure the new GameObject has an ARAnchor component
        anchor = objectPlaced.GetComponent<ARAnchor>();
        if (anchor == null)
        {
            anchor = objectPlaced.AddComponent<ARAnchor>();
        }
        Debug.Log($"Created regular anchor (id: {anchor.nativePtr}).");

    }

    bool IsPlaneSuitable(ARPlane plane)
    {
        // Check if the plane meets your criteria, e.g., size and orientation
        return (plane.size.x > ObjectSpaceCalculator(objectToPlacePrefab) && plane.size.y > ObjectSpaceCalculator(objectToPlacePrefab) && plane.alignment == PlaneAlignment.HorizontalUp);
    }


    float ObjectSpaceCalculator(GameObject objectToMeasure)
    {
        // Get the bounds of the object
        Bounds objectBounds = CalculateObjectBounds(objectToMeasure);

        // Calculate and print the width of the object
        float width = objectBounds.size.x;
        Debug.Log("Width of the object: " + width);

        return width;
    }



    Bounds CalculateObjectBounds(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }



    //Vector3[] GetBoundsCorners(Bounds bounds)
    //{
    //    Vector3[] corners = new Vector3[8];

    //    corners[0] = bounds.min;
    //    corners[1] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
    //    corners[2] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
    //    corners[3] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
    //    corners[4] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
    //    corners[5] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
    //    corners[6] = bounds.max;
    //    corners[7] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);

    //    return corners;
    //}




}