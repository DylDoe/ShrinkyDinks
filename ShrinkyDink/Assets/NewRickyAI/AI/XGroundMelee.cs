using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XGroundMelee : MonoBehaviour {

	private float check;
	private GameObject currentPlatform;
	public float crntSpeed;
	public float initalSpeed;
	private RaycastHit2D ray;
	private RaycastHit2D ray2;
	public LayerMask layersToHit;
	private RaycastHit2D view;
	public float viewRange;
	public GameObject player1;
	public GameObject player2;
	public float attackDistance;
	public GameObject playerChasing;
	public float loseDistance;
	public GameObject attackZone;
	private float attackTimer;
	public float attackCooldown;

	//State
	//CurrentState
	private State currentState;
	//Default state
	private State defaultState = State.Idle;

	//Distance Check to detect players
	public float distToPlyr1;
	public float distToPlyr2;

	//Attacking
	public GameObject leftAtkWarn;
	//public GameObject leftAtkHitBox;
	public float atkRng;
	public bool moveLeftAtkWarn = true;
	public bool moveRightAtkWarn = true;
	public bool atkingLeft = false;
	public Vector3 atkWarnInitPos;

	public int initiateLeftAtk;
	public float distForAtkWarn;
	public float atkWarnTimer = 0f;	
	public float atkHitTimer = 0f;	
	public float atkWarnSpeed;
	public GameObject hitParticleEffect;
	public bool playerIsLeft;
	public bool playerIsRight;

	public GameObject destroyedFX;

	//Before float for experience;
	public float BeforeFloat;
	//After float for experience;
	public float AfterFloat;
	//Bool for ease fo use
	public bool goThrough;
	//To get enemy script to apply damage
	public EnemyLevel EH;
	//To get player script to give experience
	public PlayerLevel PL1;
	public PlayerLevel PL2;

	public bool isQuitting = false;



	public enum State {
		Idle,
		Patrol,
		GoToPlayer,
		BeforeAttackPlayer,
		AttackPlayer,
		AfterAttackPlayer,
	}



	// Use this for initialization
	void Start () 
	{
		check = Random.Range (0, 2);
		player1 = GameObject.FindGameObjectWithTag("Player");
		player2 = GameObject.FindGameObjectWithTag("Player2");

		PL1 = player1.GetComponent<PlayerLevel>();
		PL2 = player2.GetComponent<PlayerLevel>();

		leftAtkWarn = transform.Find("Left Attack Warning").gameObject;
		//leftAtkHitBox = transform.Find("Left Attack Hitbox").gameObject;

		crntSpeed = initalSpeed;
	}

	

	// Update is called once per frame
	void Update () {

		attackTimer += Time.deltaTime;

		//Run States
		switch (this.currentState) {
		case State.Idle: this.Idle(); break;
		case State.Patrol: this.Patrol(); break;
		case State.GoToPlayer: this.GoToPlayer(); break;
		case State.BeforeAttackPlayer: this.BeforeAttackPlayer(); break;
		case State.AttackPlayer: this.AttackPlayer(); break;
		case State.AfterAttackPlayer: this.AfterAttackPlayer(); break;

		}
	}



	void Idle()
	{
		this.currentState = State.Patrol;
	}



	void Patrol()
	{

		ray = Physics2D.Raycast(transform.position, -Vector2.up, 1f);

		if (ray.collider == null) {
			if(check == 0){
				check = 1;
			} else {
				check = 0;
			}
		}

		// Walk left or right stuff.	
		if (check == 0) {
			this.transform.Translate (Vector2.right * crntSpeed * Time.deltaTime);
			//view = Physics2D.Raycast(transform.position, Vector2.right, viewRange, layersToHit);
			ray2 = Physics2D.Raycast(transform.position, Vector2.right, 1f, layersToHit);
			Debug.DrawRay(transform.position, Vector2.right);
			if(ray2.collider != null)
			check = 1;
		} else {
			this.transform.Translate (Vector2.left * crntSpeed * Time.deltaTime);
			//view = Physics2D.Raycast(transform.position, Vector2.left, viewRange, layersToHit);
			ray2 = Physics2D.Raycast(transform.position, Vector2.left, 1f, layersToHit);
			Debug.DrawRay(transform.position, Vector2.left);
			if(ray2.collider != null)
			check = 0;
		}

		distToPlyr1 = Vector3.Distance(this.transform.position, player1.transform.position);
		distToPlyr2 = Vector3.Distance(this.transform.position, player2.transform.position);

		if (distToPlyr1 <= viewRange)
		{
			this.playerChasing = player1;
			this.currentState = State.GoToPlayer;
		}

		if (distToPlyr2 <= viewRange)
		{
			this.playerChasing = player2;
			this.currentState = State.GoToPlayer;
		}
		// if(currentPlatform)
		// {
		// 	Debug.Log("currentPlatform");
		// 	if (this.transform.position.x >= (currentPlatform.transform.position.x + currentPlatform.GetComponent<SpriteRenderer> ().bounds.size.x /2) || this.transform.position.x <= (currentPlatform.transform.position.x - currentPlatform.GetComponent<SpriteRenderer> ().bounds.size.x /2)){
		// 		if(check == 0){
		// 			check = 1;
		// 		} else {
		// 			check = 0;
		// 		}
		// 	}
		// }

		//if (view.collider)
		//if (view.collider.tag == "Player"){
			//this.playerChasing = view.collider.gameObject;
			//this.currentState = State.GoToPlayer;
		//}
	}



	void GoToPlayer()
	{
		// if (view.collider.tag == null) {
		// 	this.currentState = State.Patrol;		
		// }
		distToPlyr1 = Vector3.Distance(this.transform.position, player1.transform.position);
		distToPlyr2 = Vector3.Distance(this.transform.position, player2.transform.position);

		if (distToPlyr1 < distToPlyr2)
		{
			this.playerChasing = player1;
			//Debug.Log("should change chase to p1");
		}

		if (PL1.PHealth < 0.0f) {this.playerChasing = player2;}


		if (distToPlyr2 < distToPlyr1)
		{
			this.playerChasing = player2;
			//Debug.Log("should change chase to p2");
		}

		if (PL2.PHealth < 0.0f) {this.playerChasing = player1;}


		if (playerChasing.transform.position.x - 1f >= this.transform.position.x) {
			this.transform.Translate (Vector2.right * crntSpeed * Time.deltaTime);
		} else if (playerChasing.transform.position.x + 1f <= this.transform.position.x) {
			this.transform.Translate (Vector2.left * crntSpeed * Time.deltaTime);
		}

		if (Mathf.Abs(this.transform.position.x - playerChasing.transform.position.x) <= attackDistance && attackTimer >= attackCooldown) 
		{
			attackTimer = 0;
			this.currentState = State.BeforeAttackPlayer;
		} 
		else if (Mathf.Abs(this.transform.position.x - playerChasing.transform.position.x) >= loseDistance || Mathf.Abs(this.transform.position.y - playerChasing.transform.position.y) >= loseDistance) {
			this.currentState = State.Idle;
		}
	}



	void BeforeAttackPlayer()
	{
		if (playerChasing.transform.position.x < this.transform.position.x)
		{
			playerIsLeft = true;
			playerIsRight = false;
		}
		else if (playerChasing.transform.position.x > this.transform.position.x)
		{
			playerIsLeft = false;
			playerIsRight = true;
		}

		this.currentState = State.AttackPlayer;
	}

	void AttackPlayer()
	{
		crntSpeed = 0f;

			//attack left

			if (playerIsLeft == true)
			{
				if(leftAtkWarn.activeSelf == false && moveLeftAtkWarn == true)
				{
					leftAtkWarn.SetActive(true);
					leftAtkWarn.transform.localPosition = new Vector3(0, 0, -5);
				}
					
				atkWarnTimer += Time.deltaTime;
							

				if (moveLeftAtkWarn == true)
				{
					leftAtkWarn.transform.Translate(Vector3.left * atkWarnSpeed * Time.deltaTime);
				}

				if (atkWarnTimer >= atkRng)
				{
					moveLeftAtkWarn = false;
					//leftAtkWarn.SetActive(false);
					atkWarnTimer = 0f;
				}

				if (moveLeftAtkWarn == false)
				{
					//leftAtkHitBox.SetActive(true);
					if (atkHitTimer <= 0.0f)
					Instantiate(hitParticleEffect, leftAtkWarn.transform.position, leftAtkWarn.transform.rotation, this.transform);

					atkHitTimer += Time.deltaTime;
					if (atkHitTimer >= 0.5f)
					{ moveRightAtkWarn = true; }
				}

				if (moveRightAtkWarn == true && moveLeftAtkWarn == false)
				{
					leftAtkWarn.transform.Translate(Vector3.right * atkWarnSpeed * Time.deltaTime);
				}

				if (atkWarnTimer >= (atkRng * -1))
				{
					moveRightAtkWarn = false;

				}

				atkWarnTimer += Time.deltaTime;

				if ( leftAtkWarn.activeSelf == false)
				{
					this.currentState = State.AfterAttackPlayer;
				}
			}


			//attack right

			if (playerIsRight == true)
			{
				if(leftAtkWarn.activeSelf == false && moveLeftAtkWarn == true)
				{
					leftAtkWarn.SetActive(true);
					leftAtkWarn.transform.localPosition = new Vector3(0, 0, -5);
				}
					
				atkWarnTimer += Time.deltaTime;
							

				if (moveLeftAtkWarn == true)
				{
					leftAtkWarn.transform.Translate(Vector3.right * atkWarnSpeed * Time.deltaTime);
				}

				if (atkWarnTimer >= atkRng)
				{
					moveLeftAtkWarn = false;
					//leftAtkWarn.SetActive(false);
					atkWarnTimer = 0f;
				}

				if (moveLeftAtkWarn == false)
				{
					//leftAtkHitBox.SetActive(true);
					if (atkHitTimer <= 0.0f)
					Instantiate(hitParticleEffect, leftAtkWarn.transform.position, leftAtkWarn.transform.rotation, this.transform);

					atkHitTimer += Time.deltaTime;
					if (atkHitTimer >= 0.5f)
					{ moveRightAtkWarn = true; }
				}

				if (moveRightAtkWarn == true && moveLeftAtkWarn == false)
				{
					leftAtkWarn.transform.Translate(Vector3.left * atkWarnSpeed * Time.deltaTime);
				}

				if (atkWarnTimer >= (atkRng * -1))
				{
					moveRightAtkWarn = false;

				}

				atkWarnTimer += Time.deltaTime;

				if ( leftAtkWarn.activeSelf == false)
				{
					this.currentState = State.AfterAttackPlayer;
				}
			}


		//this.currentState = State.GoToPlayer;
	}

	void AfterAttackPlayer()
	{
		crntSpeed = initalSpeed;
		moveLeftAtkWarn = true;
		atkWarnTimer = 0f;
		atkHitTimer = 0f;
		//leftAtkWarn.transform.position = new Vector3(0, 0, -5);


		this.currentState = State.GoToPlayer;
	}


	void OnApplicationQuit()
	{
		isQuitting = true;
	}


	void OnDestroy()
	{
		if (!isQuitting)
		Instantiate(destroyedFX, this.transform.position, this.transform.rotation);
	}


}
