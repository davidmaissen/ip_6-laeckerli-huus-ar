using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Maze : MonoBehaviour
{
    public GameObject levelSuccessCanvas;
    public GameObject uiController;
    public GameObject[] levels;
    private GameObject[] spawnedlevels;
    private GameObject activeLevel;
    private int comletedLevels = 0;
    private bool gameStarted = false;
    private bool gameCompleted;
    private int highScore = 0;
    private bool gameOver = false;
    private float cooldownDuration = 1.0f;
    private float canSpawn;
    private List<GameObject> cubes;
    private SpawnObjectsOnPlane spawnObjectsOnPlane;
    private GameProgress gameProgress;
    private GameSuccessController gameSuccessController;
    private MazePlayer player;
    private int gameID = 3;

    void Awake()
    {
        gameSuccessController = uiController.GetComponent<GameSuccessController>();
        gameProgress = new GameProgress();
        spawnObjectsOnPlane = GameObject.FindObjectOfType<SpawnObjectsOnPlane> (); 
        gameCompleted = false;
        spawnedlevels = new GameObject[3];
        levelSuccessCanvas.SetActive(false);
    }

    void Update()
    {
        if (spawnObjectsOnPlane.placementModeActive || gameOver) 
        {
            return;
        }

        // initialise scene with levels, as soon as plane detection has been completed
        if (!gameStarted) 
        {
            Debug.Log("Start Game");
            gameStarted = true;
            activeLevel = GameObject.FindWithTag("Respawn");
            activeLevel.SetActive(false);

            for (int i = 0; i < levels.Length; i++) 
            {
                GameObject newPrefab = Instantiate(levels[i], activeLevel.transform.position, activeLevel.transform.rotation);
                Vector3 eulerRotation = transform.rotation.eulerAngles;
                newPrefab.transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);
                newPrefab.name = levels[i].name;
                newPrefab.SetActive(false);
                spawnedlevels[i] = newPrefab;
            }
            spawnedlevels[0].SetActive(true);
        }
        
        // if ball has touched the goal or an area it shouldn't have to, update mini game progress
        player = GameObject.FindObjectOfType<MazePlayer>();
        Debug.Log("gefunden: " + player.name);

        if (player.LevelCompleted())
        {
            comletedLevels++;
            if (gameCompleted){
                return;
            }
            else
            {
                Debug.Log("Level completed");
                LevelCompleted();
            }
        }

        if (player.LevelLost()) 
        {
            Debug.Log("Level completed - No hits left");
            gameOver = true;
            SaveMiniGame();
        }
    }

    // if level has been completed, show success panel of level or of mini game if last level has been completed
    void LevelCompleted() 
    {
        Debug.Log("Completed Levels: " + comletedLevels);
        highScore += player.touchesCounter;
        if (comletedLevels == levels.Length)
        {
            gameCompleted = true;
            gameOver = true;
            SaveMiniGame();
            return;
        }
        else
        {
            Debug.Log("Load new Level");
            foreach (GameObject level in levels) 
            {
                level.SetActive(false);
            }
            spawnedlevels[comletedLevels-1].SetActive(false);
            levelSuccessCanvas.SetActive(true);
            FindObjectOfType<AudioManager>().Play("level");
        }
    }

    public void NextLevel()
	{
        levelSuccessCanvas.SetActive(false);
        spawnedlevels[comletedLevels].SetActive(true);
        player = spawnedlevels[comletedLevels].GetComponent<MazePlayer>();
    }

    public void SaveMiniGame() 
    {
        gameProgress.SaveMiniGame(gameID, highScore, comletedLevels);
        gameSuccessController.ShowSuccessPanel(gameOver, gameID, highScore, comletedLevels);
    }
}
