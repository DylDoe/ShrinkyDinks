using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour 
{
	[Header("Player Jump Attributes")]
	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = 0.4f;

	[Header("Player Acceleration Times")]
	public float accelerationTimeAirborne = 0.2f;
	public float accelerationTimeGrounded = 0.1f;

	float moveSpeed = 6;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;

	[HideInInspector]
	public Vector2 directionalInput;

	float velocityXSmoothing;

	Controller2D controller;


// Dash Stuff

	[Header("Player Dash Attributes")]
	public float dashTime;
	public float dashDistance;
	public float dashCooldown;
	float dashRefreshTime;
	bool canDash;

	[HideInInspector]
	public bool isDashing;

	[HideInInspector]
	public float lastDirection;

	float dashSpeed;

	float timer;
	float timeSinceBegining; //



	void Start () 
	{
		controller = GetComponent<Controller2D>();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

		timer = 0.0f;
	}



	void Update()
	{
		if (!isDashing)
		{
			CalculateVeloctiy();
		}

		controller.Move(velocity * Time.deltaTime);

		if (controller.collisions.above || controller.collisions.below || isDashing)
		{
			velocity.y = 0;
		}	

		lastDirection = Mathf.Sign(velocity.x);

// Dash Stuff

		dashRefreshTime += Time.deltaTime;

		if (dashRefreshTime >= dashCooldown)
		{
			canDash = true;
		}
		else 
		{
			canDash = false;
		}

		if(!isDashing)
		{
			timeSinceBegining = Time.time;
		}


		DeterminingDashVelocity();


		if (isDashing)
		{
			timer = Time.time - timeSinceBegining;

			if (timer >= dashTime)
			{
				isDashing = false;
				dashRefreshTime = 0.0f;
			}

			//transform.Translate(Vector3.right * lastDirection * dashSpeed * Time.deltaTime);

			velocity =  Vector3.right * lastDirection * dashSpeed;


		} //
	}



	public void SetDirectionalInput (Vector2 input)
	{
		if (!isDashing)
		{
			directionalInput = input;
		}
	}



	public void OnJumpInputDown()
	{
		if (controller.collisions.below && !isDashing)
		{
			velocity.y = maxJumpVelocity;
		}
	}



	public void OnJumpInputUp()
	{
		if (velocity.y > minJumpVelocity && !isDashing)
		{
			velocity.y = minJumpVelocity;
		}
	}



	public void OnDashInput()
	{
		if (canDash)
		{
			isDashing = true;
		}
	}



	void DeterminingDashVelocity()
	{
		dashSpeed = dashDistance/dashTime;
	}



	void CalculateVeloctiy()
	{
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)? accelerationTimeGrounded: accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}
}
