using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsCanvasClicked : MonoBehaviour, IPointerDownHandler
{
    public bool goClicked = false;
    // private SpawnObjectsOnPlane spawnObjectsOnPlane;
    
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Event Data World Position: " + data.pointerCurrentRaycast.worldPosition);
        Debug.Log("Event Data Screen Position: " + data.pointerCurrentRaycast.screenPosition);
        Debug.Log("Event Data Position: " + data.position);
        Debug.Log("Event Data: " + data);
        goClicked = true;
        // spawnObjectsOnPlane.UpdateObjectOnPlane(data);
    }
}
