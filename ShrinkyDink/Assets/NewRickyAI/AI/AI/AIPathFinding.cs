using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathFinding : MonoBehaviour {

	public AIBrainV4 ai1;


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			ai1 = other.gameObject.GetComponent<AIBrainV4> ();
			if (other.transform.position.y < ai1.PlayerChasing.transform.position.y - ai1.HeightDistanceToLookForUp) {
				ai1.Jump ();
			}
		}
	}
}
