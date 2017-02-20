using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager {

	AudioSource effectSource;
	AudioClip appleEat;
	AudioClip tabSound;
	AudioClip highScore;
	AudioClip youLose;
	// Use this for initialization


	private static AudioManager instance;

	private AudioManager() {
		loadEffectPref ();
		loadMusicPref ();
		checkPrefs("musicPref");
		checkPrefs ("effectPref");
		loadClips ();
	}

	public static AudioManager Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new AudioManager();
			}
			return instance;
		}
	}

	private void loadMusicPref(){
		if(GameObject.Find("musicPref") == null){
			GameObject musicPref = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Sound/musicPref"));
			musicPref.name = "musicPref";
			GameObject.DontDestroyOnLoad(musicPref);
		}
	}

	private void loadEffectPref(){
		if(GameObject.Find("effectPref") == null){
			GameObject effectPref = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Sound/effectPref"));
			effectPref.name = "effectPref";
			GameObject.DontDestroyOnLoad(effectPref);
			effectSource = effectPref.GetComponent<AudioSource> ();
		}
	}

	private void checkPrefs(string SharedPrefsKey){
		Debug.Log ("ok");
		bool isOn = (PlayerPrefs.GetInt (SharedPrefsKey,1)==1);

		if (isOn) {
			GameObject.Find(SharedPrefsKey).GetComponent<AudioSource>().enabled = true;
		} else {
			GameObject.Find(SharedPrefsKey).GetComponent<AudioSource>().enabled = false;
		}

	}

	private void loadClips (){
		appleEat = GameObject.Instantiate(Resources.Load<AudioClip>("Sounds/appleEat"));
		tabSound = GameObject.Instantiate(Resources.Load<AudioClip>("Sounds/tabSound"));
		highScore = GameObject.Instantiate(Resources.Load<AudioClip>("Sounds/highScore"));
		youLose = GameObject.Instantiate(Resources.Load<AudioClip>("Sounds/youLose"));
	}

	public void playAppleEat(){
		effectSource.PlayOneShot (appleEat);
	}

	public void playTab(){
		effectSource.PlayOneShot (tabSound);
	}

	public void playHighScore(){
		effectSource.PlayOneShot (highScore);
	}

	public void playYouLose(){
		effectSource.PlayOneShot (youLose);
	}
}
