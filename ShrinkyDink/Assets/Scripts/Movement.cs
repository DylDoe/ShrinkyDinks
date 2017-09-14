using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour 
{
	private Rigidbody2D rb2d;
	public float moveSpeed;
	public float jumpSpeed;
	public string theCurrentPlayerState;
	public bool isGrounded;
	public float maxSpeed;
	public Vector2 direction;
	private Vector2 jump;

	enum State
	{
		State_Idle,
		State_Running,
		State_Jumping
	}
	State currentState;

	void Start () 
	{
		this.currentState = State.State_Idle;

		rb2d = this.GetComponent<Rigidbody2D>();
	}

	void Update () 
	{
		switch (this.currentState)
		{
			case State.State_Idle: this.Idle(); break;
			case State.State_Running: this.Running(); break;
			case State.State_Jumping: this.Jumping(); break;
		}
		//rb2d.AddForce(Vector3.right * moveSpeed * Time.deltaTime, ForceMode2D.Force);
		theCurrentPlayerState = currentState.ToString();
	}

	void FixedUpdate()
	{
		Actions();
	}

	void Idle()
	{
		if (this.rb2d.velocity != Vector2.zero)
		{
			this.currentState = State.State_Running;
			return;
		}
		//Play idle animation here;
	}

	void Running()
	{
		if (this.rb2d.velocity.normalized == Vector2.zero)
		{
			this.currentState = State.State_Idle;
			return;
		}
		//Play running animation here;
	}

	void Jumping()
	{
		//if (isGrounded == false)
		// {
		//		
		// }
	}

	void Actions()
	{
		// if (Input.GetKey("d"))
		// { rb2d.AddForce(Vector3.right * moveSpeed * Time.deltaTime, ForceMode2D.Force); }

		// if (Input.GetKey("a"))
		// { rb2d.AddForce(Vector3.left * moveSpeed * Time.deltaTime, ForceMode2D.Force); }

		direction = Vector2.zero;

		if (Input.GetKey("d"))
		{ direction.x = 1; }

		if (Input.GetKey("a"))
		{ direction.x = -1; }

		if (Input.GetKeyDown("space"))
		{ direction += Vector2.up * jumpSpeed; }

		if (direction != Vector2.zero)
		{ rb2d.velocity = direction * moveSpeed * Time.deltaTime; }

		//  if (direction != Vector2.zero && rb2d.velocity.x < maxSpeed)
		//  { rb2d.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode2D.Force); } 
	}

}
