using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress: MonoBehaviour
{
    public static MiniGame[] miniGames;
    public static int numberOfGames = 3;
    public static int starsCollected = 0;
    private void Awake() {
        if (miniGames == null) {
            miniGames = new MiniGame[numberOfGames];
            miniGames[0] = new MiniGame(0, "Towerstacker", "Baue einen Turm mit Läckerli so hoch du kannst", 0, 0);
            miniGames[1] = new MiniGame(1, "Find Alex", "Hilf Emma Alex zu suchen", 0, 0);
            miniGames[2] = new MiniGame(2, "Combine", "Kombiniere richtig", 0, 0);
            Debug.Log("Creating new MiniGame Array");
        }
    }

    /*
    public void Initialize() {
        if (miniGames == null) {
            miniGames = new MiniGame[numberOfGames];
            miniGames[0] = new MiniGame(0, "Towerstacker", "Baue einen Turm mit Läckerli so hoch du kannst", 0, 0);
            miniGames[1] = new MiniGame(1, "Find Alex", "Hilf Emma Alex zu suchen", 0, 0);
            miniGames[2] = new MiniGame(2, "Combine", "Kombiniere richtig", 0, 0);
            Debug.Log("Creating new MiniGame Array");
        }
    }
    */

    public void SaveMiniGame (MiniGame miniGame) {
        if (miniGames != null && miniGame.id < miniGames.Length && miniGames[miniGame.id].highScore < miniGame.highScore) {
            miniGames[miniGame.id] = miniGame;
            starsCollected += miniGame.stars - miniGames[miniGame.id].stars;
            Debug.Log("Saved the game!");
        } else {
            Debug.Log("Can't save!");
        }

        foreach (MiniGame m in miniGames) {
            if (m != null) {
                Debug.Log(m.title + m.stars);
            }
        }
    }
}
