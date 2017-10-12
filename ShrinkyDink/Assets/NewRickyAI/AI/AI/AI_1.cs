using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_1 : MonoBehaviour {

	public GameObject[] Player;
	public GameObject PlayerChasing;

	public float speed;
	public float jump;
	public float AttackDistance;

	public State currentState;
	private State defaultState = State.Idle;

	private UnityEngine.AI.NavMeshAgent Enemy;

	public enum State {
		Idle,
		FindPlayer,
		GoToPlayer,
		AttackPlayer,
		TakeDamage,
		Die,
	}
		
	void Start () {
		currentState = defaultState;
		//RunStates ();
	}

	void Update () {
		RunStates ();
	}

	void RunStates (){
		switch (this.currentState) {
		case State.Idle: this.Idle(); break;
		case State.FindPlayer: this.FindPlayer(); break;
		case State.GoToPlayer: this.GoToPlayer(); break;
		case State.AttackPlayer: this.AttackPlayer (); break;
		case State.TakeDamage: this.TakeDamage (); break;
		case State.Die: this.Die (); break;
		}
	}

	void Idle (){
		this.currentState = State.FindPlayer;
		//RunStates ();
	}

	void FindPlayer(){
		Player = GameObject.FindGameObjectsWithTag("Player");
		this.PlayerChasing = Player[Random.Range(0, Player.Length)];
		this.currentState = State.GoToPlayer;
		//RunStates();
	}

	void GoToPlayer (){
		if (this.transform.position.x <= PlayerChasing.transform.position.x) {
			this.transform.Translate (Vector2.right * speed * Time.deltaTime);
			Debug.Log ("Move Right");
		} else {
			this.transform.Translate (Vector2.left * speed * Time.deltaTime);
			Debug.Log ("Move Left");
		}

		//if( Vector3.Distance(this.transform.position, this.Enemy.destination) <= AttackDistance){
		//		AttackPlayer();
		//	}
	}

	void AttackPlayer () {
				
	}

	void TakeDamage () {

	}

	void Die () {

	}

	public void Jump(){
		this.transform.Translate (Vector2.up * jump * Time.deltaTime);
	}

	/*void OnTriggerEnter(Collider other){
		Debug.Log ("Touched");
		if (other.tag == "Enemy" && other.transform.position.y < PlayerChasing.transform.position.y) {
			//ai1 = other.gameObject.GetComponent<AI_1> ();
			Jump ();
		}
	}*/
}
