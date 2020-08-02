﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    public static bool floorCollided = false;
    public static bool cookieCollided = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "LaeckerliFloor") {
            floorCollided = true;
        } else if (collision.gameObject.name.Contains("Laeckerli")) {
            if (collision.relativeVelocity.y < -0.3 ) {
                cookieCollided = true;
            }
            Debug.Log("Läckerli: " + collision);
            Debug.Log("Läckerli: " + collision.relativeVelocity);
        }
    }

}