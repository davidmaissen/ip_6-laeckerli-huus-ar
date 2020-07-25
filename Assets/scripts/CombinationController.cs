using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class CombinationController : MonoBehaviour
{
    private bool[] bowlAddedCorrectly;
    private bool[] spoonAddedCorrectly;
    private bool[] tableAddedCorrectly;
    public Canvas canvas;

    private void Awake() {
        bowlAddedCorrectly = new bool[4];
        spoonAddedCorrectly = new bool[3];
        tableAddedCorrectly = new bool[3];
    }

    private void Update() {
        /*
        Debug.Log("---");
        foreach (bool b in bowlAddedCorrectly) Debug.Log(b);
        Debug.Log("---");
        */
        if (Array.TrueForAll(bowlAddedCorrectly, value => { return value; })) {
            Debug.Log("bowlAddedCorrectly");
            canvas.gameObject.SetActive(true);
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
        } else if (name.Contains("spoon") && !spoonAddedCorrectly[index]) {
            spoonAddedCorrectly[index] = true;
            while (watch.Elapsed.TotalSeconds < 0.5) {
                yield return null;
            }
            spoonAddedCorrectly[index] = false;
        } else if (name.Contains("table") && !tableAddedCorrectly[index]) {
            tableAddedCorrectly[index] = true;
            while (watch.Elapsed.TotalSeconds < 0.5) {
                yield return null;
            }
            tableAddedCorrectly[index] = false;
        }
        StopCoroutine("CollisionUpdate");       
    }
}
