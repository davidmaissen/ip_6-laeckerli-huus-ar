using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class TapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid) {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        } else {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        var raycastManager = GetComponent<ARRaycastManager>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid) {
            placementPose = hits[0].pose;
        }
    }
}
