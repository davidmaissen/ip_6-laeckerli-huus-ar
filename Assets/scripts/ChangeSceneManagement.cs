using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManagement : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Debug.Log("Change Scene " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

}
