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

    void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        toggleButtonText.text = "Deaktivieren";
    }

    public void TogglePlaneDetection() 
    {
        planeManager.enabled = !planeManager.enabled;
        string toggleButtonMessage = "";

        if (planeManager.enabled) 
        {
            toggleButtonMessage = "Deaktivieren Toggle";
            SetAllPlanesActive(true);
        } 
        else 
        {
            toggleButtonMessage = "Aktivieren Toggle";
            SetAllPlanesActive(false);
        }
        toggleButtonText.text = toggleButtonMessage;
    }

    public void SetAllPlanesActive(bool value) {
        foreach(var plane in planeManager.trackables) {
            plane.gameObject.SetActive(value);
        }
    }
}
