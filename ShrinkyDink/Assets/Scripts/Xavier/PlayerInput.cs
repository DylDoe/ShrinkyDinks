using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour 
{
	Player player;
	Shoot shoot;

	Vector2 directionalInput;

	private bool RT_AxisInUse;
	private bool RT_AxisInUse2;

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

			if (Input.GetButtonDown("Interact"))
			{
				// bla bla 
				Debug.Log("Pressed the interact button yo");
			}



// Trying to use trigger as button.

			if (Input.GetAxisRaw("ShootController") != 0.0f) // Get button down but I want just getbutton
			{
				Debug.Log("RTrigger pressed");
				if (RT_AxisInUse == false)
				{

					RT_AxisInUse = true;
					Debug.Log("Should shoot charge bullet");
				}
			}



			if (Input.GetAxisRaw("ShootController") != 0.0f && RT_AxisInUse == true)
			{
				shoot.ChargeBullet();
			}



			if (Input.GetAxisRaw("ShootController") <= 0.0f && RT_AxisInUse == true) // Get button Up
			{
				shoot.SpawnBullet();
				shoot.FireChargedBullet();
				RT_AxisInUse = false;

				Debug.Log("RTrigger is not pressed anymore");
			}

// private bool m_isAxisInUse = false;
 
//  void Update()
//  {
//      if( Input.GetAxisRaw("Fire1") != 0)
//      {
//          if(m_isAxisInUse == false)
//          {
//              // Call your event function here.
//              m_isAxisInUse = true;
//          }
//      }
//      if( Input.GetAxisRaw("Fire1") == 0)
//      {
//          m_isAxisInUse = false;
//      }    
//  }
			
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

			if (Input.GetButtonDown("Interact2"))
			{
				// bla bla 
				Debug.Log("Pressed the interact 2 button yo");
			}




						
			// right trigger	
			if (Input.GetAxisRaw("ShootController2") != 0.0f) // Get button down but I want just getbutton
			{
				Debug.Log("RTrigger pressed");
				if (RT_AxisInUse2 == false)
				{

					RT_AxisInUse2 = true;
					Debug.Log("Should shoot charge bullet");
				}
			}



			if (Input.GetAxisRaw("ShootController2") != 0.0f && RT_AxisInUse2 == true)
			{
				shoot.ChargeBullet();
			}



			if (Input.GetAxisRaw("ShootController2") <= 0.0f && RT_AxisInUse2 == true) // Get button Up
			{
				shoot.SpawnBullet();
				shoot.FireChargedBullet();
				RT_AxisInUse2 = false;

				Debug.Log("RTrigger is not pressed anymore");
			}
			
		}

		player.SetDirectionalInput (directionalInput);
	}


}
