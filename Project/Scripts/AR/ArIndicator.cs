using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(ARRaycastManager))]

public class ArIndicator : Singleton<ArIndicator>
{
    public GameObject placementIndicator;

    private ARRaycastManager raycastManager;
    private Pose placementPose;
    public bool placementPoseIsValid;
    public bool notPlaced;
    public TextMeshProUGUI textMeshProUGUI;

    private ARAnchor anchor;
    private Camera arCamera;

    // Start is called before the first frame update
    void Start()
    {
        arCamera = Camera.main;

        raycastManager = FindObjectOfType<ARRaycastManager>();
        notPlaced = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (placementIndicator == null)
            return;

        if (notPlaced)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }
    }



    private void UpdatePlacementPose()
    {
        var screenCenter = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = arCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, -cameraForward.z);
            placementPose.rotation = Quaternion.LookRotation(cameraBearing).normalized;
        }
        else
            textMeshProUGUI.text = "not detected";


    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            textMeshProUGUI.text = "detected";

        }
        else
        {
            placementIndicator.SetActive(false);
        }

    }

}
