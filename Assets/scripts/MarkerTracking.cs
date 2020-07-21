﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;

// Source: https://www.youtube.com/watch?v=I9j3MD7gS5Y

[RequireComponent(typeof(ARTrackedImageManager))]
public class MarkerTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeablePrefab;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    private StarsCountController starsCountController;
    private static bool airPlaneStarCollected = false;
    private static bool oldImagesStarCollected = false;
    private static bool museStarCollected = false;

    private void Awake() {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        starsCountController = FindObjectOfType<StarsCountController>();

        foreach(GameObject prefab in placeablePrefab)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }

    private void OnEnable() {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable() {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs) {
        foreach(ARTrackedImage trackedImage in eventArgs.added) {
            UpdateImage(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.updated) {
            UpdateImage(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.removed) {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage) {
        Debug.Log("Is star collected: " + IsStarCollected(trackedImage.referenceImage.name));
        if (IsStarCollected(trackedImage.referenceImage.name)) return;
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        prefab.transform.rotation = trackedImage.transform.rotation;
        PositionSaveSystem.position = prefab.transform.position;
        PositionSaveSystem.rotation = prefab.transform.rotation;
        prefab.transform.Rotate(90,0,0);
        prefab.SetActive(true);
        Debug.Log(prefab.name + " spotted");

        foreach(GameObject go in spawnedPrefabs.Values) {
            if (go.name != name) {
                go.SetActive(false);
            }
        }
    }

     void Update(){
        foreach(var t in Input.touches) {
            if (t.phase != TouchPhase.Began)
            continue;
            var ray = Camera.main.ScreenPointToRay(t.position);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo)) {
                Debug.Log(PositionSaveSystem.rotation + " -- " + PositionSaveSystem.position);
                Debug.Log(hitInfo.transform.gameObject.name + " clicked");
                /*
                if (hitInfo.transform.gameObject.name == "find-alex") {
                    StartCoroutine(LoadAsyncScene(hitInfo.transform.gameObject.name));
                }
                */
                if (hitInfo.transform.gameObject.name == "airplane-star" ||
                    hitInfo.transform.gameObject.name == "old-images-star" || 
                    hitInfo.transform.gameObject.name == "muse-star") {
                    StarCollected(hitInfo.transform.gameObject.name);
                } else {
                    SceneManager.LoadScene(hitInfo.transform.gameObject.name);
                }
                /*
             if (
                 hitInfo.transform.gameObject.name == "laeckerli-tower" ||
              hitInfo.transform.gameObject.name == "find-alex"
              ) {
             }
             */
            }
        }
    }

    private void StarCollected(string name) {
        if (name == "airplane-star") airPlaneStarCollected = true;
        else if (name == "old-images-star") oldImagesStarCollected = true;
        else if (name == "muse-star") museStarCollected = true;
        Debug.Log(GameProgress.starsCollected);
        GameProgress.starsCollected++;
        starsCountController.updateStarsCounter();
        Debug.Log(GameProgress.starsCollected);
        foreach(GameObject go in spawnedPrefabs.Values) {
            if (go.name == name) {
                go.SetActive(false);
                Debug.Log(go.name + " is active: " + go.activeSelf);
            }
        }
    }
    private bool IsStarCollected(string name) {
        return (name == "airplane-star" && airPlaneStarCollected) || 
        (name == "old-images-star" && oldImagesStarCollected) ||
        (name == "muse-star" && museStarCollected);
    }

    IEnumerator LoadAsyncScene(string name)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log(PositionSaveSystem.rotation + " -- " + PositionSaveSystem.position);
            yield return null;
        }
    }
}
