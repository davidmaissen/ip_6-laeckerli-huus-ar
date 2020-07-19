using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.XR.ARFoundation;

public class FindAlexController : MonoBehaviour
{
    public GameObject scenery;
    private GameObject placedScenery;
    public GameObject arHelpCanvas;
    public Material[] materials;
    private float timeUntilHint;
    private bool gameOver = false;
    private bool touchInfoNeeded = true;
    private GameProgress gameProgress;
    private int stars;

    private void Awake() {
        gameProgress = new GameProgress();
        Quaternion rotationForScenery = PositionSaveSystem.rotation;
        StartCoroutine(StartGame());
        arHelpCanvas.SetActive(true);
        //rotationForScenery.x += 180;
        //rotationForScenery.z += 180;
        placedScenery = Instantiate(scenery, PositionSaveSystem.position, rotationForScenery);
        placedScenery.transform.Rotate(180, 0, 180);
    }

    void Update(){
        if (gameOver) return;
        if (touchInfoNeeded && Time.time > timeUntilHint) {
            timeUntilHint += 120.0f;
            scenery.transform.Find("text-emma").gameObject.SetActive(false);
            scenery.transform.Find("text-emma-3").gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("emma-3");
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
        if (hitInfo.transform.gameObject.name == "door-house-1") {
            scenery.transform.Find("door-house-1").gameObject.GetComponent<Renderer>().material = materials[0];
            scenery.transform.Find("door-house-1-open").gameObject.SetActive(true);
            scenery.transform.Find("dog").gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("dog-bark");
        } else if (hitInfo.transform.gameObject.name == "bicyclist") {
            scenery.transform.Find("text-bicyclist").gameObject.SetActive(true);
            scenery.transform.Find("star-bicyclist").gameObject.SetActive(true);
            StartCoroutine(DoAfterPlaying("star", "bicyclist"));
            // FindObjectOfType<AudioManager>().Play("star");
        } else if (hitInfo.transform.gameObject.name == "star-bicyclist") {
            scenery.transform.Find("star-bicyclist").gameObject.SetActive(false);
            stars++;
        } else if (hitInfo.transform.gameObject.name == "star-dog") {
            scenery.transform.Find("star-dog").gameObject.SetActive(false);
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
                // FindObjectOfType<AudioManager>().Play("star");
            }
        } else if (hitInfo.transform.gameObject.name == "alex-found") {
            scenery.transform.Find("text-alex").gameObject.SetActive(true);
            scenery.transform.Find("text-emma").gameObject.SetActive(false);
            scenery.transform.Find("text-emma-3").gameObject.SetActive(false);
            scenery.transform.Find("text-emma-2").gameObject.SetActive(true);
            scenery.transform.Find("alex-found").gameObject.GetComponent<Renderer>().material = materials[1];
            StartCoroutine(DoAfterPlaying("alex-found", "emma-2"));
            // FindObjectOfType<AudioManager>().Play("alex-found");
            stars++;
            GameOver(stars);
        } else if (hitInfo.transform.gameObject.name == "emma"){
            if (scenery.transform.Find("text-emma").gameObject.activeSelf) {
                scenery.transform.Find("text-emma").gameObject.SetActive(true);
                scenery.transform.Find("text-emma-3").gameObject.SetActive(false);
                FindObjectOfType<AudioManager>().Play("emma");
            }
        }
        UpdateScenery();    
    }

    private void UpdateScenery() {
        Destroy(placedScenery.gameObject);
        placedScenery = Instantiate(scenery, PositionSaveSystem.position, PositionSaveSystem.rotation);
        placedScenery.transform.Rotate(180, 0, 180);
        if (touchInfoNeeded) {
            TouchInfoNotNeeded();
        }
    }
    private void GameOver(int stars) {
        gameOver = true;
        MiniGame findAlex = new MiniGame(1, "Finde Alex", "Hilf Emma Alex zu finden", stars, stars);
        gameProgress.SaveMiniGame(findAlex);
    }

    private void TouchInfoNotNeeded() {
        arHelpCanvas.SetActive(false);
        touchInfoNeeded = false;
    }

    IEnumerator DoAfterPlaying(string soundSource, string sounSourceNext){
        AudioSource audio = FindObjectOfType<AudioManager>().GetSoundSource(soundSource);
        audio.Play();

        yield return new WaitWhile(() => audio.isPlaying);
        FindObjectOfType<AudioManager>().GetSoundSource(sounSourceNext).Play();
    }

    IEnumerator StartGame(){
        Debug.Log("Start Coroutine");
        Stopwatch watch = new Stopwatch();
        watch.Start();
        while (watch.Elapsed.TotalSeconds < 2) {
            yield return null;
        }
        Debug.Log("2 secs elapsed.");
        // arHelpCanvas.SetActive(true);
        timeUntilHint = Time.time + 20.0f;
        FindObjectOfType<AudioManager>().Play("emma");
        StopCoroutine("StartGame");
    }
}
