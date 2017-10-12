using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.SetActive (false);
	}

	void OnTriggerEnter (Collider other){
		other.GetComponent<PlayerLevel> ().PHealth -= this.GetComponent<EnemyLevel> ().damage;
	}
}
