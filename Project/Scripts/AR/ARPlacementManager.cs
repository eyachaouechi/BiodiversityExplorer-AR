using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARPlaneManager))]
[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARRaycastManager))]
public class ARPlacementManager : Singleton<ARPlacementManager>
{

    [SerializeField]
    private GameObject placedPrefab;

    private ARAnchor anchor;

    private ARAnchorManager arAnchorManager;
    private ARRaycastManager arRaycastManager;


    private GameObject placedObject;



    private void Awake()
    {
        arAnchorManager = GetComponent<ARAnchorManager>();
        arRaycastManager = GetComponent<ARRaycastManager>();

    }

    private bool TryToGetPlacementPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;
                bool isOverUI = touchPosition.IsPointOverUIObject();

                //if overui = true return false otherwise return true 
                return isOverUI ? false : true;

            }

        }

        touchPosition = default;
        return false;
    }


    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    void Update()
    {
        TryPlacePrefabs();

    }

    void TryPlacePrefabs()
    {
        if (!TryToGetPlacementPosition(out Vector2 touchPosition))
            return;

        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var touchPose = hits[0];

            anchor = CreateAnchor(touchPose, placedPrefab);
            
        }
    }

    ARAnchor CreateAnchor(in ARRaycastHit hit, GameObject prefab)
    {
        ARAnchor anchor;

        var instantiatedObject = Instantiate(prefab, hit.pose.position, hit.pose.rotation);

        // Make sure the new GameObject has an ARAnchor component
        anchor = instantiatedObject.GetComponent<ARAnchor>();
        if (anchor == null)
        {
            anchor = instantiatedObject.AddComponent<ARAnchor>();
        }
        Debug.Log($"Created regular anchor (id: {anchor.nativePtr}).");

        return anchor;
    }




}