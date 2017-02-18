using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakeBodyPart : MonoBehaviour {

	public Transform HeadPart, prevBodyPart;
	public List<turnData> turnPositions = new List<turnData>();
	public float movementSpeed = 0.01f;
	public float minDistance = 1.1f;

	// Use this for initialization
	void Start () {
		HeadPart = transform.parent.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
	 	if (Input.GetKeyUp (KeyCode.RightArrow)) {
			turnPositions.Add (new turnData(HeadPart.position, KeyCode.RightArrow));
		}else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			turnPositions.Add (new turnData(HeadPart.position, KeyCode.LeftArrow));
		}
	}

	public void handleBodyPartMove(){
		// if prevBodyPart is null then this is the head 
		if (prevBodyPart == null) {
			handleSnakeHeadMovement ();
		} else {
			handleBodyPartMovement ();
		}
	}
		
	// the head always move forward and is reponsible for the turns and the rest of the body follows 
	private void handleSnakeHeadMovement(){
		if (turnPositions.Count > 0 && V3Equal (transform.position, turnPositions [0].getTurnPos ())) {
			transform.Rotate (Vector3.up, turnPositions [0].getDirecKey () == KeyCode.LeftArrow ? -90 : 90);
			if (transform.eulerAngles.y == 90 || transform.eulerAngles.y == 270) {
				transform.position = new Vector3 (transform.position.x, transform.position.y, turnPositions [0].getTurnPos ().z);
			} else {
				transform.position = new Vector3 (turnPositions [0].getTurnPos ().x, transform.position.y, transform.position.z);
			}
			turnPositions.RemoveAt (0);
		}

		transform.position += (transform.rotation * Vector3.forward * movementSpeed);
	}

	// this method is reponsible for bodyPart to follow each other keeping the minDistance set between them
	private void handleBodyPartMovement(){
		float dis = Vector3.Distance (prevBodyPart.position, transform.position);

		Vector3 newPos = prevBodyPart.position;
		newPos.y = HeadPart.position.y;

		float T = dis / minDistance * movementSpeed;

		transform.position = Vector3.Lerp (transform.position, newPos, T);
		transform.rotation = Quaternion.Slerp (transform.rotation, prevBodyPart.rotation, T);
	}

	// check the distance bewteen two vecotrs to see if they are considered equal
	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.Distance(a , b) < movementSpeed;
	}

	// check if the head hit one of the snake body parts
	void OnTriggerEnter(Collider col)
	{
		if(col.transform.tag.Equals("snakeHead")){
			transform.parent.GetComponent<SnakeManager> ().doGameOver();	
		}
	}
}
