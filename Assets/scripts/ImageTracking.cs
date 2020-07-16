using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

// Source: https://www.youtube.com/watch?v=I9j3MD7gS5Y

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject placeablePrefab;
    private GameObject spawnedPrefab;
    public Material doorMaterialYellowHouse;

    // private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private void Awake() {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        /*
        foreach(GameObject prefab in placeablePrefab)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
        */
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
            spawnedPrefab.SetActive(false);
        }
        
    }

    private void UpdateImage(ARTrackedImage trackedImage) {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;
        Quaternion rotation = trackedImage.transform.rotation;
        // rotation.x = rotation.x + 90;
        // rotation.z = rotation.z - 180;

        // GameObject prefab = spawnedPrefabs[name];
        if (spawnedPrefab) {
            Destroy(spawnedPrefab.gameObject);
        } else {
            // First time the image is tracked, so play audio of emma
            FindObjectOfType<AudioManager>().Play("emma");
        }

        spawnedPrefab = Instantiate(placeablePrefab, position, rotation);
        spawnedPrefab.transform.Rotate(180,0,180);
        spawnedPrefab.gameObject.SetActive(true);
        /*
        prefab.transform.position = position;
        prefab.transform.rotation = trackedImage.transform.rotation;
        prefab.transform.Rotate(90,0,0);
        placeablePrefab.transform = position;
        prefab.SetActive(true);
        */
        Debug.Log(spawnedPrefab.name + " spotted. Position: " + trackedImage.transform.position);

        /*
        if (spawnedPrefab.name != name) {
            spawnedPrefab.SetActive(false);
        }
        foreach(GameObject go in spawnedPrefabs.Values) {
            if (go.name != name) {
                go.SetActive(false);
            }
        }
        */
    }

    void Update(){
        foreach(var t in Input.touches) {
            if (t.phase != TouchPhase.Began)
            continue;
            var ray = Camera.main.ScreenPointToRay(t.position);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo)) {
                Debug.Log(hitInfo.transform.gameObject.name + " clicked");
                if (hitInfo.transform.gameObject.name == "door-house-1") {
                    // GameObject windowOpened = placeablePrefab.transform.GetChild(2).gameObject;
                    // windowOpened = GameObject.Find(placeablePrefab.gameObject.name + "/window");
                    placeablePrefab.transform.GetChild(2).gameObject.GetComponent<Renderer>().material = doorMaterialYellowHouse;
                    placeablePrefab.transform.GetChild(3).gameObject.SetActive(true);
                    //windowOpened.GetComponent<Renderer>().material = windowMaterialYellowHouse;
                    Debug.Log("Changed to " + placeablePrefab.transform.GetChild(2).gameObject.GetComponent<Renderer>().material);
                }
            }
        }
    }

    /*
    void Update(){
        foreach(var t in Input.touches) {
            if (t.phase != TouchPhase.Began)
            continue;
            var ray = Camera.main.ScreenPointToRay(t.position);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo)) {
                if (hitInfo.transform.gameObject.name == "window") {
                    foreach (GameObject child in windowYellowHouse.gameObject.GetComponentsInChildren<GameObject>()) {
                        Debug.Log("Child of windowYellowHouse: " + child.gameObject.name);
                    }
                    windowYellowHouse.gameObject.GetComponent<Renderer>().material = windowMaterialYellowHouse;
                }
                Debug.Log(hitInfo.transform.gameObject.name + " clicked: " + windowYellowHouse.GetComponent<Renderer>().material);
            }
        }
    }
    */
}
