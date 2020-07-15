using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public static bool collided = false;
    void OnCollisionEnter(Collision collision)
    {
        collided = true;
        Debug.Log("Collision with " + collision.gameObject.name);
    }
}
