using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsStateController : MonoBehaviour
{
    public GameObject icons;
    private GameProgress gameProgress;


    // Start is called before the first frame update
    void Start()
    {
        gameProgress = new GameProgress();
        gameProgress.InitializeGameData();
        updateIngredientIcon(icons);
  
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void updateIngredientIcon(GameObject ingredient)
    {
      //  gameProgress = GameObject.FindObjectOfType<GameProgress>();

        string name = ingredient.name;
       

        if (gameProgress.isGameCompleted(name))
            {
            ingredient.transform.Find("complete").gameObject.SetActive(true);
            ingredient.transform.Find("incomplete").gameObject.SetActive(false);
            }
            else
            {
            ingredient.transform.Find("complete").gameObject.SetActive(false);
            ingredient.transform.Find("incomplete").gameObject.SetActive(true);
            }
    }

}
