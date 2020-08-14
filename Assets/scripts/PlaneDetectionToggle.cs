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
    }

    public void TogglePlaneDetection() 
    {
        Debug.Log("Button hit!");
        planeManager.enabled = !planeManager.enabled;

        if (planeManager.enabled) 
        {
            spawnObjectsOnPlane.placementModeActive = true;
            towerGameCanvas.SetActive(true);
            SetAllPlanesActive(true);
        } 
        else 
        {
            spawnObjectsOnPlane.placementModeActive = false;
            SetAllPlanesActive(false);
            toggleButton.gameObject.SetActive(false);
        }
    }

    private void SetAllPlanesActive(bool value) {
        foreach(var plane in planeManager.trackables) {
            plane.gameObject.SetActive(value);
        }
    }
}
