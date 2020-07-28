using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination
{
    public string name;
    public bool animationPlayed;
    public Vector3 position;
    public Quaternion rotation;

    public Combination(string name, Vector3 position, Quaternion rotation) {
        this.name = name;
        this.position = position;
        this.rotation = rotation;
        this.animationPlayed = false;
    }
}
