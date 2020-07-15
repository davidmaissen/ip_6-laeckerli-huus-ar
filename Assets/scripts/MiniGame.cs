using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame
{
    public int id;
    public string title;
    public string description;
    public int highScore;
    public int stars;

    public MiniGame (int id, string title, string description, int highScore, int stars) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.highScore = highScore;
        this.stars = stars;
        Debug.Log("Created Minigame: " + title);
    }
}
