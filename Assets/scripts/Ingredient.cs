﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient
{
    private int id;
    private string name;
    private Sprite imageActive;

    private Sprite imageInactive;


    public Ingredient (int id, string name, Sprite imageActive, Sprite imageInactive ) {
        this.id = id;
        this.name = name;
        this.imageActive = imageActive;
        this.imageInactive = imageInactive;
    }

    public int GetId()
    {
        return id;
    }

    public string GetName()
    {
        return name;
    }

    public Sprite GetImageActive()
    {
        return imageActive;
    }

    public Sprite GetImageInactive()
    {
        return imageInactive;
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public void SetName(string title)
    {
        this.name = name;
    }


}