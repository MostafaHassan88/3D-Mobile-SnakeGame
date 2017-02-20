using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour {

	public float speed = 5.0f;
	private Vector3 endPos;
	public Vector3 prevPos;
	bool turnLeft = false, turnRight = false;
	Vector2 firstPressPos, secondPressPos;

	void Awake() {
		prevPos = transform.position;
	}

	// Use this for initialization
	void Start () {
		endPos  = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Swipe ();
	}

	public void handleHeadMovement(){
		if (transform.position == endPos) {
			prevPos = endPos;
			if (turnLeft) {
				turnLeft = false;
				transform.Rotate (Vector3.up, -90);
			} else if(turnRight){
				turnRight = false;
				transform.Rotate (Vector3.up, 90);
			}
			endPos = transform.position + (transform.rotation * Vector3.forward * 1);
		}

		transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * (speed+(transform.parent.childCount/10)));	
	}
		
	// this method check if the player press right or left arrow and check if he swiped left or right using mouse or touch
	public void Swipe()
	{
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			turnRight = true;
		} else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			turnLeft = true;
		}

		if(Input.GetMouseButtonDown(0))
		{
			//save began touch 2d point
			firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		}

		if(Input.GetMouseButtonUp(0))
		{
			//save ended touch 2d point
			secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

			//create vector from the two points
			Vector2 currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

			//normalize the 2d vector
			currentSwipe.Normalize();

			//swipe left
			if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
			{
				Debug.Log("left swipe");
				turnLeft = true;
			}
			//swipe right
			if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
			{
				Debug.Log("right swipe");
				turnRight = true;
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.transform.tag.Equals("Borders")){
			transform.parent.GetComponent<SnakeManager> ().doGameOver ();	
		}
	}
}
