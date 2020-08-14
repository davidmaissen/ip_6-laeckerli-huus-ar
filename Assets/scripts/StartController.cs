using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartController : MonoBehaviour
{
    public TextMeshProUGUI introText;
    public GameObject canvasStart;
    public GameObject canvasInput;
    public GameObject canvasIntro;
    public GameObject canvasSettings;
    public TextMeshProUGUI inputField;
    private GameProgress gameProgress;

    void Start()
    {
        gameProgress = new GameProgress();
        gameProgress.InitializeGameData(true);
    }

    public void showInput()
    {
        canvasStart.SetActive(false);
        canvasInput.SetActive(true);
        canvasIntro.SetActive(false);
        canvasSettings.SetActive(false);
    }

    // only accept names between one and 15 letters to show it during game
    // if true, continue
    public void CheckIsValidAndSave(TextMeshProUGUI exceptionPanel)
    {
        int charCount = inputField.text.Length;
        Debug.Log("Länge: " + charCount);
        if (charCount > 1 && charCount < 15)
        {
            gameProgress.setPlayerName(inputField.text);
            showIntro();
        }
        else
        {
            exceptionPanel.text = "Bitte gib einen gültigen Spielernamen ein.";
            FindObjectOfType<AudioManager>().Stop("name");
            FindObjectOfType<AudioManager>().Play("wrong");
        }
    }

    public void showIntro()
    {
        if (GameProgress.playerName != null)
        {
            string playerName = GameProgress.playerName;
            introText.text = "Hallo " + playerName + "\r\nIch möchte die berühmten Basler Läckerli backen. Mir fehlen aber noch einige Zutaten, die in der Ausstellung versteckt sind. Hilfst Du mir, diese zu finden?";
            FindObjectOfType<AudioManager>().Play("intro");
        }
        canvasInput.SetActive(false);
        canvasIntro.SetActive(true);
    }
}
