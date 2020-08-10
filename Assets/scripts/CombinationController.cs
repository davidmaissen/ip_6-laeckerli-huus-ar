using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class CombinationController : MonoBehaviour
{
    private bool[] bowlAddedCorrectly;
    private bool[] whiskAddedCorrectly;
    private bool rollingPinAddedCorrectly;
    private bool[] puzzlesCompleted;
    private bool gameOver = false;
    private GameTimer gameTimer;
    private int gameID = 2;
    private int stars = 0;
    public Canvas canvas;
    public GameObject[] animations; 
    private ImageTrackingCombination imageTracking;


    //Controller
    public GameObject uiController;
    private GameSuccessController gameSuccessController;
    private GameProgress gameProgress;


    private void Awake() {
        gameSuccessController = uiController.GetComponent<GameSuccessController>();
        gameProgress = new GameProgress();
        gameTimer = GameObject.FindObjectOfType<GameTimer>();
        imageTracking = GameObject.FindObjectOfType<ImageTrackingCombination>();
        bowlAddedCorrectly = new bool[4];
        whiskAddedCorrectly = new bool[2];
        puzzlesCompleted = new bool[3];
    }

    private void Update() {
        if (!puzzlesCompleted[0] && Array.TrueForAll(bowlAddedCorrectly, value => { return value; })) {
            Debug.Log("bowlAddedCorrectly");
            // canvas.gameObject.SetActive(true);
            puzzlesCompleted[0] = true;
            stars++;
            gameSuccessController.updateProgress(1, stars);
            // SaveMiniGame();
        } else if (!puzzlesCompleted[1] && Array.TrueForAll(whiskAddedCorrectly, value => { return value; })) {
            Debug.Log("whiskAddedCorrectly");
            // canvas.gameObject.SetActive(true);
            puzzlesCompleted[1] = true;
            stars++;
            gameSuccessController.updateProgress(2, stars);
            // SaveMiniGame();
        } else if (!puzzlesCompleted[2] && rollingPinAddedCorrectly) {
            Debug.Log("rollingPinAddedCorrectly");
            // canvas.gameObject.SetActive(true);
            puzzlesCompleted[2] = true;
            stars++;
            gameSuccessController.updateProgress(3, stars);
            // SaveMiniGame();
        }

        if (gameTimer.timeOver || Array.TrueForAll(puzzlesCompleted, value => { return value; })) {
            gameOver = true;
            /*
            int stars = 0;
            foreach (bool completed in puzzlesCompleted) {
                if (completed) {
                    stars ++;
                }
            }
            */
            SaveMiniGame();
            
            //MiniGame towerstacker = new MiniGame(2, "Combine", "Kombiniere die Stücke richtig", highScore, stars);
            //gameProgress.SaveMiniGame(towerstacker);
            
        }
    }

    public void UpdateCombination(int index, string name) {
        StartCoroutine(CollisionUpdate(index, name));
    }

    public bool IsPuzzleCompleted(string name) {
        if (name.Contains("bowl")) {
            return puzzlesCompleted[0];
        } else if (name.Contains("whisk")) {
            return puzzlesCompleted[1];
        } else if (name.Contains("rolling-pin")) {
            return puzzlesCompleted[2];
        } else {
            return false;
        }
    }

    public void SaveMiniGame() {
        int highScore = (int)gameTimer.timeRemainingTotal;
        gameProgress.SaveMiniGame(gameID,highScore, stars);
        gameSuccessController.ShowSuccessPanel(gameOver, gameID, highScore, stars);
    }

    public void PlayAnimation(string name, Vector3 position, Quaternion rotation) {
        StartCoroutine(PlayAnimationThenGoOn(name, position, rotation));
    }

    IEnumerator CollisionUpdate(int index, string name)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        if (name.Contains("bowl") && !bowlAddedCorrectly[index]) {
            bowlAddedCorrectly[index] = true;
            while (watch.Elapsed.TotalSeconds < 0.5) {
                yield return null;
            }
            bowlAddedCorrectly[index] = false;
        } else if (name.Contains("whisk") && !whiskAddedCorrectly[index]) {
            whiskAddedCorrectly[index] = true;
            while (watch.Elapsed.TotalSeconds < 0.5) {
                yield return null;
            }
            whiskAddedCorrectly[index] = false;
        } else if (name.Contains("rolling-pin") && !rollingPinAddedCorrectly) {
            rollingPinAddedCorrectly = true;
        }
        StopCoroutine("CollisionUpdate");       
    }

    IEnumerator PlayAnimationThenGoOn(string name, Vector3 position, Quaternion rotation) {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        GameObject animation = Instantiate(Array.Find(animations, a => a.name.Contains(name)));
        animation.transform.position = position;
        animation.transform.rotation = rotation;
        imageTracking.ToggleImageTracking();

        while (watch.Elapsed.TotalSeconds < 5) {
            yield return null;
        }
        Destroy(animation);
        imageTracking.ToggleImageTracking();
        gameTimer.ReStart();
        StopCoroutine("PlayAnimationThenGoOn");
    }
}
