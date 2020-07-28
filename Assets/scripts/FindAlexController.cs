using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.XR.ARFoundation;

public class FindAlexController : MonoBehaviour
{
    private GameObject scenery;
    // private GameObject placedScenery;
    public GameObject arHelpCanvas;
    private SpawnObjectsOnPlane spawnObjectsOnPlane;
    public Material[] materials;
    private float timeUntilHint;
    private bool gameOver = false;
    private bool gameStarted = false;
    private bool touchInfoNeeded = true; 
    private int stars;
    private int gameID = 1;

    //Controller
    private GameProgress gameProgress;
    private GameSuccessController gameSuccessController;
    public GameObject uiController;


    private void Awake() {
        gameSuccessController = uiController.GetComponent<GameSuccessController>();
        gameProgress = new GameProgress();
        // Quaternion rotationForScenery = PositionSaveSystem.rotation;
        spawnObjectsOnPlane = GameObject.FindObjectOfType<SpawnObjectsOnPlane>();
        //placedScenery = GameObject.Find("find-alex-scenery");
        //rotationForScenery.x += 180;
        //rotationForScenery.z += 180;
        // scenery = Instantiate(placedScenery, placedScenery.transform.position, placedScenery.transform.rotation);
        // placedScenery.transform.Rotate(180, 0, 180);
    }

    void Update(){
        if (spawnObjectsOnPlane.placementModeActive || gameOver) return;
        if (!gameStarted) StartCoroutine(StartGame());
        if (touchInfoNeeded) arHelpCanvas.SetActive(true);
        if (timeUntilHint > 0 && touchInfoNeeded && Time.time > timeUntilHint) {
            timeUntilHint += 120.0f;
            // scenery.transform.Find("text-emma").gameObject.SetActive(false);
            // scenery.transform.Find("text-emma-3").gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("emma-3");
            UpdateScenery();
        }
        
        foreach(var t in Input.touches) {
            if (t.phase != TouchPhase.Began)
            continue;
            var ray = Camera.main.ScreenPointToRay(t.position);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo)) {
                Debug.Log(hitInfo.transform.gameObject.name + " clicked");
                InteractWithScenery(hitInfo);
            }
        }
    }

    private void InteractWithScenery(RaycastHit hitInfo) {
        TouchInfoNotNeeded();
        //Destroy(GameObject.FindWithTag("Player"));
        //GameObject.FindWithTag("Player").transform.Find("door-house-1-open").gameObject.SetActive(true);
        //GameObject go = GameObject.FindWithTag("Player");

        if (hitInfo.transform.gameObject.name == "door-house-1") {
            scenery.transform.Find("door-house-1").gameObject.GetComponent<Renderer>().material = materials[0];
            scenery.transform.Find("door-house-1-open").gameObject.SetActive(true);
            scenery.transform.Find("dog").gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("dog-bark");
        } else if (hitInfo.transform.gameObject.name == "bicyclist") {
            scenery.transform.Find("text-bicyclist").gameObject.SetActive(true);
            scenery.transform.Find("star-bicyclist").gameObject.SetActive(true);
            StartCoroutine(DoAfterPlaying("star", "bicyclist"));
        } else if (hitInfo.transform.gameObject.name == "star-bicyclist") {
            scenery.transform.Find("star-bicyclist").gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("star-collected");
            stars++;
        } else if (hitInfo.transform.gameObject.name == "star-dog") {
            scenery.transform.Find("star-dog").gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("star-collected");
            stars++;
        } else if (hitInfo.transform.gameObject.name == "man-window") {
            scenery.transform.Find("text-man-window").gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("man-window");
        } else if (hitInfo.transform.gameObject.name == "daughter-mother") {
            if (!scenery.transform.Find("door-house-1").gameObject.activeSelf) {
                scenery.transform.Find("text-daughter-mother-1").gameObject.SetActive(true);
                scenery.transform.Find("door-house-1").gameObject.SetActive(true);
                FindObjectOfType<AudioManager>().Play("daughter-mother-1");
            } else {
                scenery.transform.Find("text-daughter-mother-1").gameObject.SetActive(false);
                scenery.transform.Find("text-daughter-mother-2").gameObject.SetActive(true);
                scenery.transform.Find("star-dog").gameObject.SetActive(true);
                StartCoroutine(DoAfterPlaying("star", "daughter-mother-2"));
            }
        } else if (hitInfo.transform.gameObject.name == "alex-found") {
            scenery.transform.Find("text-alex").gameObject.SetActive(true);
            scenery.transform.Find("text-emma").gameObject.SetActive(false);
            scenery.transform.Find("text-emma-3").gameObject.SetActive(false);
            scenery.transform.Find("text-emma-2").gameObject.SetActive(true);
            scenery.transform.Find("alex-found").gameObject.GetComponent<Renderer>().material = materials[1];
            StartCoroutine(DoAfterPlaying("alex-found", "emma-2"));
            stars++;
            GameOver(stars);
        } else if (hitInfo.transform.gameObject.name == "emma"){
            if (scenery.transform.Find("text-emma").gameObject.activeSelf) {
                scenery.transform.Find("text-emma").gameObject.SetActive(true);
                scenery.transform.Find("text-emma-3").gameObject.SetActive(false);
                FindObjectOfType<AudioManager>().Play("emma");
            }
        }
        // UpdateScenery();
    }

    private void UpdateScenery() {
        // Destroy(scenery.gameObject);
        // scenery = Instantiate(placedScenery, placedScenery.transform.position, placedScenery.transform.rotation);
        // placedScenery.transform.Rotate(180, 0, 180);
    }
    private void GameOver(int stars) {
        gameOver = true;
        //MiniGame findAlex = new MiniGame(1, "Finde Alex", "Hilf Emma Alex zu finden", stars, stars);
        //gameProgress.SaveMiniGame(findAlex);
        gameProgress.SaveMiniGame(gameID, 0, stars);
        gameSuccessController.showSuccessPanel(gameID, 0, stars);
    }

    private void TouchInfoNotNeeded() {
        arHelpCanvas.SetActive(false);
        touchInfoNeeded = false;
    }

    IEnumerator DoAfterPlaying(string soundSource, string soundSourceNext){
        AudioSource audio = FindObjectOfType<AudioManager>().GetSoundSource(soundSource);
        audio.Play();

        yield return new WaitWhile(() => audio.isPlaying);
        FindObjectOfType<AudioManager>().GetSoundSource(soundSourceNext).Play();
    }

    IEnumerator StartGame(){
        scenery = GameObject.FindWithTag("Player");
        gameStarted = true;
        Debug.Log("Start Coroutine");
        FindObjectOfType<AudioManager>().Play("find-alex-scenery");
        Stopwatch watch = new Stopwatch();
        watch.Start();
        while (watch.Elapsed.TotalSeconds < 2) {
            yield return null;
        }
        Debug.Log("2 secs elapsed.");
        arHelpCanvas.SetActive(true);
        timeUntilHint = Time.time + 30.0f;
        FindObjectOfType<AudioManager>().Play("emma");
        scenery.transform.Find("text-emma").gameObject.SetActive(true);
        UpdateScenery();
        StopCoroutine("StartGame");
    }
}
