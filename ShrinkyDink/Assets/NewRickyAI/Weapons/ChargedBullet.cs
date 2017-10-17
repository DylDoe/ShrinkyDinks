using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBullet : MonoBehaviour {

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

	public bool startMoving;

	public Shoot shootScript;
	public Player playerScript;

	public float rotationMultiplier = 1f;

	public SpriteRenderer m_spriteRenderer;
	public Color m_initialColor;
	public Color m_newColor;
	public float timer;

	public float selfDestructTime;
	public GameObject overChargeEffect;
	public CircleCollider2D thisCircleCollider;

	public float bulletDir;
	public bool doOnce = true;

	public float chargeDmgDampner = 1f;
	public float chargeSelfDmgDampner = 1f;
	public float dmgItsGonDo;

	public GameObject bulletHitFX;

	public float chrgNeedToBrkShld = 2f;
	
	

	void Start()
	{
		shootScript = GetComponentInParent<Shoot>();
		playerScript = transform.root.GetComponent<Player>();
		PL = transform.root.GetComponent<PlayerLevel>();
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		m_initialColor = m_spriteRenderer.color;
		m_newColor = Color.red;
		thisCircleCollider = GetComponent<CircleCollider2D>();
		thisCircleCollider.enabled = false;
		doOnce = true;

		this.damage = PL.PRanged;
	}
	


	void Update () 
	{
		transform.Rotate(new Vector3(0,0,1) * rotationMultiplier * Time.deltaTime);

		dmgItsGonDo = damage * (timer / chargeDmgDampner);

		if (shootScript.moveChargedBullet)
		{
			startMoving = true;
		}

		if(startMoving == false)
		{
			thisCircleCollider.enabled = false;
			this.transform.localScale += new Vector3(0.31f * Time.deltaTime, 0.31f * Time.deltaTime, 0.31f * Time.deltaTime);
			rotationMultiplier -= 11f;
			m_initialColor = Color.Lerp(m_initialColor, m_newColor, 0.4f * Time.deltaTime);
			m_spriteRenderer.color = m_initialColor;
			timer += Time.deltaTime;
			if (timer >= selfDestructTime)
			{
				Instantiate(overChargeEffect, this.transform.position, this.transform.rotation);
				PL.PHealth -= Mathf.Clamp(damage * (timer / chargeSelfDmgDampner), PL.PRanged, PL.PRanged * 5f);
				Destroy(this.gameObject);
			}
		}

		if (startMoving)
		{
			if (doOnce)
			{
				TakePlayerOrientation();
				doOnce = false;
			}

			thisCircleCollider.enabled = true;

			if (bulletDir < 0)
			{
				this.transform.Translate (Vector3.left * speed * Time.deltaTime, Space.World);
			}
			else
			{
				this.transform.Translate (Vector3.right * speed * Time.deltaTime, Space.World);
			}

			Destroy (this.gameObject, 10f);

			transform.parent = null;
		}
		//Destroy bullet after 10 seconds
		
	}

	void TakePlayerOrientation()
	{
		bulletDir = shootScript.lastDirectionsLastDirection;
	}

	//Bullet collisions
	void OnTriggerEnter2D (Collider2D other){
		//Destroy bullet if it hits a platform or tries to leave screen
		//if (other.name == "Platform" || other.name == "ScreenEdge") {
		//	Destroy (this.gameObject);
			//if goThrough is active, do divided amount of damage to each enemy until goThroughNumber is zero, then destroy bullet
		// } else if (goThrough) {
		// 	if (goThroughNumber <= 0 && other.tag == "Enemy") {
		// 		EH = other.GetComponent<EnemyLevel> ();
		// 		BeforeFloat = EH.health;
		// 		EH.health -= damage/2;
		// 		AfterFloat = EH.health;
		// 		GiveExperience ();
		// 		Destroy (this.gameObject);
		// 	} else if (goThroughNumber > 0 && other.tag == "Enemy") {
		// 		goThroughNumber -= 1;
		// 		EH = other.GetComponent<EnemyLevel> ();
		// 		BeforeFloat = EH.health;
		// 		EH.health -= damage/2;
		// 		AfterFloat = EH.health;
		// 		GiveExperience ();
		// 	}
			//if goThrough is not active, do full damage and destroy bullet
		if (other.tag == "Enemy") 
		{
			if (other.gameObject.transform.Find("Shield"))
			{
				EnemyShield shieldScript = other.gameObject.transform.Find("Shield").GetComponent<EnemyShield>();

				if (timer > chrgNeedToBrkShld)
				{
					Destroy(other.gameObject.transform.Find("Shield").gameObject);
				}

				Destroy(this.gameObject);
				Instantiate(bulletHitFX, this.transform.position, this.transform.rotation);
				return;
			}

			EH = other.GetComponent<EnemyLevel> ();

			BeforeFloat = EH.health;
			EH.health -= Mathf.Clamp(damage * (timer / chargeSelfDmgDampner), PL.PRanged, PL.PRanged * 5f);
			AfterFloat = EH.health;
			GiveExperience ();
			Instantiate(bulletHitFX, this.transform.position, this.transform.rotation);

			//Destroy (this.gameObject);
		}

		if (other.tag == "BulletDestroyer")
		{
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