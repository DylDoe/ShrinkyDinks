using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrainV3 : MonoBehaviour {

	//Object Needed
	//Array of available platforms
	private GameObject[] Plats;
	//Array of available players
	private GameObject[] Player;
	//Which player from player array to chase (needs to be public as another code accesses it)
	public GameObject PlayerChasing;
	//The target in which the enemy is going towards
	public GameObject Target;
	//CLosest platform to check what next target should be
	private GameObject ClosestPlatform;
	//Memory of current platform standing on
	public GameObject CurrentPlatform;

	//Floats
	//How high to look up for next platform (used to avoid looking at platforms that are too high)
	public float HeightDistanceToLookForUp;
	//How high enemy can jump (yes I realise this is redundant, I just prefer them being two different numbers)
	public float JumpHeight;
	//How high to jump (yes I realise this is redundant, I just prefer them being two different numbers)
	public float jump;
	//How fast enemy can move
	public float speed;
	//Hidden float for timer
	private float tmr;
	//Amount of time between jumps
	public float jumptmr;
	//Amount of lateral space needed to jump on platform
	public float XJump;

	//Raycasts
	//I need a raycast to check which platform the enemy is currently on
	private RaycastHit2D r;


	void Start () {
		//Set ClosestPlatform to a far away object to reset ClosestPlatform check
		ClosestPlatform = GameObject.Find ("SuperFarAwayThingToNotFuckShitUp");
		//Run FindPlayer to find available players and choose one at random to chase
		FindPlayer ();
	}


	void Update () {
		//Add to timer for jump timer
		tmr += Time.deltaTime;
		//Run ifHigher to see if player is higher than enemy
		IfHigher ();
		//Move left or right depending on where Target is
		if (Target.transform.position.x < this.transform.position.x){
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
		}else if (Target.transform.position.x > this.transform.position.x){
			this.transform.Translate (Vector2.right * speed * Time.deltaTime);
		}

		//Cast raycast directly below enemy to see which platform they are on
		r = Physics2D.Raycast(this.transform.position, Vector2.down, this.transform.localScale.y / 2 + 0.01f);
		if(r.collider.tag == "Platform"){
			CurrentPlatform = r.collider.gameObject;
		}
	}

	void FindPlayer(){
		//Find all available players
		Player = GameObject.FindGameObjectsWithTag ("Player");
		//Chose a player at random to chase
		this.PlayerChasing = Player [Random.Range (0, Player.Length)];
	}

	void IfHigher(){
		//If player is higher than the enemy..
		if (PlayerChasing.transform.position.y > this.transform.position.y + HeightDistanceToLookForUp) {
			//...if YES look for way up
			LookForUp ();
			//...if NO set target to player
		} else {
			Target = PlayerChasing;
		}

	}



	void LookForUp(){
		//Find all available platforms
		Plats = GameObject.FindGameObjectsWithTag ("Platform");
		//Reset ClosestPlatform for next CLosestPlatform check
		ClosestPlatform = GameObject.Find ("SuperFarAwayThingToNotFuckShitUp");

		for (int i = 0; i < Plats.Length; i++) {
			//check if platform is jumpable by height
			if (Plats [i].transform.position.y - this.transform.position.y <= JumpHeight && Plats[i].transform.position.y > this.transform.position.y) {
				//check if platform is jumpable by laterally
				if (Mathf.Abs (Plats [i].transform.position.x - CurrentPlatform.transform.position.x) < Mathf.Abs(Plats [i].transform.position.x - CurrentPlatform.transform.position.x) + Plats [i].transform.localScale.x / 2 + CurrentPlatform.transform.localScale.x / 2) {
				//find closest platform to player to jump
					if (Mathf.Abs (Plats [i].transform.position.x - PlayerChasing.transform.position.x) < Mathf.Abs (ClosestPlatform.transform.position.x - PlayerChasing.transform.position.x)) {
						//Set this Plats[i] as the CLosestPlatform (Will repeat for all Plats and give out only one)
						ClosestPlatform = Plats [i];
					}
				}
			}
		}
		//Set target to closest jumpable platform

		if (ClosestPlatform != GameObject.Find ("SuperFarAwayThingToNotFuckShitUp")) {
			Target = ClosestPlatform;
			HowToGetOnTargetPlatform ();
		}
	}

	//Used to check which side of the platform
	void HowToGetOnTargetPlatform(){
		if (ClosestPlatform.transform.position.x < this.transform.position.x) {
			CheckLeftSidePositionOfPlatform ();
		} else {
			CheckRightSidePositionOfPlatform ();
		}
	}

	void CheckLeftSidePositionOfPlatform(){

	}

	void CheckRightSidePositionOfPlatform(){

	}

	public void Jump(){
		//if enough time has passed, make able to jump
		if (tmr > jumptmr) {
			//Reset jump timer
			tmr = 0;
			//Jump
			this.transform.Translate (Vector2.up * jump * Time.deltaTime);
		}
	}
	//Used to indicate end of code
}