using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUpdateController : MonoBehaviour
{
    public TextMeshProUGUI starsCounter;
    public TextMeshProUGUI ingredientCounter;

    private int gameCount;

    //Controller
    private GameProgress gameProgress;

    // Start is called before the first frame update
    void Start()
    {
        gameProgress = new GameProgress();
        gameProgress.InitializeGameData();
        gameCount = GameProgress.miniGames.Length;
        updateStarsCounter();
        updateIngredientCounter();
    }

    public void updateStarsCounter()
    {
        starsCounter.text = GameProgress.starsCollected.ToString();

    }

        public void updateIngredientCounter()
    {
       ingredientCounter.text = gameProgress.getCompletedGameCount().ToString() + "/" + gameProgress.GetGameCount().ToString();

    }
}
