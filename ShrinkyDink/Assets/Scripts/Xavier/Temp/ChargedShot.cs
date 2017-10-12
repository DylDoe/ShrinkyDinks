using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedShot : MonoBehaviour 
{
	public GameObject chargedBullet;
	float chargeTime;
	float maxChargeTime;
	float chargeDamageMultiplier;
	public bool isChargingShot;
	public GameObject potatao;
	public bool moveShot;
	public ChargedBullet chargedBulletScript;

	void Update () 
	{
		if(Input.GetButtonDown("Shoot"))
		{
			potatao = Instantiate(chargedBullet, transform.position, transform.rotation);
		}

		if (Input.GetButton("Shoot"))
		{
			potatao.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
		}

		if (Input.GetButtonUp("Shoot"))
		{
			moveShot = true;
		}

		if (moveShot == true)
		{
			
		}

	}
}
