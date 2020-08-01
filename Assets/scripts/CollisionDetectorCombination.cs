using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectorCombination : MonoBehaviour
{
    public GameObject collision;
    private CombinationController combinationController;
    private Combination combination;
    public string puzzleName;
    private bool bowlSuccessPlayed;
    private bool whiskSuccessPlayed;
    private bool rollingPinSuccessPlayed;

    private void Awake() {
        combinationController = GameObject.FindObjectOfType<CombinationController>();
        if (puzzleName == "bowl") {
            combination = new Combination("bowl", Vector3.forward, Quaternion.identity);
        } else if (puzzleName == "whisk") {
            combination = new Combination("whisk", Vector3.forward, Quaternion.identity);
        } else if (puzzleName == "rolling-pin") {
            combination = new Combination("rolling-pin", Vector3.forward, Quaternion.identity);
        }
    }
    
    private void Update() {
        if (puzzleName == "bowl" && !bowlSuccessPlayed) {
            if (combinationController.IsPuzzleCompleted(puzzleName) && this.name == "bowl-topleft") {
                Combination completed = combination;
                Debug.Log(completed);
                combinationController.PlayAnimation(completed.name, completed.position, completed.rotation);
                bowlSuccessPlayed = true;
            } else {
                BowlCollision();
            }
        } else if (puzzleName == "whisk" && !whiskSuccessPlayed) {
            if (combinationController.IsPuzzleCompleted(puzzleName) && this.name == "whisk-handle") {
                Combination completed = combination;
                Debug.Log(completed);
                combinationController.PlayAnimation(completed.name, completed.position, completed.rotation);
                whiskSuccessPlayed = true;
            } else {
                WhiskCollision();
            }
        } else if (puzzleName == "rolling-pin" && !rollingPinSuccessPlayed && this.name == "rolling-pin-right") {
            if (combinationController.IsPuzzleCompleted(puzzleName)) {
                Combination completed = combination;
                Debug.Log(completed);
                combinationController.PlayAnimation(completed.name, completed.position, completed.rotation);
                rollingPinSuccessPlayed = true;
            } else {
                RollingPinCollision();
            }
        }
    }

    private void BowlCollision() {
        RaycastHit hitInfo;
        Combination completed = combination;
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
                GameObject go = Instantiate(collision, hitInfo.transform.position, hitInfo.transform.rotation);
                go.transform.Rotate(0,0,90);

                if (this.name == "bowl-topleft" && hitInfo.transform.gameObject.name == "bowl-topright") {
                    Debug.Log(this.name + " is placed right");
                    completed.position = this.transform.position;
                    completed.position.x += this.GetComponent<Collider>().bounds.size.x/8;
                    completed.position.y -= this.GetComponent<Collider>().bounds.size.y/2;
                    completed.rotation = this.transform.rotation;
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
        Combination completed = combination;
        if (Physics.Raycast(transform.position, -transform.right, out hitInfo)) {
                Debug.Log("Collision without Rigidbody left " + this.name + (hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2) +
                this.GetComponent<Collider>().bounds.size);
            if (!(this.name == hitInfo.transform.gameObject.name) && hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2 < 0) {
                Debug.Log("Collision without Rigidbody left " + this.name + " with: " + 
                hitInfo.transform.gameObject.name);
                GameObject go = Instantiate(collision, hitInfo.point, hitInfo.transform.rotation);
                go.transform.Rotate(0,0,90);
                if (this.name == "whisk-handle" && hitInfo.transform.gameObject.name == "whisk-wires") {
                    Debug.Log(this.name + " is placed right");
                    completed.position = this.transform.position;
                    //completed.position.x -= this.GetComponent<Collider>().bounds.size.x/2;
                    completed.rotation = this.transform.rotation;
                    completed.rotation *= Quaternion.Euler(0, 90, 0);
                    combinationController.UpdateCombination(0, this.name);
                }
                if (this.name == "whisk-wires" && hitInfo.transform.gameObject.name == "whisk-handle") {
                    Debug.Log(this.name + " is placed right");
                    combinationController.UpdateCombination(1, this.name);
                }
            }
        }
    }

    private void RollingPinCollision() {
        RaycastHit hitInfo;
        Combination completed = combination;
        if (Physics.Raycast(transform.position, -transform.forward, out hitInfo)) {
            if (!(this.name == hitInfo.transform.gameObject.name) && hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2 < 0) {
                Debug.Log("Collision without Rigidbody down " + this.name);
                if (this.name == "rolling-pin-right" && hitInfo.transform.gameObject.name == "rolling-pin-left") {
                    GameObject go = Instantiate(collision, hitInfo.point, hitInfo.transform.rotation);
                    go.transform.Rotate(0,0,90);
                    Debug.Log(this.name + " is placed right");
                    completed.position = this.transform.position;
                    completed.rotation = this.transform.rotation;
                    completed.position.x += this.GetComponent<Collider>().bounds.size.x/8;
                    completed.rotation *= Quaternion.Euler(0, 90, 0);
                    combinationController.UpdateCombination(0, this.name);
                }
            }
        }
        if (Physics.Raycast(transform.position, -transform.right, out hitInfo)) {
            if (!(this.name == hitInfo.transform.gameObject.name) && hitInfo.distance - this.GetComponent<Collider>().bounds.size.x/2 < 0) {
                Debug.Log("Collision without Rigidbody left " + this.name);
                if (this.name == "rolling-pin-left" && hitInfo.transform.gameObject.name == "rolling-pin-right") {
                    GameObject go = Instantiate(collision, hitInfo.point, hitInfo.transform.rotation);
                    go.transform.Rotate(0,0,90);
                    Debug.Log(this.name + " is placed right");
                    combinationController.UpdateCombination(0, this.name);
                }
            }
        }
    }

}
