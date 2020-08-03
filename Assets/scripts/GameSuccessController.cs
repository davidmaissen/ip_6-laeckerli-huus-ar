using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameSuccessController : MonoBehaviour
{
    public GameObject successCanvas;
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

     public void showSuccessPanel(int gameID, int highscore, int starsCount)
    {   
        (string name, Sprite imageActive, Sprite imageInactive) gameData = gameProgress.GetIngredientInfo(gameID);
        GameObject stars = successCanvas.transform.Find("Panel-Detail/Game-Success/Stars").gameObject;
        GameObject ingredientIcon = successCanvas.transform.Find("Panel-Detail/Game-Success/Label-Success-Detail/Ingredient-Icon").gameObject; 
        TextMeshProUGUI ingredientLabel = successCanvas.transform.Find("Panel-Detail/Game-Success/Label-Success-Detail/Title-Game").gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI highscoreLabel = successCanvas.transform.Find("Panel-Detail/Game-Success/Label-Success-Detail/HighScore-Game/HighScore-Text").gameObject.GetComponent<TextMeshProUGUI>();

        ingredientLabel.text = gameData.Item1;
        highscoreLabel.text = highscore.ToString();
        ingredientIcon.transform.GetComponent<Image>().sprite = gameData.Item2;

        menuBottom.SetActive(false);
        successCanvas.SetActive(true);
        starStateController = uiController.GetComponent<StarStateController>();
        starStateController.setStarGroup(stars, starsCount);
    }
}
