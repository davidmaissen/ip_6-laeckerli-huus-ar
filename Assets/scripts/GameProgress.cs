using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress
{
    public static MiniGame[] miniGames;
    public static int numberOfGames = 3;
    public static int starsCollected = 1;
    public static bool tutorialCompleted = false;

    /*
    private void Awake() {
        if (miniGames == null) {
            miniGames = new MiniGame[numberOfGames];
            miniGames[0] = new MiniGame(0, "Towerstacker", "Baue einen Turm mit Läckerli so hoch du kannst", 0, 0);
            miniGames[1] = new MiniGame(1, "Find Alex", "Hilf Emma Alex zu suchen", 0, 0);
            miniGames[2] = new MiniGame(2, "Combine", "Kombiniere richtig", 0, 0);
            Debug.Log("Creating new MiniGame Array");
        }
    }
    */

    public void InitializeGameData()
    {
        if (miniGames == null)
        {
            Sprite sp  = Resources.Load<Sprite>("Sprites/tower");

            //Initialize Ingredients
            Ingredient lemon = new Ingredient(0, "Lemon", Resources.Load<Sprite>("Sprites/Ingredients/lemon-active"), Resources.Load<Sprite>("Sprites/Ingredients/lemon-inactive"));
            Ingredient hazelnut = new Ingredient(1, "Hazelnut", Resources.Load<Sprite>("Sprites/Ingredients/hazelnut-active"), Resources.Load<Sprite>("Sprites/Ingredients/hazelnut-inactive"));
            Ingredient flour = new Ingredient(2, "Flour", Resources.Load<Sprite>("Sprites/Ingredients/flour-active"), Resources.Load<Sprite>("Sprites/Ingredients/flour-inactive"));
            
            //Initialize MiniGames
            miniGames = new MiniGame[numberOfGames];
            miniGames[0] = new MiniGame(0, "Towerstacker", "Baue einen Turm mit Läckerli so hoch du kannst", 0, 0, lemon);
            miniGames[1] = new MiniGame(1, "Find Alex", "Hilf Emma Alex zu finden", 0, 2, hazelnut);
            miniGames[2] = new MiniGame(2, "Combine", "Kombiniere richtig", 0, 3, flour);
            Debug.Log("Creating new MiniGame Array");
        }
    }

    public void SaveMiniGame(int id, int highScore, int stars)
    {
        
        if (miniGames != null && id < miniGames.Length && miniGames[id].getHighScore() < highScore)
        {
            //Debug.Log("Stars collected before saving in model minigame: " + miniGames[miniGame.getId()].getStars());
            starsCollected += stars - miniGames[id].getStars();
            //miniGames[miniGame.getId()] = miniGame;
            miniGames[id].setStars(stars);
            miniGames[id].setHighScore(highScore);
           // Debug.Log("Stars collected: " + starsCollected);
           // Debug.Log("Stars collected in new minigame: " + miniGame.getStars());
            //Debug.Log("Stars collected in model minigame: " + miniGames[miniGame.getId()].getStars());
            //Debug.Log("Saved the game!");
        }
        else
        {
            Debug.Log("Can't save!");
        }

        foreach (MiniGame m in miniGames)
        {
            if (m != null)
            {
                Debug.Log(m.getTitle() + m.getStars());
            }
        }
    }

    public bool isGameCompleted(string name)
    {
        bool completed = false;

        foreach (MiniGame game in miniGames)
        {
            if (game.getTitle().Equals(name) && game.isCompleted())
            {
                completed = true;
            }
        }
        return completed;
    }

    public int getCompletedGameCount()
    {
        int counter = 0;

        foreach (MiniGame game in miniGames)
        {
            if (game.isCompleted())
            {
                counter++;
            }
        }
        return counter;
    }

    public (string title, int stars, string status, int highScore) getGameDetail(string name)
    {
        string title = "";
        int stars = 0;
        string status = "";
        int highScore = 0;

        foreach (MiniGame game in miniGames)
        {
            if (game.getTitle().Equals(name))
            {
                title = game.getTitle();
                stars = game.getStars();
                highScore = game.getHighScore();

                if(game.isCompleted()){
                    status = "gefunden";
                }
                else
                {
                    status = "verschollen";
                }
            }
        }

        return (title, stars, status, highScore);
    }

    public (string name, Sprite imageActive, Sprite imageInactive) GetIngredientInfo(int id)
    {
        if(miniGames != null && id < miniGames.Length)
        {
            MiniGame game = miniGames[id];
            string name = game.getIngredientName();
            Sprite imageActive = game.getIngredientImageActive();
            Sprite imageInactive = game.getIngredientImageInactive();
            return (name, imageActive, imageInactive);
        }
        else
        {
            return  (null, null, null);
        }
        
    }
}
