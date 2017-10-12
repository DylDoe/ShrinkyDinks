using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour {




	//Object Needed
	//Array of available platforms
	private GameObject[] Plats;
	//Array of available players
	private GameObject[] Player;
	//Which player from player array to chase (needs to be public as another code accesses it)
	public GameObject PlayerChasing;
	//CLosest platform to check what next target should be
	private GameObject ClosestPlatform;
	//Memory of current platform standing on
	public GameObject CurrentPlatform;

	//Floats
	//How high to look up for next platform (used to avoid looking at platforms that are too high)
	public float HeightDistanceToLookForUp;
	//How high enemy can jump
	public float JumpHeight;
	//How high to jump (this is force, and not how many units)
	public float jump;
	//How fast enemy can move
	public float speed;
	//Hidden float for timer
	private float tmr;
	//Amount of time between jumps
	public float jumptmr;
	//Amount of lateral space needed to jump on platform
	public float XJump;

	//Vectors
	//Abstract vector2 used to get off of a platform
	private Vector2 OffSet;
	//The target in which the enemy is going towards (switch from gameobject to vector2 for ease of manipulation)
	public Vector2 Target;

	//Raycasts
	//I need a raycast to check which platform the enemy is currently on
	private RaycastHit2D r;

	//Bools
	//Needed a bool to help with jumping
	private bool GoingUp = false;

	//State
	//CurrentState
	private State currentState;
	//Default state
	private State defaultState = State.Idle;


	public enum State {
		Idle,
		FindPlayer,
		IfHigher,
		CheckIfGoToPlayerOrHowToGetDown,
		HowToGetDown,
		LookForUp,
		HowToGetOnTargetPlatform,
		Walk,
		Jump,
		Attack,
	}

	// Use this for initialization
	void Start () {
		this.currentState = this.defaultState;
		//Set ClosestPlatform to a far away object to reset ClosestPlatform check
		ClosestPlatform = GameObject.Find ("SuperFarAwayThingToNotFuckShitUp");
	}


	// Update is called once per frame
	void Update () {
		//Add to timer for jump timer
		tmr += Time.deltaTime;

		//See which platform enemy starts on to avoid edge case
		r = Physics2D.Raycast(this.transform.position, Vector2.down, this.GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.1f);
		if (r.collider == null) {
			return;
		} else {
			CurrentPlatform = r.collider.gameObject;
		}

		//Run States
		switch (this.currentState) {
		case State.Idle: this.Idle(); break;
		case State.FindPlayer: this.FindPlayer(); break;
		case State.IfHigher: this.IfHigher(); break;
		case State.CheckIfGoToPlayerOrHowToGetDown: this.CheckIfGoToPlayerOrHowToGetDown(); break;
		case State.HowToGetDown: this.HowToGetDown(); break;
		case State.LookForUp: this.LookForUp(); break;
		case State.HowToGetOnTargetPlatform: this.HowToGetOnTargetPlatform(); break;
		case State.Walk: this.Walk(); break;
		case State.Jump: this.Jump(); break;
		case State.Attack: this.Attack (); break;
		}
	}

	void Idle () {
		//Check if enemy has a player target...
		if (PlayerChasing == null) {
			//...if NO, get player target
			this.currentState = State.FindPlayer;
			//...if YES, start path finder
		} else if (PlayerChasing != null) {
			this.currentState = State.IfHigher;
		}
	}

	void FindPlayer(){
		//Find all available players
		Player = GameObject.FindGameObjectsWithTag ("Player");
		//Chose a player at random to chase
		this.PlayerChasing = Player [Random.Range (0, Player.Length)];
		//Reset state to idle
		this.currentState = State.Idle;
	}

	void IfHigher(){
		//If player is higher than the enemy..
		if ((PlayerChasing.transform.position.y - this.transform.position.y) >= HeightDistanceToLookForUp) {
			//...if YES look for way up and set GoingUp bool to true
			GoingUp = true;
			this.currentState = State.LookForUp;
			//...if NO find out how to get to player and set GoingUp bool to false
		} else {
			GoingUp = false;
			this.currentState = State.CheckIfGoToPlayerOrHowToGetDown;
		}
	}

	void CheckIfGoToPlayerOrHowToGetDown(){
		//check if player is on a similar y axis or lower...
		if (Mathf.Abs(PlayerChasing.transform.position.y - this.transform.position.y) >= HeightDistanceToLookForUp) {
			//...if lower figure out how to get lower
			this.currentState = State.HowToGetDown;
		} else {
			//...if similar, chase player
			Target = PlayerChasing.transform.position;
			this.currentState = State.Walk;
		}
	}

	void HowToGetDown(){
		//check if player is to the left...
		if (CurrentPlatform.transform.position.x > this.transform.position.x) {
			//...if YES go left
			OffSet.y = CurrentPlatform.transform.position.y;
			OffSet.x = CurrentPlatform.transform.position.x - CurrentPlatform.GetComponent<SpriteRenderer> ().bounds.size.x /2;
		} else {
			//...if NO go right
			OffSet.y = CurrentPlatform.transform.position.y;
			OffSet.x = CurrentPlatform.transform.position.x + CurrentPlatform.GetComponent<SpriteRenderer> ().bounds.size.x /2;
		}
		Target = OffSet;
		this.currentState = State.Walk;
	}

	void LookForUp(){
		//Find all available platforms
		Plats = GameObject.FindGameObjectsWithTag ("Platform");
		//Reset ClosestPlatform for next CLosestPlatform check
		ClosestPlatform = GameObject.Find ("SuperFarAwayThingToNotFuckShitUp");

		for (int i = 0; i < Plats.Length; i++) {
			//check if platform is jumpable by height
			if (Plats[i].transform.position.y > this.transform.position.y && Plats [i].transform.position.y - this.transform.position.y <= JumpHeight ) {
				//check if platform is jumpable horizontally
				if (Mathf.Abs (Plats [i].transform.position.x - CurrentPlatform.transform.position.x) < Mathf.Abs(Plats [i].transform.position.x - CurrentPlatform.transform.position.x) + Plats [i].GetComponent<SpriteRenderer>().bounds.size.x / 2 + CurrentPlatform.GetComponent<SpriteRenderer>().bounds.size.x / 2) {
					//find closest platform to player to jump
					if (Mathf.Abs (Plats [i].transform.position.x - PlayerChasing.transform.position.x) < Mathf.Abs (ClosestPlatform.transform.position.x - PlayerChasing.transform.position.x)) {
						//Set this Plats[i] as the CLosestPlatform (Will repeat for all Plats and give out only one)
						ClosestPlatform = Plats [i];
					}
				}
			}
		}
		//check to make sure enemy is not targeting SFATTNFSU...
		if (ClosestPlatform.name == ("SuperFarAwayThingToNotFuckShitUp")) {
			//...if NOT then run HowToGetOnTargetPlatform
			this.currentState = State.FindPlayer;
		} else {
			this.currentState = State.HowToGetOnTargetPlatform;
		}
	}

	void HowToGetOnTargetPlatform(){
		//check if platform is more to the right...
		if (ClosestPlatform.transform.position.x > this.transform.position.x) {
			//...if YES move left
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
		} else {
			//...if NO move right
			this.transform.Translate (Vector2.right * speed * Time.deltaTime);
		}
		this.currentState = State.Idle;
	}

	void Walk(){
		if (Mathf.Abs (this.transform.position.x - Target.x) <= 1f && GoingUp) {
			this.currentState = State.Jump;
		}
		//Move left or right depending on where Target is
		if (Target.x < this.transform.position.x){
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
		}else if (Target.x > this.transform.position.x){
			this.transform.Translate (Vector2.right * speed * Time.deltaTime);
		}
	}

	void Jump(){
		Target = ClosestPlatform.transform.position;
		if (Target.x < this.transform.position.x){
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
		}else if (Target.x > this.transform.position.x){
			this.transform.Translate (Vector2.right * speed * Time.deltaTime);
		}
		this.transform.Translate (Vector2.up * jump);
		GoingUp = false;
		this.currentState = State.IfHigher;
	}
		
	void Attack(){
		Debug.Log ("Attack!!");
		this.currentState = State.Idle;
	}
}