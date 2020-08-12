using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MazePlayer : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public int touchesCounter;
	public GameObject nextLevel;

	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;

	private GameObject levelGroup;

	public int activeLevel;
	private bool finished;
	private bool gameOver = false;
    private Vector2 touchStart, touchEnd;
    private GameObject uiController;
	private GameSuccessController gameSuccessController;
	private GameObject arHelpCanvas;
    private Animator swipeAnimator;

	void Start ()
	{
        arHelpCanvas = GameObject.Find("UserInterface").gameObject.transform.Find("ARHelpCanvasTouch").gameObject;
		touchesCounter = activeLevel * 15;
		uiController = GameObject.Find("UserInterface").gameObject.transform.Find("UIControllerGame").gameObject;
        gameSuccessController = uiController.GetComponent<GameSuccessController>();
		swipeAnimator = arHelpCanvas.transform.Find("Swipe").gameObject.GetComponent<Animator>();
		swipeAnimator.SetBool("ShowInfo", true);
		finished = false;
		rb = GetComponent<Rigidbody>();
		TouchCount();
		FindObjectOfType<AudioManager>().Play("music");
		FindObjectOfType<AudioManager>().Play("sounds");
	}

	//https://www.youtube.com/watch?v=gyOOf25321M
	void Update() {
		if (gameOver || finished) return;
		// Swipe start
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
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
			FindObjectOfType<AudioManager>().Play("golf-bat");
		}   
	}


	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		Debug.Log("Collision");
		Debug.Log("Collision with " + other.gameObject);
		if (other.gameObject.CompareTag("Finish"))
		{
			Debug.Log("Collision with finish-tag");
			gameSuccessController.updateProgress(touchesCounter, activeLevel);
			finished = true;
		} else if (other.gameObject.CompareTag("Respawn")) {
			Debug.Log("Collision with respawn-tag");
			touchesCounter = 0;
			gameOver = true;
		}
	}

	private void TouchCount() {
		touchesCounter = touchesCounter > 0 ? touchesCounter - 1 : 0;
		swipeAnimator.SetBool("ShowInfo", false);
        gameSuccessController.updateProgress(touchesCounter, activeLevel-1);
	}

	public bool LevelCompleted()
	{
		return finished;
	}

	public bool LevelLost()
	{
		return gameOver;
	}

}