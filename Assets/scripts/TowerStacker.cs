using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using TMPro;

public class TowerStacker : MonoBehaviour
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

        private void Awake()
    {
        gameSuccessController = uiController.GetComponent<GameSuccessController>();
        gameProgress = new GameProgress();
        cubes = new List<GameObject>();
        spawnObjectsOnPlane = GameObject.FindObjectOfType<SpawnObjectsOnPlane> (); 
    
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnObjectsOnPlane.placementModeActive || gameOver) {
            return;
        }

        foreach(var t in Input.touches) {
            if (t.phase != TouchPhase.Began)
            continue;
            var ray = Camera.main.ScreenPointToRay(t.position);
            RaycastHit hitInfo;
            if(!gameOver && Time.time > canSpawn && Physics.Raycast(ray, out hitInfo)) {
                StartCoroutine(ShowTouchInput(hitInfo));
                // Create a Cooldown so the Player can't spawn too many Cubes to cheat
                Debug.Log("Resetting Cooldown...");
                canSpawn = Time.time + cooldownDuration;

                // Instantiate Cube
                var go = GameObject.Instantiate(cubePrefab,hitInfo.point + new Vector3(0, 1, 0), Quaternion.identity);
                go.transform.rotation = hitInfo.transform.rotation;

                // Give the Cube an unique Name and at it to the List with all Cubes
                counter++;
                go.gameObject.name = "Läckerli " + counter;
                cubes.Add(go);
                // score.text = "HIGHSCORE: " + counter;
                // go.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            }
        }

        if (CollisionDetector.cookieCollided) {
            FindObjectOfType<AudioManager>().Play("cookie");
            CollisionDetector.cookieCollided = false;
        }

        // Check if every GameObject (except first) is still higher than the one spawned before. False = GameOver
        // if (cubes.TrueForAll(f => cubes.IndexOf(f) == 0 || f.gameObject.transform.position.y >= cubes[cubes.IndexOf(f)-1].gameObject.transform.position.y)){
        if (!CollisionDetector.floorCollided) {
            highScore = cubes.Count;

           // gameSuccessController.setCounter(highScore + 1);   
            gameSuccessController.updateProgress(highScore + 1, getStarsFromCount(highScore + 1));
            //score.text = "LÄCKERLI: " + (highScore + 1);
        } else {
    
            int stars = getStarsFromCount(highScore);
            GameOver(stars);
        }
    }

    /*
    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("Läckerli: " + collisionInfo.gameObject.name);
        if (collisionInfo.gameObject.name.Contains("Läckerli")){
            Debug.Log("Läckerli: " + collisionInfo);
            Debug.Log("Läckerli: " + collisionInfo.relativeVelocity);
            Debug.Log("Läckerli: " + collisionInfo.impulse);
            FindObjectOfType<AudioManager>().Play("cookie");
        }
    }
    */

    IEnumerator ShowTouchInput(RaycastHit hitInfo){
        var inputFeedback = GameObject.Instantiate(circle3d, hitInfo.point + new Vector3(0,0.01f,0), Quaternion.identity);
        inputFeedback.transform.Rotate(90, 0, 0);
        Debug.Log("Start Coroutine");
        Stopwatch watch = new Stopwatch();
        watch.Start();
        while (watch.Elapsed.TotalSeconds < 0.5) {
            yield return null;
        }
        Destroy(inputFeedback);
        StopCoroutine("ShowTouchInput");
    }

    private void OnDestroy() {
        CollisionDetector.floorCollided = false;
        CollisionDetector.cookieCollided = false;
    }

    private void GameOver(int stars) {
        gameOver = true;
        MiniGame towerstacker = new MiniGame(0, "Towerstacker", "Baue einen Turm mit Läckerli so hoch du kannst", highScore, stars);
        gameProgress.SaveMiniGame(towerstacker);
        gameSuccessController.showSuccessPanel(stars);
    }

    private int getStarsFromCount(int count)
    {
            int stars = 0;

            if (count > 5) {
                stars = 3;
            } else if (count > 4 ) {
                stars = 2;
            } else if (count > 2 ) {
                stars = 1;
            }
            return stars;
    }


}
