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



	void Start () 
	{
		controller = GetComponent<Controller2D>();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
		print ("Gravity: " + gravity + " Jump Velocity: " + maxJumpVelocity);
	}



	void Update()
	{
		CalculateVeloctiy();

		controller.Move(velocity * Time.deltaTime);

		if (controller.collisions.above || controller.collisions.below)
		{
			velocity.y = 0;
		}	
	}



	public void SetDirectionalInput (Vector2 input)
	{
		directionalInput = input;
	}



	public void OnJumpInputDown()
	{
		if (controller.collisions.below)
		{
			velocity.y = maxJumpVelocity;
		}
	}



	public void OnJumpInputUp()
	{
		if (velocity.y > minJumpVelocity)
		{
			velocity.y = minJumpVelocity;
		}
	}



	void CalculateVeloctiy()
	{
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)? accelerationTimeGrounded: accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}
}
