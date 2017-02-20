using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {
	public GameObject Obstacle1, Obstacle2, Obstacle3;
	public bool canSpawn1stObs = true, canSpawn2ndObs = true, canSpawn3rdObs = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnFirstObstacle(){
		canSpawn1stObs = false;
		Transform obsTrans = Instantiate (Obstacle1).transform;
		obsTrans.SetParent (transform);
	}

	public void Spawn2ndObstacle(){
		canSpawn2ndObs = false;
		Transform obsTrans = Instantiate (Obstacle2).transform;
		obsTrans.SetParent (transform);
	}

	public void Spawn3rdObstacle(){
		canSpawn3rdObs = false;
		Transform obsTrans = Instantiate (Obstacle3).transform;
		obsTrans.SetParent (transform);
	}

}
