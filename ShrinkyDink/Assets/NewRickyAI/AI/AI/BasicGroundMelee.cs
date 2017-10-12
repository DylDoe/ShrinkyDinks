using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGroundMelee : MonoBehaviour {

	private float check;
	private GameObject currentPlatform;
	public float speed;
	private RaycastHit2D ray;
	private RaycastHit2D view;
	public float viewRange;
	public GameObject Player1;
	public GameObject Player2;
	public float attackDistance;
	public GameObject playerChasing;
	public float loseDistance;
	public GameObject attackZone;
	private float attackTimer;
	public float attackTime;

	//State
	//CurrentState
	private State currentState;
	//Default state
	private State defaultState = State.Idle;



	public enum State {
		Idle,
		Patrol,
		GoToPlayer,
		AttackPlayer,
	}

		

	// Use this for initialization
	void Start () {
		check = Random.Range (0, 2);
	}
	
	// Update is called once per frame
	void Update () {

		attackTimer += Time.deltaTime;

		//Run States
		switch (this.currentState) {
		case State.Idle: this.Idle(); break;
		case State.Patrol: this.Patrol(); break;
		case State.GoToPlayer: this.GoToPlayer(); break;
		case State.AttackPlayer: this.AttackPlayer(); break;
		
		}
	}


	void Idle(){
		this.currentState = State.Patrol;
	}


	void Patrol(){
		if (currentPlatform == null) {
			ray = Physics2D.Raycast(transform.position, -Vector2.up);
			currentPlatform = ray.collider.gameObject;
		}

		if (check == 0) {
			this.transform.Translate (Vector2.right * speed * Time.deltaTime);
			view = Physics2D.Raycast(transform.position, Vector2.right, 10f);
		} else {
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
			view = Physics2D.Raycast(transform.position, Vector2.left, 10f);
		}
		if (this.transform.position.x >= (currentPlatform.transform.position.x + currentPlatform.GetComponent<SpriteRenderer> ().bounds.size.x /2) || this.transform.position.x <= (currentPlatform.transform.position.x - currentPlatform.GetComponent<SpriteRenderer> ().bounds.size.x /2)){
			if(check == 0){
				check = 1;
			} else {
				check = 0;
			}
		}
		if (view.collider && view.collider.tag.Contains("Player"))
		{
			this.playerChasing = view.collider.gameObject;
			this.currentState = State.GoToPlayer;
		}
	}

	void GoToPlayer(){
		if (view.collider.tag == null) {
			this.currentState = State.Patrol;		
		}

		if (playerChasing.transform.position.x >= this.transform.position.x) {
			this.transform.Translate (Vector2.right * speed * Time.deltaTime);
		} else if (playerChasing.transform.position.x <= this.transform.position.x) {
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
		}
		if (Mathf.Abs(this.transform.position.x - playerChasing.transform.position.x) <= attackDistance && attackTimer >= attackTime) {
			attackTimer = 0;
			this.currentState = State.AttackPlayer;
		} else if (Mathf.Abs(this.transform.position.x - playerChasing.transform.position.x) >= loseDistance || Mathf.Abs(this.transform.position.y - playerChasing.transform.position.y) >= 3f) {
			this.currentState = State.Idle;
		}
	}

	void AttackPlayer(){
		attackZone.SetActive (true);
		this.currentState = State.Idle;
	}
}
