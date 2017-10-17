using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashMultiplier : MonoBehaviour {

	public float multiplier = 0f;

	void Start () {
		
	}
	
	void Update () {
		GameObject player = GameObject.Find("Player");
		Player dashDist = player.GetComponent<Player>();

		dashDist.dashDistance = 2f * multiplier;

		
	}

	public void firstUpgrade (){
		multiplier = 1f;
	}
	public void secondUpgrade (){
		multiplier = 2f;
	}
}
