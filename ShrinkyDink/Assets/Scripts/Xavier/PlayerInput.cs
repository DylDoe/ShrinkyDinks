using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour 
{
	Player player;

	Vector2 directionalInput;

	void Start()
	{
		player = GetComponent<Player>();
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
		}

		player.SetDirectionalInput (directionalInput);
	}


}
