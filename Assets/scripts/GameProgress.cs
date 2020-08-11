using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress
{
    public static MiniGame[] miniGames;
    public static int numberOfGames = 4;
    public static int starsCollected = 0;
    public static bool tutorialCompleted = false;
    public static string playerName;

    public void InitializeGameData()
    {
        if (miniGames == null)
        {
            Sprite sp  = Resources.Load<Sprite>("Sprites/tower");

            //Initialize Ingredients
            Ingredient lemon = new Ingredient(0, "Lemon", Resources.Load<Sprite>("Sprites/Ingredients/lemon-active"), Resources.Load<Sprite>("Sprites/Ingredients/lemon-inactive"), Resources.Load<Material>("Materials/Success/lemon"));
            Ingredient hazelnut = new Ingredient(1, "Hazelnut", Resources.Load<Sprite>("Sprites/Ingredients/hazelnut-active"), Resources.Load<Sprite>("Sprites/Ingredients/hazelnut-inactive"), Resources.Load<Material>("Materials/Success/hazelnut"));
            Ingredient flour = new Ingredient(2, "Flour", Resources.Load<Sprite>("Sprites/Ingredients/flour-active"), Resources.Load<Sprite>("Sprites/Ingredients/flour-inactive"), Resources.Load<Material>("Materials/Success/flour"));
            Ingredient honey = new Ingredient(3, "Honey", Resources.Load<Sprite>("Sprites/Ingredients/honey-active"), Resources.Load<Sprite>("Sprites/Ingredients/honey-inactive"), Resources.Load<Material>("Materials/Success/honey"));
            
            //Initialize MiniGames
            miniGames = new MiniGame[numberOfGames];
            miniGames[0] = new MiniGame(0, "laeckerli-tower", "Läckerli Turm", "Baue einen Turm mit Läckerli indem du sie aufeinander stapelst.", 1, 2, lemon);
            miniGames[1] = new MiniGame(1, "find-alex", "Finde Alex", "Hilf Emma Alex zu finden. Du kannst zwei zusätzliche Sterne verdienen, wenn du genau herumschaust und auch den anderen hilfst.", 1, 1, flour);
            miniGames[2] = new MiniGame(2, "combination", "Kombiniere richtig", "Setze die einzelnen Stücke zusammen, indem du die Karten richtig drehst und zusammensetzt.", 1, 1, hazelnut);
            miniGames[3] = new MiniGame(3, "maze", "Küchen Minigolf", "Spiele Minigolf in der Küche von Alex.", 1, 1, honey);
            Debug.Log("Creating new MiniGame Array");
        }
    }

    public void SaveMiniGame(int id, int highScore, int stars)
    {
        
        if (miniGames != null && id < miniGames.Length && (miniGames[id].getHighScore() < highScore || miniGames[id].getStars() < stars))
        {
            starsCollected += stars - miniGames[id].getStars();
            miniGames[id].setStars(stars);
            miniGames[id].setHighScore(highScore);
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

    public int GetGameCount()
    {
        return GameProgress.miniGames.Length;
    }

    public bool isGameCompleted(int gameID)
    {
        bool completed = false;

        if (miniGames != null && gameID < miniGames.Length)
        {
            return miniGames[gameID].isCompleted();
        }
        else
        {
            return false;
        }
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

    public (string title, int stars, string status, int highScore) getGameDetail(int gameID)
    {
        string title = "";
        int stars = 0;
        string status = "";
        int highScore = 0;


        if(miniGames != null && gameID < miniGames.Length)
        {
           MiniGame game = miniGames[gameID];
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

        return (title, stars, status, highScore);
    }

    public (string name, Sprite imageActive, Sprite imageInactive) GetIngredientInfo(int gameID)
    {
        if(miniGames != null && gameID < miniGames.Length)
        {
            MiniGame game = miniGames[gameID];
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

    public Sprite GetIngredientIcon(int gameID, bool checkState)
    {
        if(miniGames != null && gameID < miniGames.Length)
        {
            MiniGame game = miniGames[gameID];

            if(game.isCompleted() || !checkState)
            {
                return game.getIngredientImageActive();
            }
            else
            {
                return game.getIngredientImageInactive();
            }
        }
        else
        {
            return  null;
        }
        
    }

    public Material GetMaterial(int gameID) {
        return miniGames[gameID].GetIngredientMaterial();
    }

    public bool checkifStoryCompleted()
    {

        int i = 0;
        while(i < miniGames.Length && miniGames[i].isCompleted())
        {
            i++;
        }

        return i == miniGames.Length;
    }

    public void setPlayerName(string name)
    {
        playerName = name;
    }
}
