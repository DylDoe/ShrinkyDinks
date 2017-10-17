using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffOtherScripts : MonoBehaviour 
{
	public Controller2D controller2DScript;
	public Player playerScript;
	public PlayerInput playerInputScript;
	public Shoot shootScript;


	void Start () 
	{
		controller2DScript = GetComponent<Controller2D>();
		playerScript = GetComponent<Player>();
		playerInputScript = GetComponent<PlayerInput>();
		shootScript = GetComponent<Shoot>();
	}

	public void TurnOnPlayerScripts () 
	{
		//controller2DScript.enabled = true;
		//playerScript.enabled = true;
		playerInputScript.enabled = true;
		//shootScript.enabled = true;
	}
	

	public void TurnOffPlayerScripts () 
	{
		//controller2DScript.enabled = false;
		//playerScript.enabled = false;
		playerInputScript.enabled = false;
		//shootScript.enabled = false;
	}
}
