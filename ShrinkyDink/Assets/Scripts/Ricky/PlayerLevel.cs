using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour {
	
	public float PLevel;
	public float PExperience;
	public float PSkillPoints;
	public float PHealth;
	public float PHealthMax;
	//public float PMelee;
	public float PRanged;

	public float expMultReqToLvl;

	public ParticleSystem pDmgFX;

	public float lastPHealthAmount;


	[HeaderAttribute("Per Level Stat Increases")]
	public float difficultySetting;
	public float healthPerLvl;
	public float dmgPerLvl;

	[HeaderAttribute("Starting Stats")]
	public float startHealth;
	public float startDamage;







	void Start () {
		PLevel = 1;
		PHealthMax = startHealth;
		//PMelee = 5;
		PRanged = startDamage;
		PHealth = PHealthMax;

		pDmgFX = this.transform.Find("DamageTaken FX").gameObject.GetComponent<ParticleSystem>();
	}
	

	void Update () 
	{
		if (lastPHealthAmount > PHealth)
		pDmgFX.Play(true);

		lastPHealthAmount = PHealth;
	}

	public void ExperienceUp(){
		if (PExperience >= PLevel * expMultReqToLvl) 
		{
			PExperience -= PLevel * expMultReqToLvl;
				LevelUp();
		}
	}

	void LevelUp(){
		PSkillPoints += difficultySetting;
		PLevel += 1;
		PHealthMax += healthPerLvl;
		PRanged += dmgPerLvl;
		PHealth = PHealthMax;
		//PMelee += 5;
	}
}
