using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHandler : MonoBehaviour {

	public int fruitScore;
	public SnakeManager sManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		Destroy (gameObject);
		sManager.snakeAteFruit(fruitScore);
	}
}
