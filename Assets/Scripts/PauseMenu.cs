using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void pauseGame(){
		gameObject.SetActive (true);
		Time.timeScale = 0;
	}

	public void resumeGame(){
		gameObject.SetActive (false);
		Time.timeScale = 1;
	}

	public void newGame(){
		gameObject.SetActive (false);
		Time.timeScale = 1;
		SceneManager.LoadScene ("GameScene");
	}

	public void mainMenu(){
		gameObject.SetActive (false);
		Time.timeScale = 1;
		SceneManager.LoadScene ("MainMenu");
	}
}
