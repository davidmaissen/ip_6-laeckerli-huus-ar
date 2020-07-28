using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame
{
    private int id;
    private string title;
    private string description;
    private int highScore;
    private int stars;
    private Ingredient ingredient;

    public MiniGame (int id, string title, string description, int highScore, int stars, Ingredient ingredient) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.highScore = highScore;
        this.stars = stars;
        this.ingredient = ingredient;
        Debug.Log("Created Minigame: " + title);
    }

    public int getId()
    {
        return id;
    }

    public string getTitle()
    {
        return title;
    }

    public string getDescription()
    {
        return description;
    }

    public int getHighScore()
    {
        return highScore;
    }

    public int getStars()
    {
        return stars;
    }

    public string getIngredientName()
    {
        return ingredient.GetName();
    }

    public Sprite getIngredientImageActive()
    {
        return ingredient.GetImageActive();
    }

    public Sprite getIngredientImageInactive()
    {
        return ingredient.GetImageInactive();
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public void setTitle(string title)
    {
        this.title = title;
    }

    public void setDescription(string description)
    {
        this.description = description;
    }

    public void setHighScore(int highScore)
    {
        this.highScore = highScore;
    }

    public void setStars(int stars)
    {
        this.stars = stars;
    }

    public bool isCompleted()
    {
        return (stars > 0);
    }




}
