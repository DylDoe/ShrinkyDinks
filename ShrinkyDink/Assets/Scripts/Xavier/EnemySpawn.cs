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

	public AIO_LevelChange levelChangeScript;
	public GameObject curLevel;

	public GameObject spawnFX;

	public Vector3 rayHitPoint;

	//public float spawnWaitTimeAmnt = 1f;



	void Start () 
	{
		levelChangeScript = GameObject.Find("Game Manager").GetComponent<AIO_LevelChange>();
	}
	


	void Update () 
	{
		curLevel = GameObject.Find("SubLevel0" + levelChangeScript.levelCount);

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
					rayHitPoint = rayHit.point;
					Instantiate(spawnFX, new Vector3(this.transform.position.x, rayHit.point.y + 0.5f, this.transform.position.z), enemy.transform.rotation, this.transform);
					//Instantiate(enemy, new Vector3(this.transform.position.x, rayHit.point.y + 0.5f, this.transform.position.z), enemy.transform.rotation, curLevel.transform);//enemy collider height divided by 2
				}
			}
			else{Debug.Log("no enemy spawns");}

			timer = 0.0f;
		}

	}
}