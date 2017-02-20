using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour {

	public float speed = 5.0f;
	private Vector3 endPos;
	public Vector3 prevPos;
	bool turnLeft = false, turnRight = false;
	bool isGameOver = false;

	void Awake() {
		prevPos = transform.position;
	}

	// Use this for initialization
	void Start () {
		endPos  = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isGameOver){
			if (Input.GetKeyUp (KeyCode.RightArrow)) {
				turnRight = true;
			}else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				turnLeft = true;
			}

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
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.transform.tag.Equals("Borders")){
			isGameOver = true;	
		}
	}
}
