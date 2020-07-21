using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public static bool floorCollided = false;
    public static bool cookieCollided = false;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.name == "LäckerliFloor") {
            floorCollided = true;
        } else if (collision.gameObject.name.Contains("Läckerli")) {
            if (collision.relativeVelocity.y < -0.3 ) {
                cookieCollided = true;
            }
            Debug.Log("Läckerli: " + collision);
            Debug.Log("Läckerli: " + collision.relativeVelocity);
        }
    }
}
