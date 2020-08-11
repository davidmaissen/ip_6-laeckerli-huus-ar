using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsStateController : MonoBehaviour
{
    public int gameID;

    private GameProgress gameProgress;


    // Start is called before the first frame update
    void Start()
    {
        gameProgress = new GameProgress();
        gameProgress.InitializeGameData();
        SetIngredientStates(gameID);  
    }

 public void SetIngredientStates(int gameID)
    {
        GameObject ingredient = this.gameObject;
        ingredient.transform.GetComponent<Image>().sprite = gameProgress.GetIngredientIcon(gameID, true);
    }

}
