using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.WSA;

public class MainMenu : MonoBehaviour {
	public Text txtHighScore;
	public GameObject highScorePanel, soundButton, effectsButton;
	AudioManager audioManager;
	// Use this for initialization
	void Start () {
		Invoke ("loadHighScore",1.2f);
		audioManager = AudioManager.Instance;
	}

	// Update is called once per frame
	void Update () {}

	private void loadHighScore(){
		if(PlayerPrefs.GetInt("HighScore") > 0){
			highScorePanel.SetActive (true);
			txtHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
		}
		soundButton.SetActive (true);
		effectsButton.SetActive (true);
	}

	public void newGame(){
		audioManager.playTab ();
		SceneManager.LoadScene ("GameScene");
	}
}
