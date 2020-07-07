using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    public GameObject SettingsPanel;

    public void Explore()
    {

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void ShowTutorial()
    {

        SceneManager.LoadScene("tutorial");

    }


    public void QuitGame()
    {
        Application.Quit();

    }



    public void OpenPanel()
    {
        if(SettingsPanel != null){
            SettingsPanel.SetActive(true);
        }
    }
}
