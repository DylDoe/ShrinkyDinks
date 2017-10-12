using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour 
{
	Player player;
	Shoot shoot;

	Vector2 directionalInput;

	void Start()
	{
		player = GetComponent<Player>();
		shoot = GetComponent<Shoot>();
	}

	void Update()
	{
		if (this.name == "Player")
		{
			directionalInput = new Vector2(Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

			if (Input.GetButtonDown("Jump"))
			{
				player.OnJumpInputDown();
			}

			if (Input.GetButtonUp("Jump"))
			{
				player.OnJumpInputUp();
			}

			if (Input.GetButtonDown("Dash")) // "f"
			{
				player.OnDashInput();
			}

			if (Input.GetButtonUp("Shoot")) // "e"
			{
				shoot.SpawnBullet();
				shoot.FireChargedBullet();
			}

			if (Input.GetButton("Shoot")) // "e"
			{
				shoot.ChargeBullet();
			}
			
		}



		else if (this.name == "Player2")
		{
			directionalInput = new Vector2(Input.GetAxisRaw ("Horizontal2"), Input.GetAxisRaw ("Vertical2"));

			if (Input.GetButtonDown("Jump2"))
			{
				player.OnJumpInputDown();
			}

			if (Input.GetButtonUp("Jump2"))
			{
				player.OnJumpInputUp();
			}

			if (Input.GetButtonDown("Dash2")) // "num ."
			{
				player.OnDashInput();
			}

			if (Input.GetButtonUp("Shoot2")) // "num 3"
			{
				shoot.SpawnBullet();
				shoot.FireChargedBullet();
			}

			if (Input.GetButton("Shoot2")) // "num 3"
			{
				shoot.ChargeBullet();
			}
			
		}

		player.SetDirectionalInput (directionalInput);
	}


}
