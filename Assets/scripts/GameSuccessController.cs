using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameSuccessController : MonoBehaviour
{
    public GameObject successCanvas;
    public GameObject successQuitCanvas;
    public GameObject menuTop;
    public GameObject menuBottom;

   //Controller
    private StarStateController starStateController;
    private GameProgress gameProgress;
    public GameObject uiController;


    void Awake()
    {
        gameProgress = new GameProgress();
        starStateController = uiController.GetComponent<StarStateController>();
        updateProgress(0, 0);
    }

    public void updateProgress(int count, int starsCount)
    {   
        Debug.Log("update Progress");
        GameObject stars = menuTop.transform.Find("Stars").gameObject;
        starStateController.setStarGroup(stars, starsCount);

        switch(SceneManager.GetActiveScene().name)
        {
            case "laeckerli-tower":
            case "maze":
            TextMeshProUGUI counter = menuTop.transform.Find("Counter/Counter-Text").gameObject.GetComponent<TextMeshProUGUI>();
            counter.text = count.ToString();
            break;

            case "combination":
            if (count == 1) {
                GameObject bowl = menuTop.transform.Find("bowl-completed").gameObject;
                bowl.SetActive(true);
                Debug.Log(bowl.name + " " + bowl.activeSelf);
            } else if (count == 2) {
                GameObject whisk = menuTop.transform.Find("whisk-completed").gameObject;
                whisk.SetActive(true);
                Debug.Log(whisk.name + " " + whisk.activeSelf);
            } else if (count == 3) {
                GameObject rollingPin = menuTop.transform.Find("rolling-pin-completed").gameObject;
                rollingPin.SetActive(true);
                Debug.Log(rollingPin.name + " " + rollingPin.activeSelf);
            }
            break;

            case "find-alex":
            
            break;
        }
        

    }

    public void ShowSuccessPanel(bool gameOver, int gameID, int? highscore, int starsCount)
    {   
        (string name, Sprite imageActive, Sprite imageInactive) gameData = gameProgress.GetIngredientInfo(gameID);
        GameObject stars;
        GameObject ingredientIcon; 
        TextMeshProUGUI ingredientLabel;
        TextMeshProUGUI highscoreLabel;
        TextMeshProUGUI textAlex;

        if (gameOver)
        {
            stars = successCanvas.transform.Find("Panel-Detail/Game-Success/Stars").gameObject;
            ingredientIcon = successCanvas.transform.Find("Panel-Detail/Game-Success/Label-Success-Detail/Ingredient-Icon").gameObject; 
            ingredientLabel = successCanvas.transform.Find("Panel-Detail/Game-Success/Label-Success-Detail/Title-Game").gameObject.GetComponent<TextMeshProUGUI>();
            highscoreLabel = successCanvas.transform.Find("Panel-Detail/Game-Success/Label-Success-Detail/HighScore-Game/HighScore-Text").gameObject.GetComponent<TextMeshProUGUI>();
            textAlex = successCanvas.transform.Find("Panel-Detail/Alex/Speech-Bubble/Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();
            switch (starsCount) {
                case 0:
                textAlex.text = "Schade!";
                FindObjectOfType<AudioManager>().Play("fail");
                break;
                case 1:
                textAlex.text = "Gut!";
                FindObjectOfType<AudioManager>().Play("good");
                break;
                case 2:
                textAlex.text = "Bravo!";
                FindObjectOfType<AudioManager>().Play("bravo");
                break;
                case 3:
                textAlex.text = "Perfekt!";
                FindObjectOfType<AudioManager>().Play("perfect");
                break;
            }
            successCanvas.SetActive(true);
        } else 
        {
            stars = successQuitCanvas.transform.Find("Panel-Detail/Game-Success/Stars").gameObject;
            ingredientIcon = successQuitCanvas.transform.Find("Panel-Detail/Game-Success/Label-Success-Detail/Ingredient-Icon").gameObject;
            ingredientLabel = successQuitCanvas.transform.Find("Panel-Detail/Game-Success/Label-Success-Detail/Title-Game").gameObject.GetComponent<TextMeshProUGUI>();
            highscoreLabel = successQuitCanvas.transform.Find("Panel-Detail/Game-Success/Label-Success-Detail/HighScore-Game/HighScore-Text").gameObject.GetComponent<TextMeshProUGUI>();
            successQuitCanvas.SetActive(true);
        }

        if(starsCount > 0)
        {
            ingredientLabel.text = gameData.Item1 + " erhalten";
        }
        else
        {
            ingredientLabel.text = gameData.Item1 + " nicht erhalten";
        }
        
        highscoreLabel.text = highscore != null ? highscore.ToString() : "-";
        if (starsCount < 1) {
            ingredientIcon.transform.GetComponent<Image>().sprite = gameData.Item3;
        } else {
            ingredientIcon.transform.GetComponent<Image>().sprite = gameData.Item2;
        }

        menuBottom.SetActive(false);
        starStateController = uiController.GetComponent<StarStateController>();
        starStateController.setStarGroup(stars, starsCount);
    }
}
