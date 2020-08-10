using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.XR.ARFoundation;

public class FindAlexController : MonoBehaviour
{
    private GameObject scenery;
    public GameObject arHelpCanvas;
    private SpawnObjectsOnPlane spawnObjectsOnPlane;
    public Material[] materials;
    private float timeUntilHint;
    private bool gameOver = false;
    private bool gameStarted = false;
    private bool touchInfoNeeded = true;
    private int stars = 0;
    private int gameID = 1;

    //Controller
    private GameProgress gameProgress;
    private GameSuccessController gameSuccessController;
    public GameObject uiController;


    private void Awake() {
        gameSuccessController = uiController.GetComponent<GameSuccessController>();
        gameProgress = new GameProgress();
        spawnObjectsOnPlane = GameObject.FindObjectOfType<SpawnObjectsOnPlane>();
    }

    void Update(){
        if (spawnObjectsOnPlane.placementModeActive || gameOver) return;
        if (!gameStarted) StartCoroutine(StartGame());
        if (touchInfoNeeded) arHelpCanvas.SetActive(true);
        if (timeUntilHint > 0 && Time.time > timeUntilHint) {
            scenery.transform.Find("text-emma").gameObject.SetActive(false);
            scenery.transform.Find("text-emma-3").gameObject.SetActive(true);
            timeUntilHint += 90.0f;
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
        TouchInfoNotNeeded();
        string hit = hitInfo.transform.gameObject.name;

        if (hit == "door-house-1") {
            scenery.transform.Find("door-house-1").gameObject.GetComponent<Renderer>().material = materials[0];
            scenery.transform.Find("door-house-1-open").gameObject.SetActive(true);
            Animator animator = scenery.transform.Find("door-house-1-open").GetComponent<Animator>();
            animator.Play("door-open");
            scenery.transform.Find("dog").gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("dog-bark");
        } else if (hit == "bicyclist") {
            scenery.transform.Find("text-bicyclist").gameObject.SetActive(true);
            scenery.transform.Find("star-bicyclist").gameObject.SetActive(true);
            StartCoroutine(DoAfterPlaying("star", "bicyclist"));
        } else if (hit == "star-bicyclist") {
            scenery.transform.Find("star-bicyclist").gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("star-collected");
            stars++;
        } else if (hit == "star-dog") {
            scenery.transform.Find("star-dog").gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("star-collected");
            stars++;
        } else if (hit == "man-window") {
            scenery.transform.Find("text-man-window").gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("man-window");
        } else if (hit == "daughter-mother") {
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
        } else if (hit == "alex-found") {
            scenery.transform.Find("text-alex").gameObject.SetActive(true);
            scenery.transform.Find("text-emma").gameObject.SetActive(false);
            scenery.transform.Find("text-emma-3").gameObject.SetActive(false);
            scenery.transform.Find("text-emma-2").gameObject.SetActive(true);
            scenery.transform.Find("alex-found").gameObject.GetComponent<Renderer>().material = materials[1];
            StartCoroutine(DoAfterPlaying("alex-found", "emma-2"));
            stars++;
            gameOver = true;
            GameOver();
        } else if (hit == "emma"){
            if (scenery.transform.Find("text-emma").gameObject.activeSelf) {
                scenery.transform.Find("text-emma").gameObject.SetActive(true);
                scenery.transform.Find("text-emma-3").gameObject.SetActive(false);
                FindObjectOfType<AudioManager>().Play("emma");
            }
        } else if (hit == "birdie") {
            Animator animator = scenery.transform.Find("birdie").GetComponent<Animator>();
            animator.Play("birdie");
            FindObjectOfType<AudioManager>().Play("bird-chirp");
        }
         else if (hit == "papeterie-woman") {
            scenery.transform.Find("papeterie-woman").gameObject.GetComponent<Renderer>().material = materials[2];
            scenery.transform.Find("text-papeterie").gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("papeterie");
        }
         gameSuccessController.updateProgress(0, stars);
    }

    public void GameOver() {
        //MiniGame findAlex = new MiniGame(1, "Finde Alex", "Hilf Emma Alex zu finden", stars, stars);
        //gameProgress.SaveMiniGame(findAlex);
        gameProgress.SaveMiniGame(gameID, 0, stars);
        gameSuccessController.ShowSuccessPanel(gameOver, gameID, null, stars);
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
        gameSuccessController.updateProgress(0, stars);
        Debug.Log("Start Coroutine");
        FindObjectOfType<AudioManager>().Play("find-alex-scenery");
        Stopwatch watch = new Stopwatch();
        watch.Start();
        while (watch.Elapsed.TotalSeconds < 2) {
            yield return null;
        }
        Debug.Log("2 secs elapsed.");
        arHelpCanvas.SetActive(true);
        timeUntilHint = Time.time + 15.0f;
        FindObjectOfType<AudioManager>().Play("emma");
        scenery.transform.Find("text-emma").gameObject.SetActive(true);
        StopCoroutine("StartGame");
    }
}
