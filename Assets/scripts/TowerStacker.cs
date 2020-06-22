using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStacker : MonoBehaviour
{
    public GameObject cubePrefab;
    private SpawnObjectsOnPlane spawnObjectsOnPlane;

        private void Awake()
    {
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
            if(Physics.Raycast(ray, out hitInfo)) {
                var go = GameObject.Instantiate(cubePrefab,hitInfo.point + new Vector3(0, 2, 0), Quaternion.identity);
                // go.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            }
        }
    }
}
