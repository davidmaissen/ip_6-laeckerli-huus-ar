using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class saveName : MonoBehaviour
{

    public TextMeshProUGUI inputField;

    //Controller
    private ChangeSceneManagement sceneManagement;
    private GameProgress gameProgress;


void Start()
{
    gameProgress = new GameProgress();
    sceneManagement = GetComponent<ChangeSceneManagement>();
}

public void CheckIsValidAndSave(TextMeshProUGUI exceptionPanel)
{
    int charCount = inputField.text.Length;
    Debug.Log("Länge: " + charCount);
    if(charCount > 1)
    {
        gameProgress.setPlayerName(inputField.text);
        Debug.Log(GameProgress.playerName);
        sceneManagement.ChangeScene("explore");
    }
    else
    {
        exceptionPanel.text = "Bitte gib einen gültigen Spielernamen ein";
    }

   // TMP_TextInfo textInfo = textMP.textInfo;
}


}
