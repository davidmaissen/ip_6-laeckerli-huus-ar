using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsCanvasClicked : MonoBehaviour, IPointerDownHandler
{
    void Awake() {
        Debug.Log("AWAKE");

    }
    // Update is called once per frame
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("CLICK");
    }
}
