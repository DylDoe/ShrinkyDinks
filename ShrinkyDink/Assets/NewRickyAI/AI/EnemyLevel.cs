using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel : MonoBehaviour {

	public float enemy;
	public float level;
	public float health;
	public float damage;

	public DifficultyTimer diffScript;


	public float baseDamage = 3f;

	public float rngDamagePerEnemy = 0.5f;



	public float baseHealth = 5f;

	public float rngHealthPerEnemy = 1.5f;


	// Use this for initialization
	void Start () 
	{
		diffScript = GameObject.Find("Game Manager").GetComponent<DifficultyTimer>();

		level = diffScript.enemyDifficultyLevel + diffScript.subLvlDifficultBoost;
		//enemy = Random.Range (1, 4);

		//if (enemy == 1) 
		//{
			//ground melee
			damage = baseDamage * level + (Random.Range(-level, (level * rngDamagePerEnemy)));
		//}
		// } else if (enemy == 2) 
		// {
		// 	//ground ranged
		// 	damage = 2 * level + (Random.Range(-level, (level * 2f)));

		// } else if (enemy == 3) 
		// {
		// 	//flying melee
		// 	damage = 2 * level + (Random.Range(-level, (level * 2f)));

		// } else if (enemy == 4) 
		// {
		// 	//flying ranged
		// 	damage = 1 * level + (Random.Range(-level, (level * 2f)));

		// }
		//can be changed per enemy but im leaving it like this for now
		health = baseHealth * level + (Random.Range(-level, (level * rngHealthPerEnemy)));


	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0) {
			Destroy (this.gameObject);
		}

	}
}
