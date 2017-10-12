using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIO_LevelBuilder : MonoBehaviour 
{
	public int currentMainLevel = 0;
	public int currentSubLevel = 0;

	public int levelLenght = 5;
	public GameObject[] levelPieces;

	public AIO_LevelChange levelChangeScript;



	void Start()
	{
			for (int i = 0; i < levelLenght; i++)
		{
			Instantiate(levelPieces[Random.Range(0,levelPieces.Length)], new Vector2(i * 100.0f, currentMainLevel * 100.0f), this.transform.rotation);
		}

		currentMainLevel += 1;
	}



	public void SubLevelLayoutBuilder()
	{
			for (int i = 0; i < levelLenght; i++)
		{
			Instantiate(levelPieces[Random.Range(0,levelPieces.Length)], new Vector2(i * 100.0f, levelChangeScript.levelCount * -100.0f), this.transform.rotation);
		}

		currentSubLevel += 1;
	}
}
