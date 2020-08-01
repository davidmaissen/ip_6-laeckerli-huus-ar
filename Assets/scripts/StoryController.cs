using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            canvasSuccess.SetActive(true);
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
