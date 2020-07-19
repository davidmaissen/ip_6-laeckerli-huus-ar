using System.Collections;
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

    private void Awake() {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

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
                SceneManager.LoadScene(hitInfo.transform.gameObject.name);
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
}
