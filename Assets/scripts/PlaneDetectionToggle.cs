using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager))]
public class PlaneDetectionToggle : MonoBehaviour
{
    private ARPlaneManager planeManager;
    [SerializeField]
    private Text toggleButtonText;
    private SpawnObjectsOnPlane spawnObjectsOnPlane;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        spawnObjectsOnPlane = GameObject.FindObjectOfType<SpawnObjectsOnPlane> ();
        spawnObjectsOnPlane.placementModeActive = true;
        toggleButtonText.text = "Platzierung beenden";
    }

    public void TogglePlaneDetection() 
    {
        Debug.Log("Button hit!");
        planeManager.enabled = !planeManager.enabled;
        string toggleButtonMessage = "";

        if (planeManager.enabled) 
        {
            spawnObjectsOnPlane.placementModeActive = true;
            // spawnObjectsOnPlane.enabled = false;
            toggleButtonMessage = "Platzierung bestätigen und beenden";
            SetAllPlanesActive(true);
        } 
        else 
        {
            spawnObjectsOnPlane.placementModeActive = false;
            // spawnObjectsOnPlane.enabled = true;
            toggleButtonMessage = "Platzierung verändern";
            SetAllPlanesActive(false);
        }
        toggleButtonText.text = toggleButtonMessage;
    }

    private void SetAllPlanesActive(bool value) {
        foreach(var plane in planeManager.trackables) {
            plane.gameObject.SetActive(value);
        }
    }
}
