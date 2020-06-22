using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnObjectsOnPlane : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;
    public bool placementModeActive = true;

    [SerializeField]
    private GameObject PlaceablePrefab;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchposition) {
        if (Input.touchCount > 0) {
            touchposition = Input.GetTouch(0).position;
            return true;
        }

        touchposition = default;
        return false;
    }

    void Update()
    {
        if (!placementModeActive || !TryGetTouchPosition(out Vector2 touchposition)) {
            return;
        }

        if (raycastManager.Raycast(touchposition,s_Hits,TrackableType.PlaneWithinPolygon)) {
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
}
