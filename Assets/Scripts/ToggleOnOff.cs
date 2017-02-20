using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleOnOff : MonoBehaviour {

	public Image onImg;
	public Sprite OnImage,OffImage;
	public string SharedPrefsKey;
	public bool isOn;
	// Use this for initialization
	void Start () {
		int savedData = PlayerPrefs.GetInt (SharedPrefsKey,1);
		isOn = (savedData == 1);
		if (isOn) {
			onImg.sprite = OnImage;
			GameObject.Find(SharedPrefsKey).GetComponent<AudioSource>().enabled = true;
		} else {
			onImg.sprite = OffImage;
			GameObject.Find(SharedPrefsKey).GetComponent<AudioSource>().enabled = false;
		}
	}

	public void OnClick(){
		if(isOn){
			isOn = false;
			PlayerPrefs.SetInt (SharedPrefsKey,0);
			onImg.sprite = OffImage;
			GameObject.Find(SharedPrefsKey).GetComponent<AudioSource>().enabled = false;
		}else{
			PlayerPrefs.SetInt (SharedPrefsKey,1);
			isOn = true;
			onImg.sprite = OnImage;
			GameObject.Find(SharedPrefsKey).GetComponent<AudioSource>().enabled = true;
		}

		AudioManager.Instance.playTab ();
	
	}
}
