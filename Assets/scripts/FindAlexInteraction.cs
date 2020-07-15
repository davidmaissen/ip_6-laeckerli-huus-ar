using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAlexInteraction : MonoBehaviour
{
    public GameObject windowYellowHouse;
    public Material windowMaterialYellowHouse;

    private void Awake() {
        
    }

    public void OpenWindow() {
        windowYellowHouse.GetComponent<Renderer>().material = windowMaterialYellowHouse;
        Debug.Log("Opened Window!");
    }
}
