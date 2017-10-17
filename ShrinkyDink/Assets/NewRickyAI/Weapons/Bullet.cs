using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	//Speed at which bullet travels
	public float speed;
	//Damage bullet delivers
	public float damage;
	//How much to divide damage by when goThrough is active
	public float GoThroughDivider;
	//How many enemies to go through when goThrough is active
	public float goThroughNumber;
	//Before float for experience;
	public float BeforeFloat;
	//After float for experience;
	public float AfterFloat;
	//Bool for ease fo use
	public bool goThrough;
	//To get enemy script to apply damage
	public EnemyLevel EH;
	//To get player script to give experience
	public PlayerLevel PL;
	//if bullet should move left
	public bool left;

	public GameObject bulletHitFX;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (left) {
			//Make bullet move left
			this.transform.Translate (Vector3.left * speed * Time.deltaTime);
		} else {
			//Make bullet move right
			this.transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
		//Destroy bullet after 10 seconds
		Destroy (this.gameObject, 10f);
	}

	//Bullet collisions
	void OnTriggerEnter2D (Collider2D other){
		//Debug.Log(other.name);
		//Destroy bullet if it hits a platform or tries to leave screen
		if (other.name == "Platform" || other.name == "ScreenEdge" || other.tag != "Enemy") {
			Instantiate(bulletHitFX, this.transform.position, this.transform.rotation);
			Destroy (this.gameObject);
			//if goThrough is active, do divided amount of damage to each enemy until goThroughNumber is zero, then destroy bullet
		} else if (goThrough) {
			if (goThroughNumber <= 0 && other.tag == "Enemy") 
			{

				if (other.gameObject.transform.Find("Shield"))
			{
				other.gameObject.transform.Find("Shield").GetComponent<EnemyShield>();

				Destroy(this.gameObject);
				Instantiate(bulletHitFX, this.transform.position, this.transform.rotation);
				return;
			}

				EH = other.GetComponent<EnemyLevel> ();
				BeforeFloat = EH.health;
				EH.health -= damage/2;
				AfterFloat = EH.health;
				GiveExperience ();
				Instantiate(bulletHitFX, this.transform.position, this.transform.rotation);
				Destroy (this.gameObject);
			} else if (goThroughNumber > 0 && other.tag == "Enemy") 
			{
				if (other.gameObject.transform.Find("Shield"))
			{
				other.gameObject.transform.Find("Shield").GetComponent<EnemyShield>();

				Destroy(this.gameObject);
				Instantiate(bulletHitFX, this.transform.position, this.transform.rotation);
				return;
			}
				goThroughNumber -= 1;
				EH = other.GetComponent<EnemyLevel> ();
				BeforeFloat = EH.health;
				EH.health -= damage/2;
				AfterFloat = EH.health;
				GiveExperience ();
				Instantiate(bulletHitFX, this.transform.position, this.transform.rotation);
			}
			//if goThrough is not active, do full damage and destroy bullet
		} else if (!goThrough && other.tag == "Enemy") 
		{
			if (other.gameObject.transform.Find("Shield"))
			{
				other.gameObject.transform.Find("Shield").GetComponent<EnemyShield>();

				Destroy(this.gameObject);
				Instantiate(bulletHitFX, this.transform.position, this.transform.rotation);
				return;
			}
			EH = other.GetComponent<EnemyLevel> ();
			BeforeFloat = EH.health;
			EH.health -= damage;
			AfterFloat = EH.health;
			GiveExperience ();
			Instantiate(bulletHitFX, this.transform.position, this.transform.rotation);
			Destroy (this.gameObject);
		}
	}

	void GiveExperience(){
		if (AfterFloat < 0) {
			AfterFloat = 0;
		}
		PL.PExperience += BeforeFloat - AfterFloat;
		PL.ExperienceUp ();
	}
}