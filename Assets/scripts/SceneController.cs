using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneController : MonoBehaviour
{
    public GameObject tutorial;
    private void Awake()
    {
        if (!(GameProgress.tutorialCompleted)){

            tutorial.SetActive(true);
        }
        
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Change Scene " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
