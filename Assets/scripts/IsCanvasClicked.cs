using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Based on source https://stackoverflow.com/questions/35529940/how-to-make-gameplay-ignore-clicks-on-ui-button-in-unity3d
public class IsCanvasClicked : MonoBehaviour, IPointerDownHandler
{
    public bool goClicked = false;

    // stop propagation of ui clicks    
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Event Data World Position: " + data.pointerCurrentRaycast.worldPosition);
        Debug.Log("Event Data Screen Position: " + data.pointerCurrentRaycast.screenPosition);
        Debug.Log("Event Data Position: " + data.position);
        Debug.Log("Event Data: " + data);
        goClicked = true;
    }
}
