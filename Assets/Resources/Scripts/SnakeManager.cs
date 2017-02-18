using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SnakeManager : MonoBehaviour {

	public List<SnakeBodyPart> snakeBodyParts = new List<SnakeBodyPart>();
	public bool isGameOver = false;
	public GameObject gameOver;
	public GameObject bodyPartPrefab;
	public Text txtScore;

	List<Vector3> fruitSpawnPositions = new List<Vector3>();

	// Use this for initialization
	void Start () {
		InvokeRepeating ("moveTheSnake", 0, 0.01f);
		setFruitSpawnPositions ();
		spawnFruit ();
	}

	private void setFruitSpawnPositions(){
		for (int x = -18; x < 19; x++) {
			for (int z = -18; z < 19; z++) {
				fruitSpawnPositions.Add (new Vector3 (x, 0.2f, z));
			}
		} 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// called when ever the snake hit the wall to activate the gameOver Screen
	public void doGameOver(){
		gameOver.SetActive (true);
		isGameOver = true;
	}

	private void spawnFruit(){
		List<Vector3> copyPos = new List<Vector3> (fruitSpawnPositions);
		for (int i = 0; i < transform.childCount; i++) {
			copyPos.RemoveAll (Pos=> Vector3.Distance(Pos , transform.position) < 1);
		}
			
		int randomChance = UnityEngine.Random.Range (0, 10);
		GameObject fruitObj = Instantiate(Resources.Load<GameObject>("Prefabs/Apple")); //("Prefabs/"+((randomChance == 9) ? "Apple": "Pear")));
		fruitObj.GetComponent<FruitHandler> ().sManager = this;
		fruitObj.transform.position = copyPos [UnityEngine.Random.Range (0, copyPos.Count)];

	}

	// when the snake eats a fruit we add the score and spawn a new fruit and a new snake body part
	public void snakeAteFruit(int fruitScore){
		txtScore.text = (int.Parse (txtScore.text) + fruitScore).ToString();
		spawnFruit ();
		addSnakeBodyPart ();
	}

	// create a new snake body part and add a reference to the part before it 
	public void addSnakeBodyPart(){
		Transform prevPart = snakeBodyParts [snakeBodyParts.Count - 1].transform;

		SnakeBodyPart bodyPart = Instantiate (bodyPartPrefab, prevPart.position, prevPart.rotation).GetComponent<SnakeBodyPart> ();
		bodyPart.transform.SetParent (transform);
		bodyPart.name = "bodyPart" + transform.childCount;
		bodyPart.prevBodyPart = prevPart;
		snakeBodyParts.Add (bodyPart);
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
