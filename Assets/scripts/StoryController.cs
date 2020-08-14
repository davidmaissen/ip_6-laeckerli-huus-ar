using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class StoryController : MonoBehaviour
{
    public static bool successShown = false;
    private GameProgress gameProgress;
    public GameObject canvasSuccess;

    void Start()
    {
        gameProgress = new GameProgress();
        if(GameProgress.tutorialCompleted && gameProgress.checkifStoryCompleted())
        {
            Debug.Log(GameProgress.tutorialCompleted);
            Debug.Log("Game Completed!");
            StartCoroutine(ShowSuccessScene());            
        }      
    }

    // show game success scene if every game has been completed and success scene hasn't been shown before
    IEnumerator ShowSuccessScene()
    {
        if (!successShown) {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (watch.Elapsed.TotalSeconds < 3) {
                yield return null;
            }
            canvasSuccess.SetActive(true);
            FindObjectOfType<AudioManager>().Play("gameover");
            successShown = true;
        }
        StopCoroutine("ShowSuccessScene");
    }
}
