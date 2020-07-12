using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishCollision : MonoBehaviour
{
    public static int score = 0;
    void OnCollisionEnter(Collision collision)
    {
        score++;
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Läckerli")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here " + score);
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            // Debug.Log("Do something else here " + score);
            // Debug.Log("Do something else here " + collision);
            Debug.Log("Collision with Player " + collision.gameObject.name);
            Debug.Log("Collision with Player " + collision.contactCount);
            // Debug.Log("Do something else here rigidbody " + collision.rigidbody);
            // Debug.Log("Do something else here collider " + collision.collider.transform.position.y.ToString("0.0000"));
            // Debug.Log("Do something else here collision " + collision.transform.position.y.ToString("0.0000"));
            Debug.Log("Collision with Player " + collision.gameObject.transform.position.y.ToString("0.0000"));
            Debug.Log("Collision with Player --------------------------");
        }

        if (collision.gameObject.tag == "Respawn")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            // Debug.Log("Do something else here " + score);
            // Debug.Log("Do something else here " + collision);
            Debug.Log("Collision with Respawn " + collision.gameObject.name);
            Debug.Log("Collision with Respawn " + collision.contactCount);
            // Debug.Log("Do something else here rigidbody " + collision.rigidbody);
            // Debug.Log("Do something else here collider " + collision.collider.transform.position.y.ToString("0.0000"));
            // Debug.Log("Do something else here collision " + collision.transform.position.y.ToString("0.0000"));
            Debug.Log("Collision with Respawn " + collision.gameObject.transform.position.y.ToString("0.0000"));
            Debug.Log("Collision with Respawn --------------------------");
        }
    }
}
