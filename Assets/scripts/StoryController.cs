using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class StoryController : MonoBehaviour
{

    private GameProgress gameProgress;
    public GameObject canvasSuccess;

    // Start is called before the first frame update
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

    IEnumerator ShowSuccessScene()
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        while (watch.Elapsed.TotalSeconds < 3) {
            yield return null;
        }

        SceneManager.LoadScene("success-board");
        StopCoroutine("ShowSuccessScene");
    }
}
