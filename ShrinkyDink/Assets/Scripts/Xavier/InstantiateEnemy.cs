using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemy : MonoBehaviour 
{
	public GameObject enemy;
	public GameObject shieldEnemy;
	public AIO_LevelChange levelChangeScript;
	public EnemySpawn enemySpawnScript;
	public GameObject curLevel;
	public float whatISpawn;

	[HeaderAttribute("Spawn Enemy Wait Time")]
	public float spawnWaitTimer;
	public float afterFXSpawn;
	private bool doISpawn = false;


	void Start () 
	{
		levelChangeScript = GameObject.Find("Game Manager").GetComponent<AIO_LevelChange>();
		enemySpawnScript = transform.parent.GetComponent<EnemySpawn>();
		this.transform.parent = null;

		whatISpawn = Random.Range(0,100);
	}
	

	void Update () 
	{
		curLevel = GameObject.Find("SubLevel0" + levelChangeScript.levelCount);

		if (doISpawn == false)
		{
			spawnWaitTimer += Time.deltaTime;
		}
		
		if (spawnWaitTimer >= afterFXSpawn)
		{
			doISpawn = true;
		}

		if (doISpawn)
		{
			if (whatISpawn <= 80)
			{
				Instantiate(enemy, new Vector3(enemySpawnScript.rayHitPoint.x, enemySpawnScript.rayHitPoint.y + 0.5f, enemySpawnScript.rayHitPoint.z), enemy.transform.rotation, curLevel.transform);//enemy collider height divided by 2
			}
			if (whatISpawn >= 81)
			{
				Instantiate(shieldEnemy, new Vector3(enemySpawnScript.rayHitPoint.x, enemySpawnScript.rayHitPoint.y + 0.5f, enemySpawnScript.rayHitPoint.z), enemy.transform.rotation, curLevel.transform);//enemy collider height divided by 2
			}
			doISpawn = false;
			spawnWaitTimer = 0.0f;
		}
	}
}
