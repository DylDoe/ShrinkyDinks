using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel : MonoBehaviour {

	public float enemy;
	public float level;
	public float health;
	public float damage;


	// Use this for initialization
	void Start () {
		enemy = Random.Range (1, 4);
		if (enemy == 1) {
			//ground melee
			damage = 3 * level + (Random.Range(-level, (level * 2f)));
		} else if (enemy == 2) {
			//ground ranged
			damage = 2 * level + (Random.Range(-level, (level * 2f)));
		} else if (enemy == 3) {
			//flying melee
			damage = 2 * level + (Random.Range(-level, (level * 2f)));
		} else if (enemy == 4) {
			//flying ranged
			damage = 1 * level + (Random.Range(-level, (level * 2f)));
		}
		//can be changed per enemy but im leaving it like this for now
		health = level * 5 + (Random.Range(-level, (level * 2f)));


	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (this.gameObject);
		}

	}
}
