using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class saveName : MonoBehaviour
{
    public TextMeshProUGUI inputField;
    private GameProgress gameProgress;

    void Start()
    {
        gameProgress = new GameProgress();
    }

    // only accept names between one and 15 letters to show it during game
    public void CheckIsValidAndSave(TextMeshProUGUI exceptionPanel)
    {
        int charCount = inputField.text.Length;
        Debug.Log("Länge: " + charCount);
        if (charCount > 1 && charCount < 15)
        {
            gameProgress.setPlayerName(inputField.text);
        }
        else
        {
            exceptionPanel.text = "Bitte gib einen gültigen Spielernamen ein";
        }
    }
}
