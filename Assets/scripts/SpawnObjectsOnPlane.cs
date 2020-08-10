using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class SpawnObjectsOnPlane : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    public Animator moveDeviceAnimator;
    public Animator tapToPlaceAnimator;
    public GameObject spawnedObject;
    //private IsCanvasClicked isCanvasClicked;
    public bool placementModeActive = true;
    private bool planeHit = false;
    // public Animator moveDeviceAnimation;

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
        // animator = GameObject.FindObjectOfType<Animator>();
        // moveDeviceAnimation.SetTrigger("FadeOn");
        //isCanvasClicked = GameObject.FindObjectOfType<IsCanvasClicked>();
    }

    bool TryGetTouchPosition(out Vector2 touchposition) {
        if (Input.touchCount > 0) {
            touchposition = Input.GetTouch(0).position;
            return true;
        }

        touchposition = default;
        return false;
    }

    private void Update() {
        if (spawnedObject == null) {
            arHelpCanvas.SetActive(true);
            if (PlanesFound()) {
                moveDeviceAnimator.SetBool("PlanesDetected", true);
                tapToPlaceAnimator.SetBool("PlanesDetected", true);
                // Debug.Log("PlanesDetected - Animation: Tap To Place");
            } else {
                moveDeviceAnimator.SetBool("PlanesDetected", false);
                tapToPlaceAnimator.SetBool("PlanesDetected", false);
                // Debug.Log("PlanesDetectedFalse - Animation: Move Device");
            }
        } else {
            moveDeviceAnimator.enabled = false;
            tapToPlaceAnimator.enabled = false;
            arHelpCanvas.SetActive(false);
        }

        // Debug.Log("Event Data Spawn: " + IsCanvasClicked.goClicked.ToString());
        if (!placementModeActive || !TryGetTouchPosition(out Vector2 touchposition)) {
            return;
        }

        if (raycastManager.Raycast(touchposition,hits,TrackableType.PlaneWithinPolygon)) {
            Pose hitPose = hits[0].pose;
            planeHit = true;
            Debug.Log("Event Data touchposition: " + touchposition);
            Debug.Log("Event Data s_Hits: " + hits[0].pose);
            towerGameCanvas.SetActive(true);
            if (spawnedObject == null) {
                if (PlaceablePrefab.name.Contains("find-alex-scenery") && PlanesFound()) {
                    Quaternion orientation = Quaternion.identity;
                    Quaternion zUp = Quaternion.identity;
 
                    GetWallPlacement(hits[0], out orientation, out zUp);
            
                    spawnedObject = Instantiate(PlaceablePrefab, hitPose.position, orientation);
                    spawnedObject.transform.rotation = zUp;
                    
                    spawnedObject.gameObject.transform.Rotate(0,180,0);
                    SetAllPlanesActive(false);
                    planeManager.enabled = false;
                    placementModeActive = false;
                } else {
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
            else {
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }
            spawnedObject.SetActive(true);
            Debug.Log("Object spawned with name: " + spawnedObject.gameObject.name);
            // Debug.Log("Event Data Spawn: " + isCanvasClicked.goClicked);
        }
        // hit = false;
    }

    bool PlanesFound()
    {
        if (planeManager == null)
            return false;

        return planeManager.trackables.count > 0;
    }

    private void SetAllPlanesActive(bool value) {
        foreach(var plane in planeManager.trackables) {
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

    /*
    public void UpdateObjectOnPlane(PointerEventData touchposition) {
        if (!placementModeActive) {
            return;
        }
        Debug.Log("Event Data hit position: " + raycastManager.Raycast(touchposition.position,s_Hits,TrackableType.PlaneWithinPolygon));
        Debug.Log("Event Data hit screen position: " + raycastManager.Raycast(touchposition.pointerCurrentRaycast.screenPosition,s_Hits,TrackableType.PlaneWithinPolygon));
        Debug.Log("Event Data hit world position: " + raycastManager.Raycast(touchposition.pointerCurrentRaycast.worldPosition,s_Hits,TrackableType.PlaneWithinPolygon));

        if (raycastManager.Raycast(touchposition.position,s_Hits,TrackableType.PlaneWithinPolygon)) {
            var hitPose = s_Hits[0].pose;
            if (spawnedObject == null) {
                spawnedObject = Instantiate(PlaceablePrefab, hitPose.position, hitPose.rotation);
            }
            else {
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }
        }
    }

   public void OnPointerDown(PointerEventData eventData) {
       Debug.Log("Click on GO");
       hit = true;
   }
    */
}
