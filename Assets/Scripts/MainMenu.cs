using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public Text txtHighScore;
	public GameObject highScorePanel;
	// Use this for initialization
	void Start () {
		Invoke ("loadHighScore",1.2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void loadHighScore(){
		if(PlayerPrefs.GetInt("HighScore") > 0){
			highScorePanel.SetActive (true);
			txtHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
		}
	}

	public void newGame(){
		SceneManager.LoadScene ("GameScene");
	}
}
