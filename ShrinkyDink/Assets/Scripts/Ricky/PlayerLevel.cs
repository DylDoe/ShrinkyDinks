using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour {
	
	public float PLevel;
	public float PExperience;
	public float PSkillPoints;
	public float PHealth;
	public float PHealthMax;
	public float PMelee;
	public float PRanged;

	public float difficultySetting;



	// Use this for initialization
	void Start () {
		PLevel = 1;
		PHealthMax = 10;
		PMelee = 5;
		PRanged = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ExperienceUp(){
		if (PExperience >= PLevel * 10) {
			PExperience -= PLevel * 10;
				LevelUp();
		}
	}

	void LevelUp(){
		PSkillPoints += difficultySetting;
		PLevel += 1;
		PHealthMax += 5;
		PRanged += 2;
		PMelee += 5;
	}
}
