using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SnakeManager : MonoBehaviour {
	bool isGameOver = false;
	public GameObject gameOver, highScore;
	public PauseMenu pauseMenu;
	public GameObject bodyPartPrefab;
	public Text txtScore;
	public ObstacleManager oManager;
	AudioManager audioManager;
	List<Vector3> fruitSpawnPositions = new List<Vector3>();

	// Use this for initialization
	void Start () {
		audioManager = AudioManager.Instance;
//		setMusicTransform ();
		setFruitSpawnPositions ();
		spawnFruit ();
		InvokeRepeating ("handleSnakeMovement", 0, 0.02f);
	}

//	private void setMusicTransform(){
//		Transform musicTransform = GameObject.Find ("musicPref").transform;
//		musicTransform.SetParent (Camera.main.transform);
//		musicTransform.position = new Vector3 (0,0,0);
//	}

	// populates the fruit spawn positions array 
	private void setFruitSpawnPositions(){
		for (int x = -11; x < 11; x++) {
			for (int z = -11; z < 11; z++) {
				fruitSpawnPositions.Add (new Vector3 (x+0.5f, 0.2f, z+0.5f));
			}
		} 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			pauseMenu.pauseGame ();
		}
	}

	// synchronize movement of all body parts
	private void handleSnakeMovement(){
		if (isGameOver) {
			CancelInvoke ("handleSnakeMovement");
		} else {
			for (int i = 0; i < transform.childCount; i++) {
				if (transform.GetChild (i).GetComponent<SnakeHead> () != null) {
					transform.GetChild (i).GetComponent<SnakeHead> ().handleHeadMovement ();
				} else {
					transform.GetChild (i).GetComponent<SnakeBodyPart> ().handleBodyMovement();
				}
			}
		}
	}

	// called when ever the snake hit the wall to activate the gameOver Screen
	public void doGameOver(){
		int score = int.Parse(txtScore.text);
		if (PlayerPrefs.GetInt ("HighScore", 0) < score) {
			PlayerPrefs.SetInt ("HighScore", score);
			audioManager.playHighScore ();
			highScore.SetActive (true);
			highScore.transform.GetChild (0).GetComponent<Text> ().text = score.ToString ();
		} else {
			audioManager.playYouLose ();
			gameOver.SetActive (true);
		}
	}

	// remove the positions where the snake is covering then spawn the fruit at one of the remaining positions
	private void spawnFruit(){
		List<Vector3> copyPos = new List<Vector3> (fruitSpawnPositions);
		for (int i = 0; i < transform.childCount; i++) {
			copyPos.RemoveAll (Pos=> Pos == transform.GetChild(i).position);
		}
			
		GameObject fruitObj = Instantiate(Resources.Load<GameObject>("Prefabs/Fruit/Apple"));
		fruitObj.GetComponent<FruitHandler> ().sManager = this;
		fruitObj.transform.position = copyPos [UnityEngine.Random.Range (0, copyPos.Count)];

	}

	// when the snake eats a fruit we add the score and spawn a new fruit and a new snake body part
	public void snakeAteFruit(int fruitScore){
		int score = int.Parse (txtScore.text) + fruitScore;
		audioManager.playAppleEat ();
		txtScore.text = score.ToString();
		checkAndSpawnObstacle (score);
		spawnFruit ();
		addSnakeBodyPart ();
	}

	private void checkAndSpawnObstacle(int score){
		if(score >= 45 && oManager.canSpawn1stObs){
			fruitSpawnPositions.RemoveAll (Pos => Pos.x < -9 && Pos.x > -11 && Pos.z < 1 && Pos.z > -1);
			oManager.SpawnFirstObstacle ();
		}
		else if(score >= 80 && oManager.canSpawn2ndObs){
			fruitSpawnPositions.RemoveAll (Pos => Pos.x < 11 && Pos.x > 8 && Pos.z < 11 && Pos.z > 7);
			oManager.Spawn2ndObstacle ();
		}else if(score >= 110 && oManager.canSpawn3rdObs){
			fruitSpawnPositions.RemoveAll (Pos => Pos.x < -7 && Pos.x > -11 && Pos.z < 11 && Pos.z > 8);
			oManager.Spawn3rdObstacle ();
		}
	}

	// create a new snake body part and add a reference to the part before it 
	public void addSnakeBodyPart(){
		SnakeBodyPart prevPart = transform.GetChild(transform.childCount - 1).GetComponent<SnakeBodyPart>();

		SnakeBodyPart bodyPart = Instantiate (bodyPartPrefab, prevPart.prevPos, prevPart.transform.rotation).GetComponent<SnakeBodyPart> ();
		bodyPart.transform.SetParent (transform);
		bodyPart.name = "bodyPart" + transform.childCount;
		bodyPart.prevPart = prevPart.gameObject;
	}
}
