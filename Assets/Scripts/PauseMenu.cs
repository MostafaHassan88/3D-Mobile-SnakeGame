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
		AudioManager.Instance.playTab ();
		gameObject.SetActive (true);
		Time.timeScale = 0;
	}

	public void resumeGame(){
		AudioManager.Instance.playTab ();
		gameObject.SetActive (false);
		Time.timeScale = 1;
	}

	public void newGame(){
		AudioManager.Instance.playTab ();
		gameObject.SetActive (false);
		Time.timeScale = 1;
		SceneManager.LoadScene ("GameScene");
	}

	public void mainMenu(){
		AudioManager.Instance.playTab ();
		gameObject.SetActive (false);
		Time.timeScale = 1;
		SceneManager.LoadScene ("MainMenu");
	}
}
