using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrainV6 : MonoBehaviour {

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


	void Start () {
		//See which platform enemy starts on to avoid edge case
		r = Physics2D.Raycast(this.transform.position, Vector2.down, this.GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.01f);
		if (r.collider == null) {
			return;
		} else {
			CurrentPlatform = r.collider.gameObject;
		}
		//Set ClosestPlatform to a far away object to reset ClosestPlatform check
		ClosestPlatform = GameObject.Find ("SuperFarAwayThingToNotFuckShitUp");
		//Run FindPlayer to find available players and choose one at random to chase
		FindPlayer ();
	}


	void Update () {
		//Add to timer for jump timer
		tmr += Time.deltaTime;
		//check to see if should jump
		if (Mathf.Abs (this.transform.position.x - Target.x) < XJump && GoingUp) {
			Jump ();
		}
		//Run ifHigher to see if player is higher than enemy
		IfHigher ();
		//Move left or right depending on where Target is
		if (Target.x < this.transform.position.x){
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
		}else if (Target.x > this.transform.position.x){
			this.transform.Translate (Vector2.right * speed * Time.deltaTime);
		}

		//Cast raycast directly below enemy to see which platform they are on
		r = Physics2D.Raycast(this.transform.position, Vector2.down, this.GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.01f);
		if (r.collider == null) {
			return;
		} else {
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
			//...if YES look for way up and set GoingUp bool to true
			GoingUp = true;
			LookForUp ();
			//...if NO find out how to get to player
		} else {
			CheckIfGoToPlayerOrHowToGetDown ();
		}
	}


	void CheckIfGoToPlayerOrHowToGetDown(){
		//check if player is on a similar y axis or lower...
		if (this.transform.position.y > PlayerChasing.transform.position.y + HeightDistanceToLookForUp) {
			//...if lower figure out how to get lower and set GoingUp bool to false
			GoingUp = false;
			HowToGetDown ();
		} else {
			//...if similar, chase player
			Target = PlayerChasing.transform.position;
		}
	}

	void HowToGetDown(){
		//check if player is to the left...
		if (CurrentPlatform.transform.position.x > PlayerChasing.transform.position.x) {
			//...if YES go left
			OffSet.y = CurrentPlatform.transform.position.y;
			OffSet.x = CurrentPlatform.transform.position.x - CurrentPlatform.GetComponent<SpriteRenderer> ().bounds.size.x /2;
		} else {
			//...if NO go right
			OffSet.y = CurrentPlatform.transform.position.y;
			OffSet.x = CurrentPlatform.transform.position.x + CurrentPlatform.GetComponent<SpriteRenderer> ().bounds.size.x /2;
		}
		Target = OffSet;
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
		if (ClosestPlatform != GameObject.Find ("SuperFarAwayThingToNotFuckShitUp")) {
			//...if NOT then run HowToGetOnTargetPlatform
			HowToGetOnTargetPlatform ();
		}
	}

	//Used to check which side of the platform
	void HowToGetOnTargetPlatform(){
		//check if platform is more to the right...
		if (ClosestPlatform.transform.position.x > this.transform.position.x) {
			//...if YES move left
			OffSet.x = ClosestPlatform.transform.position.x - ClosestPlatform.GetComponent<SpriteRenderer> ().bounds.size.x / 2;
			OffSet.y = ClosestPlatform.transform.position.y;
			Target = OffSet;
		} else {
			//...if NO move right
			OffSet.x = ClosestPlatform.transform.position.x + ClosestPlatform.GetComponent<SpriteRenderer> ().bounds.size.x / 2;
			OffSet.y = ClosestPlatform.transform.position.y;
			Target = OffSet;
		}
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
