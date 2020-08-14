using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Based on source https://www.youtube.com/watch?v=7KR5IKi8m8g

public class SceneController : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Debug.Log("Change Scene " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
