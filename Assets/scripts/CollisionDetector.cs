using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public static bool floorCollided = false;
    public static bool cookieCollided = false;

    // used to check, if cookie of laeckerli stacking game hasn't touched the floor
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.name == "LäckerliFloor") 
        {
            floorCollided = true;
        } 
        else if (collision.gameObject.name.Contains("laeckerli")) 
        {
            cookieCollided = true;
            Debug.Log("Läckerli: " + collision);
            Debug.Log("Läckerli: " + collision.relativeVelocity);
        }
    }
}
