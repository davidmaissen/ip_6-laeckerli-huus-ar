using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorCombination : MonoBehaviour
{
    public GameObject collision;
    private CombinationController combinationController;

    private void Awake() {
        combinationController = GameObject.FindObjectOfType<CombinationController>();
    }
    
    private void Update() {
        if (this.name.Contains("bowl")) {
            BowlCollision();
        } else if (this.name.Contains("whisk")) {
            WhiskCollision();
        }
    }

    private void BowlCollision() {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, -transform.forward, out hitInfo)) {
            if (!(this.name == hitInfo.transform.gameObject.name) && hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2 < 0) {
                Debug.Log("Collision without Rigidbody down " + this.name + " with: " + 
                hitInfo.transform.gameObject.name);
                Instantiate(collision, hitInfo.point, hitInfo.transform.rotation);
                
                if (this.name == "bowl-topright" && hitInfo.transform.gameObject.name == "bowl-bottomright") {
                    Debug.Log(this.name + " is placed right");
                    combinationController.UpdateCombination(1, this.name);
                }
                if (this.name == "bowl-topleft" && hitInfo.transform.gameObject.name == "bowl-bottomleft") {
                    Debug.Log(this.name + " is placed right");
                    combinationController.UpdateCombination(3, this.name);
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo)) {
            if (!(this.name == hitInfo.transform.gameObject.name) && hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2 < 0) {
                Debug.Log("Collision without Rigidbody up " + this.name + " with: " + 
                hitInfo.transform.gameObject.name);
                Instantiate(collision, hitInfo.point, hitInfo.transform.rotation);
                if (this.name == "bowl-bottomright" && hitInfo.transform.gameObject.name == "bowl-topright") {
                    Debug.Log(this.name + " is placed right");
                    combinationController.UpdateCombination(1, this.name);
                }
                if (this.name == "bowl-bottomleft" && hitInfo.transform.gameObject.name == "bowl-topleft") {
                    Debug.Log(this.name + " is placed right");
                    combinationController.UpdateCombination(3, this.name);
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.right, out hitInfo)) {
            if (!(this.name == hitInfo.transform.gameObject.name) && hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2 < 0) {
                Debug.Log("Collision without Rigidbody right " + this.name + " with: " + 
                hitInfo.transform.gameObject.name);
                GameObject go = Instantiate(collision, hitInfo.point, hitInfo.transform.rotation);
                go.transform.Rotate(0,0,90);

                if (this.name == "bowl-topleft" && hitInfo.transform.gameObject.name == "bowl-topright") {
                    Debug.Log(this.name + " is placed right");
                    combinationController.UpdateCombination(0, this.name);
                }
                if (this.name == "bowl-bottomleft" && hitInfo.transform.gameObject.name == "bowl-bottomright") {
                    Debug.Log(this.name + " is placed right");
                    combinationController.UpdateCombination(2, this.name);
                }
            }
        }
        if (Physics.Raycast(transform.position, -transform.right, out hitInfo)) {
            if (!(this.name == hitInfo.transform.gameObject.name) && hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2 < 0) {
                Debug.Log("Collision without Rigidbody left " + this.name + " with: " + 
                hitInfo.transform.gameObject.name);
                GameObject go = Instantiate(collision, hitInfo.point, hitInfo.transform.rotation);
                go.transform.Rotate(0,0,90);
                if (this.name == "bowl-bottomright" && hitInfo.transform.gameObject.name == "bowl-bottomleft") {
                    Debug.Log(this.name + " is placed right");
                    combinationController.UpdateCombination(2, this.name);
                }
                if (this.name == "bowl-topright" && hitInfo.transform.gameObject.name == "bowl-topleft") {
                    Debug.Log(this.name + " is placed right");
                    combinationController.UpdateCombination(0, this.name);
                }
            }
        }
    }

    private void WhiskCollision() {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, -transform.right, out hitInfo)) {
            if (!(this.name == hitInfo.transform.gameObject.name) && hitInfo.distance - this.GetComponent<Collider>().bounds.size.z/2 < 0) {
                Debug.Log("Collision without Rigidbody left " + this.name + " with: " + 
                hitInfo.transform.gameObject.name);
                GameObject go = Instantiate(collision, hitInfo.point, hitInfo.transform.rotation);
                go.transform.Rotate(0,0,90);
                if (this.name == "whisk-handle" && hitInfo.transform.gameObject.name == "whisk-wires"
                || this.name == "whisk-wires" && hitInfo.transform.gameObject.name == "whisk-handle") {
                    Debug.Log(this.name + " is placed right");
                    combinationController.UpdateCombination(0, this.name);
                }
            }
        }

    }
}
