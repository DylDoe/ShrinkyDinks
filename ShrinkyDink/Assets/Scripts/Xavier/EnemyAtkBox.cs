using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkBox : MonoBehaviour 
{

	public EnemyLevel EL;
	public float dmg;
	public PlayerLevel PL;

	void Start () 
	{
		EL = this.transform.parent.GetComponent<EnemyLevel>();
		dmg = EL.damage;
	}
	

	void OnTriggerEnter2D (Collider2D other) 
	{
		if(other.tag == "Player")
		{
			PL = other.GetComponent<PlayerLevel>();
			PL.PHealth -= dmg;
		}

		if(other.tag == "Player2")
		{
			PL = other.GetComponent<PlayerLevel>();
			PL.PHealth -= dmg;
		}
	}
}
