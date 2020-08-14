using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// Based on source: https://www.youtube.com/watch?v=VMjZ70PmnPs&t=313s
// Based on source: https://forum.unity.com/threads/ar-foundation-align-ar-object-pararell-to-floor.699614/

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class SpawnObjectsOnPlane : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    public Animator moveDeviceAnimator;
    public Animator tapToPlaceAnimator;
    public GameObject spawnedObject;
    public bool placementModeActive = true;
    private bool planeHit = false;
    [SerializeField]
    private GameObject PlaceablePrefab;
    private GameObject towerGameCanvas;
    private GameObject arHelpCanvas;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        towerGameCanvas = GameObject.FindGameObjectWithTag("GameController");
        arHelpCanvas = GameObject.Find("ARHelpCanvas");
        planeManager = GetComponent<ARPlaneManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchposition) 
    {
        if (Input.touchCount > 0) 
        {
            touchposition = Input.GetTouch(0).position;
            return true;
        }
        touchposition = default;
        return false;
    }

    private void Update() 
    {
        // show ui animation for ar help based on plane detection state
        if (spawnedObject == null) 
        {
            arHelpCanvas.SetActive(true);
            if (PlanesFound()) 
            {
                moveDeviceAnimator.SetBool("PlanesDetected", true);
                tapToPlaceAnimator.SetBool("PlanesDetected", true);
            } 
            else 
            {
                moveDeviceAnimator.SetBool("PlanesDetected", false);
                tapToPlaceAnimator.SetBool("PlanesDetected", false);
            }
        } 
        else 
        {
            moveDeviceAnimator.enabled = false;
            tapToPlaceAnimator.enabled = false;
            arHelpCanvas.SetActive(false);
        }

        if (!placementModeActive || !TryGetTouchPosition(out Vector2 touchposition)) 
        {
            return;
        }

        // place prefab on detected plane
        if (raycastManager.Raycast(touchposition,hits,TrackableType.PlaneWithinPolygon)) 
        {
            Pose hitPose = hits[0].pose;
            planeHit = true;
            Debug.Log("Event Data touchposition: " + touchposition);
            Debug.Log("Event Data s_Hits: " + hits[0].pose);
            towerGameCanvas.SetActive(true);
            if (spawnedObject == null) 
            {
                // place prefab differently based on mini game
                if (PlaceablePrefab.name.Contains("find-alex-scenery") && PlanesFound()) 
                {
                    Quaternion orientation = Quaternion.identity;
                    Quaternion zUp = Quaternion.identity;
 
                    GetWallPlacement(hits[0], out orientation, out zUp);
            
                    spawnedObject = Instantiate(PlaceablePrefab, hitPose.position, orientation);
                    spawnedObject.transform.rotation = zUp;
                    
                    spawnedObject.gameObject.transform.Rotate(0,180,0);
                    SetAllPlanesActive(false);
                    planeManager.enabled = false;
                    placementModeActive = false;
                } 
                else 
                {
                    var cameraForward = Camera.current.transform.forward;
                    var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                    var placementPoseRotation = hitPose.rotation;
                    placementPoseRotation = Quaternion.LookRotation(cameraBearing);

                    spawnedObject = Instantiate(PlaceablePrefab, hitPose.position, placementPoseRotation);
                    SetAllPlanesActive(false);
                    spawnedObject.gameObject.transform.Rotate(0,-90,0);
                    planeManager.enabled = false;
                    placementModeActive = false;
                }
            }
            else 
            {
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }
            spawnedObject.SetActive(true);
            Debug.Log("Object spawned with name: " + spawnedObject.gameObject.name);
        }
    }

    bool PlanesFound()
    {
        if (planeManager == null)
            return false;

        return planeManager.trackables.count > 0;
    }

    private void SetAllPlanesActive(bool value) 
    {
        foreach(var plane in planeManager.trackables) 
        {
            plane.gameObject.SetActive(value);
        }
    }

    private void GetWallPlacement(ARRaycastHit _planeHit, out Quaternion orientation, out Quaternion zUp)
    {
        TrackableId planeHit_ID = _planeHit.trackableId;
        ARPlane planeHit = planeManager.GetPlane(planeHit_ID);
        Vector3 planeNormal = planeHit.normal;
        orientation = Quaternion.FromToRotation(Vector3.up, planeNormal);
        Vector3 forward = _planeHit.pose.position - (_planeHit.pose.position + Vector3.down);
        zUp = Quaternion.LookRotation(forward, planeNormal);
    }
}
