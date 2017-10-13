using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour 
{
	public GameObject[] Enemies;
	public GameObject enemy;
	public float timer;
	public float spawnEvery;
	public float percentageSpawnChance;
	public float rayCastLenght;
	public RaycastHit2D rayHit;
	public LayerMask whatLayersRayHits;



	void Start () 
	{
		
	}
	


	void Update () 
	{
		//Every couple seconds, check to see if an enemy spawns.
		timer += Time.deltaTime;

		if (timer >= spawnEvery)
		{
			if (Random.Range(0, 100) > (100 - percentageSpawnChance))
			{
				Debug.Log("enemy spawns");
				rayHit = Physics2D.Raycast(this.transform.position, Vector2.down, rayCastLenght, whatLayersRayHits);
				if (rayHit.collider != null)
				{
					Instantiate(enemy, new Vector3(this.transform.position.x, rayHit.point.y + 0.5f, this.transform.position.z), enemy.transform.rotation);//enemy collider height divided by 2
				}
			}
			else{Debug.Log("no enemy spawns");}

			timer = 0.0f;
		}

	}
}