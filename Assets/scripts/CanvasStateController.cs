using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasStateController : MonoBehaviour
{

    public GameObject tutorial;
    private GameProgress gameProgress;


    // Start is called before the first frame update
    void Start()
    {
        gameProgress = new GameProgress();
        gameProgress.InitializeGameData();

        //set canvas visibilty
        tutorial.SetActive(false);


        if(!(GameProgress.tutorialCompleted))
        {
            showTutorial(tutorial);
            GameProgress.tutorialCompleted = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


        public void showTutorial(GameObject tutorial)
    {
        tutorial.SetActive(true);
    }

}
