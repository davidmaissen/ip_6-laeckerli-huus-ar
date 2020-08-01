using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class MazePlayer : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	//public Text countText;
	//public Text winText;

	public GameObject nextLevel;

	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;

	private GameObject levelGroup;

	private GameObject[] levels;

	//private int levelCount;
	private bool finished;
    private Vector2 touchStart, touchEnd; 

	//private GameObject activeLevel;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		//rb = GetComponent<Rigidbody>();

		

		//levelGroup = GameObject.FindGameObjectWithTag("Level");
		//levelCount = levelGroup.transform.childCount;
		finished = false;


/*         for (int i = 0; i < levelGroup.transform.childCount; i++)
        {   
            if (i < 1)
            {
                levelGroup.transform.GetChild(i).gameObject.SetActive(true);
				activeLevel = levelGroup.transform.GetChild(i).gameObject;
            }
            else
            {
                levelGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
        } */

		rb = GetComponent<Rigidbody>();
		Debug.Log("Kugel: " + rb.name);

	}

	// Each physics step..

//https://www.youtube.com/watch?v=gyOOf25321M
void Update() {
   // Swipe start
   if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
     touchStart = Input.GetTouch(0).position;
   }
   // Swipe end
   if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) {
     touchEnd = Input.GetTouch(0).position;
     float cameraFacing = Camera.main.transform.eulerAngles.y;
     Vector2 swipeVector = touchEnd - touchStart;
     Vector3 inputVector = new Vector3(swipeVector.x, 0.0f, swipeVector.y);
     Vector3 movement = Quaternion.Euler(0.0f, cameraFacing, 0.0f) * Vector3.Normalize(inputVector);
     rb.velocity = movement;
   }   
 }


	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		Debug.Log("Collision");
		if (other.gameObject.CompareTag ("Finish"))
		{
			Debug.Log("Collision with target");
		//	Debug.Log("Finish");
			finished = true;
		}
	}



	public bool levelStatus()
	{
		return finished;
	}

}