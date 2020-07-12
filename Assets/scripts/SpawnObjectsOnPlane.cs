using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnObjectsOnPlane : MonoBehaviour, IPointerDownHandler
{
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;
    //private IsCanvasClicked isCanvasClicked;
    public bool placementModeActive = true;
    private bool hit = false;

    [SerializeField]
    private GameObject PlaceablePrefab;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
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
        Debug.Log("Event Data Spawn: " + IsCanvasClicked.goClicked.ToString());
        if (!placementModeActive || !TryGetTouchPosition(out Vector2 touchposition)) {
            return;
        }

        if (raycastManager.Raycast(touchposition,s_Hits,TrackableType.PlaneWithinPolygon) && IsCanvasClicked.goClicked) {
            var hitPose = s_Hits[0].pose;
            Debug.Log("Event Data touchposition: " + touchposition);
            Debug.Log("Event Data s_Hits: " + s_Hits[0].pose);
            if (spawnedObject == null) {
                spawnedObject = Instantiate(PlaceablePrefab, hitPose.position, hitPose.rotation);
            }
            else {
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }
            // Debug.Log("Event Data Spawn: " + isCanvasClicked.goClicked);
            IsCanvasClicked.goClicked = false;
        }
        // hit = false;
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
    */

   public void OnPointerDown(PointerEventData eventData) {
       Debug.Log("Click on GO");
       hit = true;
   }
}
