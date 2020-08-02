using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Maze : MonoBehaviour
{
    //public GameObject cubePrefab;
    //public GameObject circle3d;
    //public TextMeshProUGUI score;

    public GameObject successCanvas;

    public GameObject uiController;

    public GameObject[] levels;
    private GameObject[] spawnedlevels;

    private GameObject activeLevel;
    private int counter = 0;
    private bool gameStarted = false;
    private bool gameCompleted;
    private int highScore;
    private bool gameOver = false;
    private float cooldownDuration = 1.0f;
    private float canSpawn;
    
    private List<GameObject> cubes;
    private SpawnObjectsOnPlane spawnObjectsOnPlane;
    private GameProgress gameProgress;
    private GameSuccessController gameSuccessController;

    private MazePlayer player;

    private int gameID = 3;

    // Start is called before the first frame update
    void Awake()
    {
       // Debug.Log("Player: " + player.name);
       // gameSuccessController = uiController.GetComponent<GameSuccessController>();
        gameProgress = new GameProgress();
        spawnObjectsOnPlane = GameObject.FindObjectOfType<SpawnObjectsOnPlane> (); 
        gameCompleted = false;
        spawnedlevels = new GameObject[3];
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnObjectsOnPlane.placementModeActive || gameOver) {
        return;
        }

        if (!gameStarted) {
            gameStarted = true;
            activeLevel = GameObject.FindWithTag("Respawn");
            activeLevel.SetActive(false);

            for (int i = 0; i < levels.Length; i++) {
                GameObject newPrefab = Instantiate(levels[i], activeLevel.transform.position, activeLevel.transform.rotation);
                Vector3 eulerRotation = transform.rotation.eulerAngles;
                newPrefab.transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);
                newPrefab.name = levels[i].name;
                newPrefab.SetActive(false);
                spawnedlevels[i] = newPrefab;
            }
            spawnedlevels[0].SetActive(true);
        }
        
        player = GameObject.FindObjectOfType<MazePlayer>();
        Debug.Log("gefunden: " + player.name);

        if (player.levelStatus())
        {
            if (gameCompleted){
                int stars = 2;
                GameOver(2);
            }
            else
            {
                counter++;
                Debug.Log("Level completed");
                NextLevel();
            }

        }
        
    }


    	void NextLevel()
	{

        if (counter == levels.Length)
        {
            gameCompleted = true;
            return;
        }
        else
        {
            Debug.Log("Load new Level");
            //Destroy(activeLevel);
            foreach (GameObject level in levels) {
                level.SetActive(false);
            }
            // activeLevel = levels[counter];
            spawnedlevels[counter-1].SetActive(false);
            spawnedlevels[counter].SetActive(true);
            
            player = spawnedlevels[counter].GetComponent<MazePlayer>();
        }

	}


        private void GameOver(int stars) {
        gameOver = true;
        //MiniGame towerstacker = new MiniGame(0, "Towerstacker", "Baue einen Turm mit Läckerli so hoch du kannst", highScore, stars);
        //gameProgress.SaveMiniGame(towerstacker);
       // gameProgress.SaveMiniGame(gameID,highScore, stars);
        //gameSuccessController.showSuccessPanel(gameID, highScore, stars);
    }



}
