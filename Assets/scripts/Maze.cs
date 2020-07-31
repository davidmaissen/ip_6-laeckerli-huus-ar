using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject circle3d;
    //public TextMeshProUGUI score;

    public GameObject successCanvas;

    public GameObject uiController;
    private int counter = 0;
    private int highScore;
    private bool gameOver = false;
    private float cooldownDuration = 1.0f;
    private float canSpawn;
    
    private List<GameObject> cubes;
    private SpawnObjectsOnPlane spawnObjectsOnPlane;
    private GameProgress gameProgress;
    private GameSuccessController gameSuccessController;

    private int gameID = 3;

    // Start is called before the first frame update
    void Awake()
    {
        gameSuccessController = uiController.GetComponent<GameSuccessController>();
        gameProgress = new GameProgress();
    
        
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnObjectsOnPlane.placementModeActive || gameOver) {
        return;
        }

         if (!CollisionDetector.floorCollided) {
          gameSuccessController.updateProgress(0 + 1, 2);
        }

        
    }
}
