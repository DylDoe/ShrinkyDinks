using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour 
{
	public float dashTime;
	public float dashDistance;
	public bool isDashing;

	float dashSpeed;

	float timer;
	float timeSinceBegining;



	void Start () 
	{
		timer = 0.0f;
	}


	
	void Update () 
	{
		if(!isDashing)
		{
			timeSinceBegining = Time.time;
		}


		DeterminingDashVelocity();


		if (Input.GetButtonDown("Dash")) // "f"
		{
			isDashing = true;
		}


		if (isDashing)
		{
			timer = Time.time - timeSinceBegining;

			if (timer >= dashTime)
			{
				isDashing = false;
			}

			transform.Translate(Vector2.right * dashSpeed * Time.deltaTime);
		}
	}



	void DeterminingDashVelocity()
	{
		dashSpeed = dashDistance/dashTime;
	}
}
