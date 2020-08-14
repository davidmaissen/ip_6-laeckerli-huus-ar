using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

// Based on source https://www.youtube.com/watch?v=I9j3MD7gS5Y

[RequireComponent(typeof(ARTrackedImageManager))]
public class MarkerTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeablePrefab;
    public GameObject gameStartScreen;
    public GameObject gamePlayButton;
    public TextMeshProUGUI gameTitle;
    public TextMeshProUGUI gameDescription;
    public Sprite emma;
    public GameObject imageAlex;
    public Animator starCountAnimation;
    private GameProgress gameProgress;
    private MiniGame selectedMiniGame;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    private GameUpdateController starsCountController;
    private static bool airPlaneStarCollected = false;
    private static bool oldImagesStarCollected = false;
    private static bool museStarCollected = false;
    private static bool productsStarCollected = false;
    private static bool historyStarCollected = false;

    private void Awake() 
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        starsCountController = FindObjectOfType<GameUpdateController>();
        gameProgress = new GameProgress();
        foreach(GameObject prefab in placeablePrefab)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }

    private void OnEnable() 
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable() 
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs) 
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added) 
        {
            UpdateImage(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.updated) 
        {
            if (trackedImage.trackingState == TrackingState.Tracking) UpdateImage(trackedImage);
            if (trackedImage.trackingState == TrackingState.Limited) spawnedPrefabs[trackedImage.referenceImage.name].SetActive(false);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.removed) 
        {
            spawnedPrefabs[trackedImage.referenceImage.name].SetActive(false);
        }
    }

    // show game start buttons, stars if not collected or questions if its star hasn't been collected yet
    private void UpdateImage(ARTrackedImage trackedImage) 
    {
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
        if (!prefab.name.Contains("star")) 
        {
            ShowGameIcon(prefab);
        }

        if (prefab.name.Contains("question")) 
        {
            prefab.transform.Rotate(0,-90,0);
        }

        prefab.SetActive(true);
        Debug.Log(prefab.name + " spotted");
    }

    // if game objects have been hit by raycast, react to it
     void Update()
     {
        foreach(var t in Input.touches) 
        {
            if (t.phase != TouchPhase.Began)
            continue;
            var ray = Camera.main.ScreenPointToRay(t.position);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo)) 
            {
                Debug.Log(PositionSaveSystem.rotation + " -- " + PositionSaveSystem.position);
                Debug.Log(hitInfo.transform.gameObject.name + " clicked");
                // if star has been clicked, update the status of this star to collected
                if (hitInfo.transform.gameObject.name == "airplane-star" ||
                    hitInfo.transform.gameObject.name == "old-images-star" || 
                    hitInfo.transform.gameObject.name == "muse-star" ||
                    hitInfo.transform.gameObject.name == "history-star" ||
                    hitInfo.transform.gameObject.name == "products-star") 
                    {
                    StarCollected(hitInfo.transform.gameObject.name);
                }
                // if an answer of a question pannel has been clicked, show a star or fail pannel based on answer
                else if (hitInfo.transform.gameObject.name.Contains("Answer")) 
                {
                    var hit = hitInfo.transform.gameObject;
                    GameObject pannel = hit.transform.parent.gameObject;
                    GameObject question = pannel.transform.parent.gameObject;
                    if (hit.name == "Answer - Pralinen") 
                    {
                        pannel.gameObject.SetActive(false);
                        question.transform.Find("products-star").gameObject.SetActive(true);
                    } 
                    else if (hit.name == "Answer - Brot") 
                    {
                        GameObject wrongAnswer = question.transform.Find("Pannel - Wrong").gameObject;
                        StartCoroutine(AnswerWrong(pannel, wrongAnswer));
                    } 
                    else if (hit.name == "Answer - 1904") 
                    {
                        pannel.gameObject.SetActive(false);
                        question.transform.Find("history-star").gameObject.SetActive(true);
                    } 
                    else if (hit.name == "Answer - 2001") 
                    {
                        GameObject wrongAnswer = question.transform.Find("Pannel - Wrong").gameObject;
                        StartCoroutine(AnswerWrong(pannel, wrongAnswer));
                    }
                } 
                else
                // show mini game button
                {
                    gameStartScreen.SetActive(true);
                    selectedMiniGame = Array.Find(GameProgress.miniGames, minigame => minigame.getTitleKey() == hitInfo.transform.gameObject.name);
                    gameTitle.text = selectedMiniGame.getTitle();
                    gameDescription.text = selectedMiniGame.getDescription();
                    gamePlayButton.GetComponent<Button>().onClick.AddListener(LoadScene);
                    FindObjectOfType<AudioManager>().Play(selectedMiniGame.getTitleKey());
                    // if the mini game is find-alex, show emma insted of alex
                    if (selectedMiniGame.getTitleKey() == "find-alex") 
                    {
                        imageAlex.gameObject.GetComponent<Image>().sprite = emma;
                    }
                }
            }
        }
    }

    // if star is collected, update progress and play animation and hide star
    private void StarCollected(string name) 
    {
        if (name == "airplane-star") airPlaneStarCollected = true;
        else if (name == "old-images-star") oldImagesStarCollected = true;
        else if (name == "muse-star") museStarCollected = true;
        else if (name == "products-star") productsStarCollected = true;
        else if (name == "history-star") historyStarCollected = true;
        Debug.Log(GameProgress.starsCollected);
        starCountAnimation.Play("Animate-StarCount");
        GameProgress.starsCollected++;
        starsCountController.updateStarsCounter();
        Debug.Log(GameProgress.starsCollected);
        foreach(GameObject go in spawnedPrefabs.Values) 
        {
            if (go.name.Contains(name)) 
            {
                go.SetActive(false);
                Debug.Log(go.name + " is active: " + go.activeSelf);
            }
        }
    }
    private bool IsStarCollected(string name) 
    {
        return (name == "airplane-star" && airPlaneStarCollected) || 
        (name == "old-images-star" && oldImagesStarCollected) ||
        (name == "muse-star" && museStarCollected) ||
        (name == "question-history-star" && historyStarCollected) ||
        (name == "question-products-star" && productsStarCollected);
    }

    private void LoadScene() 
    {
        SceneManager.LoadScene(selectedMiniGame.getTitleKey());
    }

    private void ShowGameIcon(GameObject prefab) 
    {
        MiniGame miniGame = null;
        for (int i = 0; i < GameProgress.miniGames.Length; i++ ) 
        {
            if (GameProgress.miniGames[i].getTitleKey() == prefab.name) 
            {
            miniGame = GameProgress.miniGames[i];
            }
        }

        if (miniGame != null && miniGame.isCompleted()) 
        {
            prefab.transform.Find("completed").gameObject.SetActive(true);
            Animator animator = prefab.transform.Find("completed").Find("marker_pulse_completed").GetComponent<Animator>();
            animator.enabled = true;
            animator.Play("pulse-succ");
            prefab.transform.Find("not-completed").gameObject.SetActive(false);
        } 
        else 
        {
            prefab.transform.Find("completed").gameObject.SetActive(false);
            prefab.transform.Find("not-completed").gameObject.SetActive(true);
        }
    }

    // if the answer is wrong, show fail panel shortly until it shows question again
    IEnumerator AnswerWrong(GameObject deactivate, GameObject activate) 
    {
        Stopwatch watch = new Stopwatch();
        deactivate.gameObject.SetActive(false);
        activate.gameObject.SetActive(true);
        watch.Start();
        while (watch.Elapsed.TotalSeconds < 2) 
        {
            yield return null;
        }
        deactivate.gameObject.SetActive(true);
        activate.gameObject.SetActive(false);
        StopCoroutine("AnswerWrong");
    }

    IEnumerator LoadAsyncScene(string name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        // wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log(PositionSaveSystem.rotation + " -- " + PositionSaveSystem.position);
            yield return null;
        }
    }
}
