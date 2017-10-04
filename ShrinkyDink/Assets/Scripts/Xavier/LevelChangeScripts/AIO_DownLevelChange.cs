using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIO_DownLevelChange : MonoBehaviour 
{
	GameObject player1;
	GameObject player2;

	//public GameObject downLevelChanger;

	public AIO_LevelChange levelChangeScript;



	void Start()
	{
		player1 = GameObject.FindGameObjectWithTag("Player");
		player2 = GameObject.FindGameObjectWithTag("Player2");
	}



	void Update () 
	{
		if (Vector2.Distance(this.transform.position, player1.transform.position) <= 5f && Vector2.Distance(this.transform.position, player2.transform.position) <= 5f && (Input.GetButtonDown("Interact")))
		{
			levelChangeScript.MovePlayersToNewLevel();
		}
	}
}
