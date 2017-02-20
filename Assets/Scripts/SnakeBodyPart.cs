using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakeBodyPart : MonoBehaviour {

	public float speed = 5.0f;
	public GameObject prevPart;
	private Vector3 endPos;
	public Vector3 prevPos;

	void Awake() {
		prevPos = transform.position;
	}

	void Start () {
		getEndPos ();
	}

	void Update () {
		if (transform.position == endPos) {
			prevPos = endPos;
			getEndPos();
		}
		transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * (speed+(transform.parent.childCount/10)));
	}

	void getEndPos(){
		if(prevPart.GetComponent<SnakeHead>() != null){
			endPos = prevPart.GetComponent<SnakeHead> ().prevPos;
		}else{
			endPos = prevPart.GetComponent<SnakeBodyPart> ().prevPos;
		}
	}

	// check if the head hit one of the snake body parts
	void OnTriggerEnter(Collider col)
	{
		if(col.transform.tag.Equals("snakeHead")){
			transform.parent.GetComponent<SnakeManager> ().doGameOver();	
		}
	}
}
