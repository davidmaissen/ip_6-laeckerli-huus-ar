using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExploreController : MonoBehaviour
{

    public TextMeshProUGUI starsCounter;
    public TextMeshProUGUI completedGameCounter;
    public GameObject gameDetails;
    public GameObject[] ingredients;
    public GameObject tutorial;
    public GameObject menu;
    public GameObject ingredientsbar;


    private GameProgress gameProgress;
    private UIController uiController;



    // Start is called before the first frame update
    void Start()
    {

        gameProgress = new GameProgress();
        gameProgress.InitializeGameData();

        if(!(GameProgress.tutorialCompleted))
        {
            uiController.showTutorial(tutorial);
            GameProgress.tutorialCompleted = true;
        }

        //set icons of ingredientsBar
        //uiController.updateIngredientIcon(ingredients);
        uiController.updateStarsCounter(starsCounter);
       // uiController.updateCompledetGameCounter(completedGameCounter);
        uiController.updateGameDetails(gameDetails);
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void animateIngredientsBar(Animator animation)
    {
      //  animation.SetBool("expanded", !animation.GetBool("expanded"));
         uiController.animateIngredientsBar(animation);
    }


    //public void updateIngredientIcon(GameObject[] ingredients)
    //{
    //        //gameProgress = GameObject.FindObjectOfType<GameProgress>();

    //        for (int i = 0; i < ingredients.Length; i++)
    //        {
    //            GameObject gameObject = ingredients[i];
    //            string name = gameObject.name;

    //            if (gameProgress.isGameCompleted(name))
    //            {
    //                gameObject.transform.Find("complete").gameObject.SetActive(true);
    //                gameObject.transform.Find("incomplete").gameObject.SetActive(false);
    //            }
    //            else
    //            {
    //                gameObject.transform.Find("complete").gameObject.SetActive(false);
    //                gameObject.transform.Find("incomplete").gameObject.SetActive(true);
    //            }
    //        }
    //}



    public void updateCompledetGameCounter(TextMeshProUGUI completedGameCounter)
    {
            //gameProgress = GameObject.FindObjectOfType<GameProgress>();

            //set value of completedGameCounter
            completedGameCounter = completedGameCounter.GetComponent<TextMeshProUGUI>();
            completedGameCounter.text = gameProgress.getCompletedGameCount().ToString() + "/" + GameProgress.numberOfGames;

    }

}
