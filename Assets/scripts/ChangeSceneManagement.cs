using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManagement : MonoBehaviour
{
    // Source: https://www.youtube.com/watch?v=7KR5IKi8m8g

    public void ChangeScene(string sceneName) {
        Debug.Log("Change Scene " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
