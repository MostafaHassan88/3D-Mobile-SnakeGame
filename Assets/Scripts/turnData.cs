using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnData {
		Vector3 turnPos;
		KeyCode direcKey;

		public turnData(Vector3 tPos, KeyCode pressedKey){
			turnPos = tPos;
			direcKey = pressedKey;
		}

		public Vector3 getTurnPos(){
			return turnPos;
		}

		public KeyCode getDirecKey(){
			return direcKey;
		}
}
