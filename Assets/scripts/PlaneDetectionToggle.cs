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
    // private Text toggleButtonText;
    private SpawnObjectsOnPlane spawnObjectsOnPlane;
    public Button toggleButton;
    private GameObject towerGameCanvas;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        spawnObjectsOnPlane = GameObject.FindObjectOfType<SpawnObjectsOnPlane> ();
        towerGameCanvas = GameObject.FindGameObjectWithTag("GameController");
        towerGameCanvas.SetActive(false);
        spawnObjectsOnPlane.placementModeActive = true;
        // toggleButtonText.text = "Platzierung beenden";
    }

    public void TogglePlaneDetection() 
    {
        Debug.Log("Button hit!");
        planeManager.enabled = !planeManager.enabled;
        // string toggleButtonMessage = "";

        if (planeManager.enabled) 
        {
            spawnObjectsOnPlane.placementModeActive = true;
            // spawnObjectsOnPlane.enabled = false;
            // toggleButtonMessage = "Platzierung bestätigen und beenden";
            towerGameCanvas.SetActive(true);
            SetAllPlanesActive(true);
        } 
        else 
        {
            spawnObjectsOnPlane.placementModeActive = false;
            // spawnObjectsOnPlane.enabled = true;
            // toggleButtonMessage = "Platzierung verändern";
            SetAllPlanesActive(false);
            Debug.Log("Set Toggle Button to false, proceeding....");
            toggleButton.gameObject.SetActive(false);
            Debug.Log("Set Toggle Button to false");
            // toggleButton.gameObject.SetActive(false);
        }
        // toggleButtonText.text = toggleButtonMessage;
    }

    private void SetAllPlanesActive(bool value) {
        foreach(var plane in planeManager.trackables) {
            plane.gameObject.SetActive(value);
        }
    }
}
