using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;
using TMPro;

public class MazePlayer : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public int touchesCounter = 0;
    // public TextMeshProUGUI touches;
	//public Text winText;

	public GameObject nextLevel;

	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;

	private GameObject levelGroup;

	// private GameObject[] levels;

	public int activeLevel;
	private bool finished;
    private Vector2 touchStart, touchEnd;
    private GameObject uiController;
	private GameSuccessController gameSuccessController;
	private GameObject arHelpCanvas;
    private Animator swipeAnimator;


	//private GameObject activeLevel;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		//rb = GetComponent<Rigidbody>();
        arHelpCanvas = GameObject.Find("UserInterface").gameObject.transform.Find("ARHelpCanvasTouch").gameObject;
		// arHelpCanvas.SetActive(true);
		//arHelpCanvas.gameObject.transform.Find("Swipe").gameObject.SetActive(true);
		touchesCounter = activeLevel * 15;
		uiController = GameObject.Find("UserInterface").gameObject.transform.Find("UIControllerGame").gameObject;
        gameSuccessController = uiController.GetComponent<GameSuccessController>();
		gameSuccessController.updateProgress(touchesCounter, activeLevel-1);
		swipeAnimator = arHelpCanvas.transform.Find("Swipe").gameObject.GetComponent<Animator>();
		swipeAnimator.SetBool("ShowInfo", true);

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
		// swipeAnimator.SetBool("ShowInfo", false);
    	touchStart = Input.GetTouch(0).position;
		TouchCount();
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

	private void TouchCount() {
		touchesCounter--;
		swipeAnimator.SetBool("ShowInfo", false);
        gameSuccessController.updateProgress(touchesCounter, activeLevel-1);
	}


	public bool LevelCompleted()
	{
		return finished;
	}

}