using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TowerStacker : MonoBehaviour
{
    public GameObject cubePrefab;
    public int counter = 0;
    public int highScore;
    private bool gameOver = false;
    private float cooldownDuration = 1.0f;
    private float canSpawn;
    public Text score;
    private List<GameObject> cubes;
    private SpawnObjectsOnPlane spawnObjectsOnPlane;
    private GameProgress gameProgress;

        private void Awake()
    {
        cubes = new List<GameObject>();
        spawnObjectsOnPlane = GameObject.FindObjectOfType<SpawnObjectsOnPlane> ();
        gameProgress = GameObject.FindObjectOfType<GameProgress>();

    }


    // Update is called once per frame
    void Update()
    {
        if (spawnObjectsOnPlane.placementModeActive) {
            return;
        }

        foreach(var t in Input.touches) {
            if (t.phase != TouchPhase.Began)
            continue;
            var ray = Camera.main.ScreenPointToRay(t.position);
            RaycastHit hitInfo;
            if(!gameOver && Time.time > canSpawn && Physics.Raycast(ray, out hitInfo)) {
                // Create a Cooldown so the Player can't spawn too many Cubes to cheat
                Debug.Log("Resetting Cooldown...");
                canSpawn = Time.time + cooldownDuration;

                // Instantiate Cube
                var go = GameObject.Instantiate(cubePrefab,hitInfo.point + new Vector3(0, 2, 0), Quaternion.identity);
                go.transform.rotation = hitInfo.transform.rotation;

                // Give the Cube an unique Name and at it to the List with all Cubes
                counter++;
                go.gameObject.name = "Läckerli " + counter;
                cubes.Add(go);
                // score.text = "HIGHSCORE: " + counter;
                // go.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            }
        }
        // Check if every GameObject (except first) is still higher than the one spawned before. False = GameOver
        // if (cubes.TrueForAll(f => cubes.IndexOf(f) == 0 || f.gameObject.transform.position.y >= cubes[cubes.IndexOf(f)-1].gameObject.transform.position.y)){
        if (!CollisionDetector.collided) {
            highScore = cubes.Count;
            score.text = "LÄCKERLI: " + (highScore + 1);
            Debug.Log("Collision with everything AAAIGHT");
        } else {
            Debug.Log("Collision with everything GAME OVER!!");
            score.text = "Game Over! Highscore: " + (highScore);

            int stars = 0;

            if (highScore > 5) {
                stars = 3;
            } else if (highScore > 4 ) {
                stars = 2;
            } else if (highScore > 2 ) {
                stars = 1;
            }

            MiniGame towerstacker = new MiniGame(0, "Towerstacker", "Baue einen Turm mit Läckerli so hoch du kannst", highScore, stars);
            gameProgress.SaveMiniGame(towerstacker);
        }
    }

    private void OnDestroy() {
        CollisionDetector.collided = false;
    }
}
