using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController
{
    public void showTutorial(GameObject tutorial)
    {
        tutorial.SetActive(true);
    }

    public void disableMenu(Animator animation)
    {
        animation.SetBool("displayed", false);
    }

    public void enableMenu(Animator animation)
    {
        animation.SetBool("displayed", true);
    }

    public void animateIngredientsBar(Animator animation)
    {
        animation.SetBool("expanded", !animation.GetBool("expanded"));
    }

    public void updateStarsCounter(TextMeshProUGUI starsCounter)
    {
        starsCounter = starsCounter.GetComponent<TextMeshProUGUI>();
        starsCounter.text = GameProgress.starsCollected.ToString();
    }

    public void updateGameDetails(GameObject gameDetails)
    {
        TextMeshProUGUI gameTitle = gameDetails.transform.Find("Title-Game").gameObject.GetComponent<TextMeshProUGUI>();
        Debug.Log(gameTitle.text);
        gameTitle.text = "Läckerliturm";
    }
}
