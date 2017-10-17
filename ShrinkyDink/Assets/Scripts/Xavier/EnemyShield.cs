using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour 
{
	public EnemyLevel enemyLvlScript;

	public GameObject player1;
	public GameObject player2;

	public PlayerLevel PL1;
	public PlayerLevel PL2;

	//public float shieldAmount;


	void Start () 
	{
		player1 = GameObject.FindGameObjectWithTag("Player");
		player2 = GameObject.FindGameObjectWithTag("Player2");

		PL1 = player1.GetComponent<PlayerLevel>();
		PL2 = player2.GetComponent<PlayerLevel>();

		enemyLvlScript = this.transform.parent.GetComponent<EnemyLevel>();

		// if ( PL1.PLevel > PL2.PLevel)
		// {
		// 	shieldAmount = Mathf.Clamp(PL2.PRanged * (timer / chargeSelfDmgDampner), PL.PRanged, PL.PRanged * 5f);
		// }
	}
}
