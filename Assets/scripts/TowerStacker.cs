﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using TMPro;

// Based on source: https://www.youtube.com/watch?v=JT9A1tBj79o&t=351s
public class TowerStacker : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject circle3d;
    public GameObject successCanvas;
    public GameObject uiController;
    private int counter = 0;
    private int stars = 0;
    private int highScore = 0;
    private bool gameOver = false;
    private bool gameStarted = false;
    private float cooldownDuration = 1.0f;
    private float canSpawn;
    private List<GameObject> cubes;
    private SpawnObjectsOnPlane spawnObjectsOnPlane;
    private GameProgress gameProgress;
    private GameSuccessController gameSuccessController;
    private int gameID = 0;
    private void Awake()
    {
        gameSuccessController = uiController.GetComponent<GameSuccessController>();
        gameProgress = new GameProgress();
        cubes = new List<GameObject>();
        spawnObjectsOnPlane = GameObject.FindObjectOfType<SpawnObjectsOnPlane>();
    }

    void Update()
    {
        if (spawnObjectsOnPlane.placementModeActive) 
        {
            gameSuccessController.updateProgress(1, 0);
            return;
        }

        if (!gameStarted) 
        {
            gameStarted = true;
            FindObjectOfType<AudioManager>().Play("music");
        }

        if (gameOver) return;

        // spawn a cookie based on touch position
        foreach(var t in Input.touches) 
        {
            if (t.phase != TouchPhase.Began)
            continue;
            var ray = Camera.main.ScreenPointToRay(t.position);
            RaycastHit hitInfo;
            if(!gameOver && Time.time > canSpawn && Physics.Raycast(ray, out hitInfo)) 
            {
                if (!hitInfo.transform.gameObject.GetComponent<IsCanvasClicked>().goClicked) 
                {
                    return;
                }
                StartCoroutine(ShowTouchInput(hitInfo));
                Debug.Log("Resetting Cooldown...");
                canSpawn = Time.time + cooldownDuration;

                // snstantiate cookie
                var go = GameObject.Instantiate(cubePrefab,hitInfo.point + new Vector3(0, 1, 0), Quaternion.identity);
                go.transform.rotation = hitInfo.transform.rotation;
                
                // give the cookie an unique name and at it to the List with all cookies
                go.gameObject.name = "laeckerli " + counter;
                cubes.Add(go);
            }
        }

        // if cookie collided the floor the game is over
        if (CollisionDetector.floorCollided) 
        {
            gameSuccessController.updateProgress(highScore, getStarsFromCount(highScore));
            gameOver = true;
            GameOver();
        }

        // if cookie collided with another, play sound and update the progress
        else if (CollisionDetector.cookieCollided) 
        {
            Debug.Log("Play Cookie");
            FindObjectOfType<AudioManager>().Play("cookie");
            CollisionDetector.cookieCollided = false;

            highScore = cubes.Count;
            stars = getStarsFromCount(cubes.Count);
            counter++;
            gameSuccessController.updateProgress(highScore + 1, getStarsFromCount(highScore + 1));
        }
    }

    // show touch input on tapped game object to give visual feedback
    IEnumerator ShowTouchInput(RaycastHit hitInfo)
    {
        var inputFeedback = GameObject.Instantiate(circle3d, hitInfo.point + new Vector3(0,0.01f,0), Quaternion.identity);
        inputFeedback.transform.Rotate(90, 0, 0);
        Debug.Log("Start Coroutine");
        Stopwatch watch = new Stopwatch();
        watch.Start();
        while (watch.Elapsed.TotalSeconds < 0.5) 
        {
            yield return null;
        }
        Destroy(inputFeedback);
        StopCoroutine("ShowTouchInput");
    }

    private void OnDestroy() 
    {
        CollisionDetector.floorCollided = false;
        CollisionDetector.cookieCollided = false;
    }

    public void GameOver() 
    {
        gameProgress.SaveMiniGame(gameID,highScore, stars);
        gameSuccessController.ShowSuccessPanel(gameOver, gameID, highScore, stars);
    }

    // give stars based on cookies on stacked tower
    private int getStarsFromCount(int count)
    {
            int stars = 0;

            if (count > 6) 
            {
                stars = 3;
            } 
            else if (count > 4 ) 
            {
                stars = 2;
            } 
            else if (count > 2 ) 
            {
                stars = 1;
            }
            return stars;
    }


}
