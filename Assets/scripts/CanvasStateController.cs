using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasStateController : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject[] setActive;
    public GameObject[] setInactive;
    private GameProgress gameProgress;

    void Start()
    {
        gameProgress = new GameProgress();
        gameProgress.InitializeGameData();

        foreach (GameObject obj in setActive)
        {
             obj.SetActive(true);   
        }

        foreach (GameObject obj in setInactive)
        {
             obj.SetActive(false);   
        }

        if (SceneManager.GetActiveScene().name.Equals("explore") && !(GameProgress.tutorialCompleted))
        {
            showTutorial(tutorial);
            GameProgress.tutorialCompleted = true;
        }

    }

    public void showTutorial(GameObject tutorial)
    {
        tutorial.SetActive(true);
    }

    public void ToggleQuitMenu() 
    {
        GameObject go = GameObject.Find("UserInterface").transform.Find("Canvas-Quit-Success").gameObject;
        go.SetActive(!go.activeSelf);
    }
}
