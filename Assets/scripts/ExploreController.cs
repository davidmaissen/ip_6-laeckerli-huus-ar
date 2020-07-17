using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExploreController : MonoBehaviour
{
    public TextMeshProUGUI starsCounter;
    public TextMeshProUGUI completedGameCounter;
    public GameObject[] ingredients;
    public GameObject tutorial;
    private GameProgress gameProgress;


    // Start is called before the first frame update
    void Start()
    {
      
        if(!(GameProgress.tutorialCompleted))
        {
            tutorial.SetActive(true);
            GameProgress.tutorialCompleted = true;
        }

        //set icons of ingredientsBar
        updateIngredientsBar();

    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void updateIngredientsBar()
    {
        gameProgress = GameObject.FindObjectOfType<GameProgress>();

        //set value of starsCounter
        starsCounter = starsCounter.GetComponent<TextMeshProUGUI>();
        starsCounter.text = GameProgress.starsCollected.ToString();

        //set value of completedGameCounter
        completedGameCounter = completedGameCounter.GetComponent<TextMeshProUGUI>();
        completedGameCounter.text = gameProgress.getCompletedGameCount().ToString() + "/" + GameProgress.numberOfGames;

        for (int i = 0; i < ingredients.Length; i++)
        {
            string name = ingredients[i].name;

            if (gameProgress.isGameCompleted(name))
            {
                ingredients[i].transform.Find("complete").gameObject.SetActive(true);
                ingredients[i].transform.Find("incomplete").gameObject.SetActive(false);
            }
            else
            {
                ingredients[i].transform.Find("complete").gameObject.SetActive(false);
                ingredients[i].transform.Find("incomplete").gameObject.SetActive(true);
            }
        }


    }
}
