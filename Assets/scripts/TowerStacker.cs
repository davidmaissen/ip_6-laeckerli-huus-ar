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
    // private bool cooldown = false;
    public Text score;
    private List<GameObject> cubes;
    private SpawnObjectsOnPlane spawnObjectsOnPlane;

        private void Awake()
    {
        cubes = new List<GameObject>();
        spawnObjectsOnPlane = GameObject.FindObjectOfType<SpawnObjectsOnPlane> ();
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
            if(!gameOver && Physics.Raycast(ray, out hitInfo)) {
                Invoke("ResetCoolDown", 1.0f);
                // cooldown = true;
                var go = GameObject.Instantiate(cubePrefab,hitInfo.point + new Vector3(0, 2, 0), Quaternion.identity);
                go.gameObject.name = "Läckerli " + counter;
                cubes.Add(go);
                counter++;
                // score.text = "HIGHSCORE: " + counter;
                // go.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            }
        }
        // Check if every GameObject (except first) is still higher than the one spawned before
        if (cubes.TrueForAll(f => cubes.IndexOf(f) == 0 || f.gameObject.transform.position.y >= cubes[cubes.IndexOf(f)-1].gameObject.transform.position.y)){
            highScore = cubes.Count;
            score.text = "HIGHSCORE: " + highScore;
            Debug.Log("Collision with everything AAAIGHT");
        } else {
            Debug.Log("Collision with everything GAME OVER JUNGE!!");
            score.text = "Game Over Junge! Highscore: " + highScore;
        }
    }
    
    /*
    void ResetCooldown() {
        cooldown = false;
    }
    */
}
