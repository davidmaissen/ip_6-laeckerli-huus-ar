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
    private GameObject arHelpCanvas;
    public Material[] materials;
    private ARTrackedImage lastTrackedImage;

    // private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    private float timeUntilHint;
    private bool gameStarted = false;
    private bool gameOver = false;

    private void Awake() {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        lastTrackedImage = new ARTrackedImage();
        arHelpCanvas = GameObject.Find("ARHelpCanvas");
        arHelpCanvas.SetActive(false);
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
        if (!gameStarted) {
            arHelpCanvas.SetActive(true);
            timeUntilHint = Time.time + 15.0f;
            gameStarted = true;
        }
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;
        Quaternion rotation = trackedImage.transform.rotation;

        if (spawnedPrefab) {
            return;
        } else {
            // First time the image is tracked, so play audio of emma
            FindObjectOfType<AudioManager>().Play("emma");
            lastTrackedImage = trackedImage;
        }

        // Have to reinstantiate because else the image would move weirdly
        spawnedPrefab = Instantiate(placeablePrefab, position, rotation);
        if (name == "find-alex-scenery") {
            spawnedPrefab.transform.Rotate(180,0,180);
        } else if (name == "find-alex-scenery-1") {
            Debug.Log("Blue house spotted");
            spawnedPrefab.transform.Rotate(180,0,270);
            spawnedPrefab.transform.position += new Vector3(-0.6f, 0, 0);
        } else if (name == "find-alex-scenery-2") {
            spawnedPrefab.transform.Rotate(180,0,270);
            spawnedPrefab.transform.position += new Vector3(-4.6f, 0, 0);
        }
        spawnedPrefab.gameObject.SetActive(true);

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
        if (gameStarted && Time.time > timeUntilHint && !gameOver) {
            timeUntilHint += 120.0f;
            placeablePrefab.transform.Find("text-emma").gameObject.SetActive(false);
            placeablePrefab.transform.Find("text-emma-3").gameObject.SetActive(true);
            Destroy(spawnedPrefab.gameObject);
            spawnedPrefab = Instantiate(placeablePrefab, spawnedPrefab.transform.position, spawnedPrefab.transform.rotation);
            UpdateImage(lastTrackedImage);
        }
        

        foreach(var t in Input.touches) {
            if (t.phase != TouchPhase.Began)
            continue;
            var ray = Camera.main.ScreenPointToRay(t.position);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo)) {
                Debug.Log(hitInfo.transform.gameObject.name + " clicked");
                if (hitInfo.transform.gameObject.name == "door-house-1") {
                    placeablePrefab.transform.Find("door-house-1").gameObject.GetComponent<Renderer>().material = materials[0];
                    placeablePrefab.transform.Find("door-house-1-open").gameObject.SetActive(true);
                    placeablePrefab.transform.Find("dog").gameObject.SetActive(true);
                    placeablePrefab.transform.Find("text-daughter-mother-1").gameObject.SetActive(false);
                    placeablePrefab.transform.Find("text-daughter-mother-2").gameObject.SetActive(true);
                    placeablePrefab.transform.Find("star-dog").gameObject.SetActive(true);
                    arHelpCanvas.SetActive(false);
                } else if (hitInfo.transform.gameObject.name == "bicyclist") {
                    placeablePrefab.transform.Find("text-bicyclist").gameObject.SetActive(true);
                    placeablePrefab.transform.Find("star-bicyclist").gameObject.SetActive(true);
                    arHelpCanvas.SetActive(false);
                } else if (hitInfo.transform.gameObject.name == "star-bicyclist") {
                    placeablePrefab.transform.Find("star-bicyclist").gameObject.SetActive(false);
                    arHelpCanvas.SetActive(false);
                } else if (hitInfo.transform.gameObject.name == "star-dog") {
                    placeablePrefab.transform.Find("star-dog").gameObject.SetActive(false);
                    arHelpCanvas.SetActive(false);
                } else if (hitInfo.transform.gameObject.name == "man-window") {
                    placeablePrefab.transform.Find("text-man-window").gameObject.SetActive(true);
                    arHelpCanvas.SetActive(false);
                } else if (hitInfo.transform.gameObject.name == "daughter-mother") {
                    if (!placeablePrefab.transform.Find("door-house-1").gameObject.activeSelf) {
                        placeablePrefab.transform.Find("text-daughter-mother-1").gameObject.SetActive(true);
                        placeablePrefab.transform.Find("door-house-1").gameObject.SetActive(true);
                        arHelpCanvas.SetActive(false);
                    }
                } else if (hitInfo.transform.gameObject.name == "alex-found") {
                    placeablePrefab.transform.Find("text-alex").gameObject.SetActive(true);
                    placeablePrefab.transform.Find("text-emma").gameObject.SetActive(false);
                    placeablePrefab.transform.Find("text-emma-3").gameObject.SetActive(false);
                    placeablePrefab.transform.Find("text-emma-2").gameObject.SetActive(true);
                    placeablePrefab.transform.Find("alex-found").gameObject.GetComponent<Renderer>().material = materials[1];
                    arHelpCanvas.SetActive(false);
                    gameOver = true;
                } else if (hitInfo.transform.gameObject.name == "emma"){
                    if (placeablePrefab.transform.Find("text-emma").gameObject.activeSelf) {
                        placeablePrefab.transform.Find("text-emma").gameObject.SetActive(false);
                        placeablePrefab.transform.Find("text-emma-3").gameObject.SetActive(true);
                        arHelpCanvas.SetActive(false);
                   }
                }
                Destroy(spawnedPrefab.gameObject);
                spawnedPrefab = Instantiate(placeablePrefab, spawnedPrefab.transform.position, spawnedPrefab.transform.rotation);
                //spawnedPrefab.transform.Rotate(180,0,180);
                UpdateImage(lastTrackedImage);
            }
        }
    }
}
