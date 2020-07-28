using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class CombinationController : MonoBehaviour
{
    private bool[] bowlAddedCorrectly;
    private bool whiskAddedCorrectly = false;
    private bool rollingPinAddedCorrectly;
    private bool[] puzzlesCompleted;
    private bool gameOver;
    private GameTimer gameTimer;
    private int gameID = 2;
    public Canvas canvas;


    //Controller
    public GameObject uiController;
    private GameSuccessController gameSuccessController;
    private GameProgress gameProgress;


    private void Awake() {
        gameSuccessController = uiController.GetComponent<GameSuccessController>();
        gameProgress = new GameProgress();
        gameTimer = GameObject.FindObjectOfType<GameTimer>();
        bowlAddedCorrectly = new bool[4];
        puzzlesCompleted = new bool[3];
    }

    private void Update() {
        if (Array.TrueForAll(bowlAddedCorrectly, value => { return value; })) {
            Debug.Log("bowlAddedCorrectly");
            canvas.gameObject.SetActive(true);
            puzzlesCompleted[0] = true;
            gameTimer.ReStart();
        } else if (whiskAddedCorrectly) {
            Debug.Log("whiskAddedCorrectly");
            canvas.gameObject.SetActive(true);
            gameTimer.ReStart();
            puzzlesCompleted[1] = true;
        } else if (rollingPinAddedCorrectly) {
            Debug.Log("rollingPinAddedCorrectly");
            canvas.gameObject.SetActive(true);
            gameTimer.ReStart();
            puzzlesCompleted[2] = true;
        }

        if (gameTimer.timeOver || Array.TrueForAll(puzzlesCompleted, value => { return value; })) {
            gameOver = true;
            int stars = 0;
            foreach (bool completed in puzzlesCompleted) {
                if (completed) {
                    stars ++;
                }
            }
            int highScore = (int)gameTimer.timeRemainingTotal;
            //MiniGame towerstacker = new MiniGame(2, "Combine", "Kombiniere die Stücke richtig", highScore, stars);
            //gameProgress.SaveMiniGame(towerstacker);

            gameProgress.SaveMiniGame(gameID,highScore, stars);
            gameSuccessController.showSuccessPanel(gameID, highScore, stars);
        }
    }

    public void UpdateCombination(int index, string name) {
        StartCoroutine(CollisionUpdate(index, name));
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
        } else if (name.Contains("whisk") && !whiskAddedCorrectly) {
            whiskAddedCorrectly = true;
        } else if (name.Contains("rolling-pin") && !rollingPinAddedCorrectly) {
            rollingPinAddedCorrectly = true;
        }
        StopCoroutine("CollisionUpdate");       
    }
}
