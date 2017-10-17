using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUnlock : MonoBehaviour {


	public float unlockF = 2f;
	public Button dashUnlock;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject player = GameObject.Find("Player");
		PlayerLevel level = player.GetComponent<PlayerLevel>();
		float levelF = level.PLevel;
		if (levelF >= unlockF) {
			dashUnlock.interactable = true;
		}	
	}
}
