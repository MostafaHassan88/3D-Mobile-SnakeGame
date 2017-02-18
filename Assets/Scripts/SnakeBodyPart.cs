using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakeBodyPart : MonoBehaviour {

	public Transform HeadPart, prevBodyPart;
	List<turnData> turnPositions = new List<turnData>();
	public float movementSpeed = 0.01f;

	// Use this for initialization
	void Start () {
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
			if (turnPositions.Count > 0 && V3Equal (transform.position, turnPositions [0].getTurnPos ())) {
				transform.Rotate (Vector3.up, turnPositions [0].getDirecKey () == KeyCode.LeftArrow ? -90 : 90);
				if (transform.eulerAngles.y == 90 || transform.eulerAngles.y == 270) {
					transform.position = new Vector3 (prevBodyPart.position.x - 125, transform.position.y, turnPositions [0].getTurnPos ().z);
				} else {
					transform.position = new Vector3 (turnPositions [0].getTurnPos ().x, transform.position.y, prevBodyPart.position.x - 125);
				}
				turnPositions.RemoveAt (0);
			}


			transform.position += (transform.rotation * Vector3.forward * movementSpeed);	
	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.Distance(a , b) < 3;
	}

	// store the data related to when should the body part take a different direction
	private class turnData {
		Vector3 turnPos;
		KeyCode direcKey;

		public turnData(Vector3 tPos, KeyCode pressedKey){
			turnPos = tPos;
			direcKey = pressedKey;
		}

		public Vector3 getTurnPos(){
			return turnPos;
		}

		public KeyCode getDirecKey(){
			return direcKey;
		}
	}
}
