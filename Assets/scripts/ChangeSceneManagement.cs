using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ChangeSceneManagement : MonoBehaviour
{
    // Source: https://www.youtube.com/watch?v=7KR5IKi8m8g

    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    public void ChangeScene(string sceneName) {
        Debug.Log("Change Scene " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
