using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyTimer : MonoBehaviour 
{
	public float difficultyTimer = 0.0f;
	public float enemyDifficultyLevel = 1f;
	public float difficultyIncreaseFrequency = 0.0f;
	
	public float subLvlDifficultBoost;

	public AIO_LevelChange lvlChngScript;

	void Update () 
	{
		difficultyTimer += Time.deltaTime;

		if (difficultyTimer >= difficultyIncreaseFrequency)
		{
			enemyDifficultyLevel += 1f;

			difficultyTimer = 0.0f;
		}

		if (lvlChngScript.levelCount == 0) { subLvlDifficultBoost = 0;}

		if (lvlChngScript.levelCount == 1) { subLvlDifficultBoost = 0;}

		if (lvlChngScript.levelCount == 2) { subLvlDifficultBoost = 1;}

		if (lvlChngScript.levelCount == 3) { subLvlDifficultBoost = 2;}

		if (lvlChngScript.levelCount >= 4) { subLvlDifficultBoost = lvlChngScript.levelCount - 1;}

	}
}
