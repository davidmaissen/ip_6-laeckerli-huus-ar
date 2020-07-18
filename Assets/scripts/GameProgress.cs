using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress
{
    public static MiniGame[] miniGames;
    public static int numberOfGames = 3;
    public static int starsCollected = 0;
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
    
    public void InitializeGameData() {
        if (miniGames == null) {
            miniGames = new MiniGame[numberOfGames];
            miniGames[0] = new MiniGame(0, "Towerstacker", "Baue einen Turm mit Läckerli so hoch du kannst", 0, 0);
            miniGames[1] = new MiniGame(1, "Find Alex", "Hilf Emma Alex zu suchen", 0, 0);
            miniGames[2] = new MiniGame(2, "Combine", "Kombiniere richtig", 0, 0);
            Debug.Log("Creating new MiniGame Array");
        }
    }

    public void SaveMiniGame (MiniGame miniGame) {
        if (miniGames != null && miniGame.getId() < miniGames.Length && miniGames[miniGame.getId()].getHighScore() < miniGame.getHighScore()) {
            Debug.Log("Stars collected before saving in model minigame: " +  miniGames[miniGame.getId()].getStars());
            starsCollected += miniGame.getStars() - miniGames[miniGame.getId()].getStars();
            miniGames[miniGame.getId()] = miniGame;
            Debug.Log("Stars collected: " + starsCollected);
            Debug.Log("Stars collected in new minigame: " + miniGame.getStars());
            Debug.Log("Stars collected in model minigame: " +  miniGames[miniGame.getId()].getStars());
            Debug.Log("Saved the game!");
        } else {
            Debug.Log("Can't save!");
        }

        foreach (MiniGame m in miniGames) {
            if (m != null) {
                Debug.Log(m.getTitle() + m.getStars());
            }
        }
    }

    public bool isGameCompleted(string name)
    {
        bool completed = false;

        foreach (MiniGame game in miniGames){
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
}
