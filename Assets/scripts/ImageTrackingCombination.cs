using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

// Source: https://www.youtube.com/watch?v=I9j3MD7gS5Y

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTrackingCombination : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeablePrefab;
    private Dictionary<string, float> placeablePrefabTimers = new Dictionary<string, float>();

    private float timeUntilDeactivation = 1.0f;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    private bool activated = true;

    private void Awake() {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach(GameObject prefab in placeablePrefab)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(prefab.name, newPrefab);
            placeablePrefabTimers.Add(prefab.name, Time.time);
        }
    }

    void Update()
    {
        foreach(KeyValuePair<string, float> timer in placeablePrefabTimers) {
            if (timer.Value + timeUntilDeactivation < Time.time) {
                spawnedPrefabs[timer.Key].SetActive(false);
            }
        }
    }

    private void OnEnable() {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable() {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs) {
        if (activated) {
            foreach(ARTrackedImage trackedImage in eventArgs.added) {
                UpdateImage(trackedImage);
            }
            foreach(ARTrackedImage trackedImage in eventArgs.updated) {
                if (trackedImage.trackingState == TrackingState.Tracking) UpdateImage(trackedImage);
            }
            foreach(ARTrackedImage trackedImage in eventArgs.removed) {
                spawnedPrefabs[trackedImage.name].SetActive(false);
            }
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage) {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        GameObject prefab = spawnedPrefabs[name];
        placeablePrefabTimers[name] = Time.time;
        prefab.transform.position = position;
        prefab.transform.rotation = trackedImage.transform.rotation;
        // PositionSaveSystem.position = prefab.transform.position;
        // PositionSaveSystem.rotation = prefab.transform.rotation;
        Debug.Log(prefab.name + " spotted");
        prefab.SetActive(true);
    }

    public void ToggleImageTracking() {
        activated = !activated;
        foreach (GameObject prefab in spawnedPrefabs.Values) {
            prefab.SetActive(false);
        }
    }

    /*
    IEnumerator ActivatePrefab(GameObject prefab) {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        prefab.SetActive(true);

        while (watch.Elapsed.TotalSeconds < 1) {
            yield return null;
        }
        prefab.SetActive(false);
        StopCoroutine("ActivatePrefab");

    }
    */
}
