using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeManager : MonoBehaviour {

	public List<SnakeBodyPart> snakeBodyParts = new List<SnakeBodyPart>();
	public bool isGameOver = false;
	public GameObject gameOver;
	public GameObject bodyPartPrefab;
	public Text txtScore;
	// spawn objects from -4.5 to 4.5

	// Use this for initialization
	void Start () {
		InvokeRepeating ("moveTheSnake", 0, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// called when ever the snake hit the wall to activate the gameOver Screen
	public void doGameOver(){
		gameOver.SetActive (true);
		isGameOver = true;
	}

	public void snakeAteFruit(int fruitScore){
		txtScore.text = (int.Parse (txtScore.text) + fruitScore).ToString();
	}

	public void addSnakeBodyPart(){
		Transform bodyPart = Instantiate (bodyPartPrefab, transform.GetChild(transform.childCount -1).position, transform.GetChild(transform.childCount -1).rotation).transform;
		bodyPart.GetComponent<SnakeBodyPart> ().HeadPart = transform.GetChild (0);
		bodyPart.SetParent (transform);
	}

	private void moveTheSnake(){
		if (isGameOver) {
			CancelInvoke ("moveTheSnake");
		} else {
			foreach (SnakeBodyPart bPart in snakeBodyParts) {
				bPart.handleBodyPartMove ();
			}
		}
	}

}
