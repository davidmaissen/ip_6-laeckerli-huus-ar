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

    private GameObject activeLevel;
    private int counter = 0;
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
        
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnObjectsOnPlane.placementModeActive || gameOver) {
        return;
        }
        
        activeLevel = GameObject.FindWithTag("Respawn");
        player = GameObject.FindObjectOfType<MazePlayer> ();
        Debug.Log("gefunden: " + player.name);

        if(player.levelStatus())
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

        if(counter == levels.Length)
        {
            gameCompleted = true;
            return;
        }
        else
        {
            Debug.Log("Load new Level");
            Vector3   position = activeLevel.transform.position;
            Quaternion  rotation = activeLevel.transform.rotation;
            Destroy(activeLevel);
            activeLevel = Instantiate(levels[counter], position, rotation);
            
            player = activeLevel.GetComponent<MazePlayer>();
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
