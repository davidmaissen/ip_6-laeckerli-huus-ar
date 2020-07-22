using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorCombination : MonoBehaviour
{
    private void Update() {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo)) {
            if (hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2 < 0) {
                Debug.Log("Collision without Rigidbody down " + this.name + " with: " + 
                hitInfo.transform.gameObject.name);
            }
        }
        if (Physics.Raycast(transform.position, Vector3.up, out hitInfo)) {
            if (hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2 < 0) {
                Debug.Log("Collision without Rigidbody up " + this.name + " with: " + 
                hitInfo.transform.gameObject.name);
            }
        }
        if (Physics.Raycast(transform.position, Vector3.right, out hitInfo)) {
            if (hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2 < 0) {
                Debug.Log("Collision without Rigidbody right " + this.name + " with: " + 
                hitInfo.transform.gameObject.name);
            }
        }
        if (Physics.Raycast(transform.position, Vector3.left, out hitInfo)) {
            if (hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2 < 0) {
                Debug.Log("Collision without Rigidbody left " + this.name + " with: " + 
                hitInfo.transform.gameObject.name);
            }
        }
    }
}
