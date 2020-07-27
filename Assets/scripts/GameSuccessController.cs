using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSuccessController : MonoBehaviour
{
    public GameObject successCanvas;
    public GameObject uiController;

    public GameObject menuTop;

    public GameObject menuBottom;

    private StarStateController starStateController;


    void Start()
    {
        starStateController = uiController.GetComponent<StarStateController>();
        updateProgress(0, 0);
    }

    public void updateProgress(int count, int starsCount)
    {
        GameObject stars = menuTop.transform.Find("Stars").gameObject;
        TextMeshProUGUI counter = menuTop.transform.Find("Counter/Counter-Text").gameObject.GetComponent<TextMeshProUGUI>();

        counter.text = count.ToString();
        starStateController.setStarGroup(stars, starsCount);
    }

     public void showSuccessPanel(string ingredient, int highscore, int starsCount)
    {   
        GameObject stars = successCanvas.transform.Find("Panel-Detail/Game-Success/Stars").gameObject;
        TextMeshProUGUI ingredientLabel = successCanvas.transform.Find("Panel-Detail/Game-Success/Label-Success-Detail/Title-Game").gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI highscoreLabel = successCanvas.transform.Find("Panel-Detail/Game-Success/Label-Success-Detail/HighScore-Game/HighScore-Text").gameObject.GetComponent<TextMeshProUGUI>();

        ingredientLabel.text = ingredient;
        highscoreLabel.text = highscore.ToString();

        menuBottom.SetActive(false);
        successCanvas.SetActive(true);
        starStateController = uiController.GetComponent<StarStateController>();
        starStateController.setStarGroup(stars, starsCount);
    }
}
