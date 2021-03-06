﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health2 : MonoBehaviour {

	public float CurrentHealth;
	public float MaxHealth { get; set;}

	public Slider healthBar;

	void Start () 
	{
		MaxHealth = 20f;
		//Resets health to 20 on game load
		CurrentHealth = MaxHealth;

		healthBar.value = CalculateHealth ();

	}
	
	void Update () 
	{
		GameObject player = GameObject.Find("Player2");
		PlayerLevel hp = player.GetComponent<PlayerLevel>();
		CurrentHealth = hp.PHealth; 
		CalculateHealth ();
		healthBar.value = CalculateHealth ();


		
		//if (Input.GetKeyDown (KeyCode.X))
		//	DealDamage (2);	
	}

	/*void DealDamage (float damageValue)
	{
		//Deduct damage dealt
		CurrentHealth -= damageValue;
		healthBar.value = CalculateHealth ();
		//if character hp=100, die
		if (CurrentHealth <= 0)
			Die ();
	}*/

	float CalculateHealth()
	{
		return CurrentHealth / MaxHealth;
	}

	/*void Die()
	{
		CurrentHealth = 0;
		Debug.Log ("A Loser is YOU!");
	}*/
}
