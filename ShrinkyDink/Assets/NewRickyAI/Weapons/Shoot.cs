using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	//Some shit
	public GameObject Bullet;
	public Bullet bullet;
	public PlayerLevel playerLevel;
	//Amount of enemies to go through
	public float PlayerVarForGoThrough;
	//Amount of damage the bullet does
	public float PlayerVarForDamage;
	//Player Script to refenrence the player's last direction to know which way to shoot.
	public Player playerScript;

	//Charged shot shit
	public float chargeTime;
	public float maxChargeTime;
	public float chargeDamageMultiplier;
	public bool isChargingShot;
	public GameObject chargedBulletObj;

	public ChargedBullet chargeBulletScript;

	public GameObject bulletSpawnPoint;

	public bool moveChargedBullet;

	public bool isChargingBulletSpawned;

	public float lastDirectionsLastDirection;
	



	// Use this for initialization
	void Start () 
	{
		playerLevel = this.GetComponent<PlayerLevel> ();
		playerScript = this.GetComponent<Player>();
	}
	
	// Update is called once per frame
	public void SpawnBullet () 
	{
		if(playerScript.isDashing == false && isChargingShot == false)
		{
			//Simple press button and instantiate bullet
			bullet = Bullet.GetComponent<Bullet> ();
			//How many enemies the bullet goes through is equal to the amount set by player (zero means get destroyed on first contact)
			bullet.goThroughNumber = PlayerVarForGoThrough;
			//Set bullet damage to player reference
			bullet.damage = playerLevel.PRanged;
			//Set goThrough bool on bullet for ease
			if(PlayerVarForGoThrough <= 0){
				bullet.goThrough = false;
			} else {
				bullet.goThrough = true;
			}

			bullet.PL = this.gameObject.GetComponent<PlayerLevel> ();
			//Instantiate the bullet
			if (playerScript.lastDirection < 0)
			{
				Instantiate (Bullet, bulletSpawnPoint.transform.position, transform.rotation * Quaternion.Euler(0, 180, 0));
			}
			else 
			{
				Instantiate (Bullet, bulletSpawnPoint.transform.position, transform.rotation);
			}
		}
	}


	public void ChargeBullet()
	{
		if(playerScript.isDashing == false)
		{
			chargeTime += Time.deltaTime;

			if (chargeTime >= 0.2f)
			{
				isChargingShot = true;
				moveChargedBullet = false;

				if(!isChargingBulletSpawned)SpawnOneBullet();
			}


		}
	}


	void SpawnOneBullet()
	{
		Instantiate (chargedBulletObj, bulletSpawnPoint.transform.position, transform.rotation, bulletSpawnPoint.transform);

		isChargingBulletSpawned = true;
	}


	public void FireChargedBullet()
	{
		if (chargeTime <= 0.2f)
		{
			chargeTime = 0.0f;
		}

		if(isChargingShot == true)
		{	
			lastDirectionsLastDirection = playerScript.lastDirection;

			moveChargedBullet = true;

			isChargingBulletSpawned = false;
			
			isChargingShot = false;

			chargeTime = 0.0f;

			
		}
	}
}
