using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerStart : MonoBehaviour
{
    public GameObject canvasStart;
    public GameObject canvasInput;
    public GameObject canvasSettings;


    private void Awake()
    {

        if (canvasStart != null)
        {
            canvasStart.SetActive(true);
        }

        if (canvasInput != null)
        {
            canvasInput.SetActive(false);
        }

        if (canvasSettings != null)
        {
            canvasSettings.SetActive(false);
        }


    }



    public void Explore()
    {

        SceneManager.LoadScene("explore");

    }

    public void ShowTutorial()
    {

        SceneManager.LoadScene("tutorial");

    }


    public void QuitGame()
    {
        Application.Quit();

    }



    //public void OpenPanel()
    //{
    //    if(SettingsPanel != null){
    //        SettingsPanel.SetActive(true);
    //    }
    //}
}
