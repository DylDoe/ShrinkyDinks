using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIO_LevelChange : MonoBehaviour 
{
	GameObject player1;
	GameObject player2;

	public Vector3 currentSpawnLoc1 = new Vector3 (3, 5, 0);
	public Vector3 currentSpawnLoc2 = new Vector3 (6, 5, 0);

	Vector3  nextSpawnLoc1;
	Vector3  nextSpawnLoc2;

	Vector3  previousSpawnLoc1;
	Vector3  previousSpawnLoc2;

	public Vector3 distBetweenLevels;

	public GameObject currentLevelObj;
	public int levelCount;

	public GameObject downLevelChanger;
	public GameObject upLevelChanger;

	public int levelLenght = 10;
	public int subLevelLenght = 3;
	public GameObject[] levelPieces;

	public GameObject previousLevel; // TURN IT INTO AN ARRAY YO
	public GameObject currentLevel;

	//public GameObject[] previousLevels;

	public List<GameObject> previousLevels = new List<GameObject>();

	//public Vector2 previousLevelPlayerPos1;
	//public Vector2 previousLevelPlayerPos2;

	public List<GameObject> previousLevelPlayerPositions1 = new List<GameObject>();
	public List<GameObject> previousLevelPlayerPositions2 = new List<GameObject>();

	GameObject previousLevelPlayer1PosToDestroy;
	GameObject previousLevelPlayer2PosToDestroy;

	public AIO_PlayerDies player1DiesScript;
	public AIO_PlayerDies player2DiesScript;

	public PlayerLevel PL1;
	public PlayerLevel PL2;

	//public List<Vector3> previousLevelPlayerPositions1 = new List<Vector3>();
	//public List<Vector3> previousLevelPlayerPositions2 = new List<Vector3>();

	//public GameObject newLevel;

	//public AIO_LevelBuilder levelBuilderScript;

	//public int currentMainLevel = 0;
	//public int currentSubLevel = 0;

	public TurnOffOtherScripts scriptsOnOff1;
	public TurnOffOtherScripts scriptsOnOff2;

	public GameObject player1DeadFX;
	public GameObject player2DeadFX;


	void Start () 
	{
		player1 = GameObject.FindGameObjectWithTag("Player");
		player2 = GameObject.FindGameObjectWithTag("Player2");

		PL1 = player1.GetComponent<PlayerLevel>();
		PL2 = player2.GetComponent<PlayerLevel>();

		scriptsOnOff1 = player1.GetComponent<TurnOffOtherScripts>();
		scriptsOnOff2 = player2.GetComponent<TurnOffOtherScripts>();

		nextSpawnLoc1 = currentSpawnLoc1 - distBetweenLevels;
		nextSpawnLoc2 = currentSpawnLoc2 - distBetweenLevels;

		previousLevels.Add(new GameObject("SubLevel0" + levelCount));
		currentLevel = GameObject.Find("SubLevel0" + levelCount);

		previousLevelPlayerPositions1.Add(new GameObject("player1SpawnPosition" + levelCount));
		previousLevelPlayerPositions2.Add(new GameObject("player2SpawnPosition" + levelCount));
		previousLevelPlayer1PosToDestroy = GameObject.Find("player1SpawnPosition" + levelCount);
		previousLevelPlayer2PosToDestroy = GameObject.Find("player2SpawnPosition" + levelCount);
		
		for (int i = 0; i < levelLenght; i++)
		{
			Instantiate(levelPieces[Random.Range(0,levelPieces.Length)], new Vector2(i * 100.0f, levelCount * 100.0f), this.transform.rotation, GameObject.Find("SubLevel0" + levelCount).transform);
		}

		float upLvlChngXpos = upLevelChanger.transform.position.x;

		upLvlChngXpos = levelLenght * 100f;

		upLevelChanger.transform.position = new Vector3(upLvlChngXpos, upLevelChanger.transform.position.y, upLevelChanger.transform.position.z);
	}
	


	void Update () 
	{
		if (Vector2.Distance(downLevelChanger.transform.position, player1.transform.position) <= 2.5f && Vector2.Distance(downLevelChanger.transform.position, player2.transform.position) <= 2.5f)
		{
			if ((Input.GetButtonDown("Interact")) && player2DiesScript.isPlayerDead == true)
			{
				MovePlayersToNewLevel();
			}

			if ((Input.GetButtonDown("Interact2")) && player1DiesScript.isPlayerDead == true)
			{
				MovePlayersToNewLevel();
			}
		}


		if ( player1DiesScript.isPlayerDead == true && player2DiesScript.isPlayerDead == true)
		{
			Application.Quit();
			Debug.Log("YouGuysPoopedUp");
		}

		if (PL1.PHealth <= 0f)
		{
			downLevelChanger.transform.position = player1.transform.position;
			player1.GetComponent<TurnOffOtherScripts>().TurnOffPlayerScripts();
			player1DeadFX.SetActive(true);	
		}
		if (PL1.PHealth >= 0f) {player1DeadFX.SetActive(false);}

		if (PL2.PHealth <= 0f)
		{
			downLevelChanger.transform.position = player2.transform.position;
			player2.GetComponent<TurnOffOtherScripts>().TurnOffPlayerScripts();
			player2DeadFX.SetActive(true);
		}
		if (PL2.PHealth >= 0f) {player2DeadFX.SetActive(false);}

		if (PL1.PHealth<= 0f && PL2.PHealth<= 0f)
		{
			Debug.Log("Both players dead.");
			Application.Quit();
		}

		// for (int i = 0; i < levelLenght; i++)
		// {
		// 	Instantiate(levelPieces[Random.Range(0,levelPieces.Length)], new Vector2(i * 100.0f, currentLevel * 100.0f), this.transform.rotation, GameObject.Find("SubLevel0" + currentLevel).transform);
		// }

		// if (Vector2.Distance(upLevelChanger.transform.position, player1.transform.position) <= 5f && Vector2.Distance(upLevelChanger.transform.position, player2.transform.position) <= 5f)
		// {
		// 	MovePlayersToNewLevel();
		// }
	}



	public void MovePlayersToNewLevel()
	{
		//Instantiate(newLevel, currentLevelObj.transform.position, currentLevelObj.transform.rotation);

		// previousLevelPlayerPos1 = player1.transform.position;
		// previousLevelPlayerPos2 = player2.transform.position;

		levelCount += 1;

		//new GameObject("player1SpawnPosition" + levelCount);
		//new GameObject("player2SpawnPosition" + levelCount);

		previousLevelPlayerPositions1.Add(new GameObject("player1SpawnPosition" + levelCount));
		previousLevelPlayerPositions2.Add(new GameObject("player2SpawnPosition" + levelCount));

		GameObject.Find("player1SpawnPosition" + levelCount).transform.position = player1.transform.position;
		GameObject.Find("player2SpawnPosition" + levelCount).transform.position = player2.transform.position;
 
		player1.transform.position = currentLevelObj.transform.position + nextSpawnLoc1;
		player2.transform.position = currentLevelObj.transform.position + nextSpawnLoc2;

		currentSpawnLoc1 = nextSpawnLoc1;
		currentSpawnLoc2 = nextSpawnLoc2;

		nextSpawnLoc1 = currentSpawnLoc1 - distBetweenLevels;
		nextSpawnLoc2 = currentSpawnLoc2 - distBetweenLevels;

		previousSpawnLoc1 = currentSpawnLoc1 + distBetweenLevels;
		previousSpawnLoc2 = currentSpawnLoc2 + distBetweenLevels;

		player1DiesScript.isPlayerDead = false;
		player2DiesScript.isPlayerDead = false;

		SubLevelLayoutBuilder();
	}



	public void SubLevelLayoutBuilder()
	{
		previousLevels.Add(new GameObject("SubLevel0" + levelCount));

		if (levelCount == 1)	{subLevelLenght = 3;}
		if (levelCount == 2)	{subLevelLenght = 2;}
		if (levelCount >= 3)	{subLevelLenght = 1;}

			for (int i = 0; i < subLevelLenght; i++)
		{
			Instantiate(levelPieces[Random.Range(0,levelPieces.Length)], new Vector2(i * 100.0f + i * levelCount, levelCount * -100.0f), this.transform.rotation, GameObject.Find("SubLevel0" + levelCount).transform);
		}

		downLevelChanger.transform.position = downLevelChanger.transform.position - distBetweenLevels;

		//upLevelChanger.transform.position = upLevelChanger.transform.position - distBetweenLevels;
		float upLvlChngXpos = upLevelChanger.transform.position.x;

		upLvlChngXpos = subLevelLenght * 100f;

		upLevelChanger.transform.position = new Vector3(upLvlChngXpos, upLevelChanger.transform.position.y - distBetweenLevels.y, upLevelChanger.transform.position.z);

		//previousLevel = GameObject.Find("SubLevel0" + (levelCount - 1));
		currentLevel = GameObject.Find("SubLevel0" + levelCount);
		previousLevelPlayer1PosToDestroy = GameObject.Find("player1SpawnPosition" + levelCount);
		previousLevelPlayer2PosToDestroy = GameObject.Find("player2SpawnPosition" + levelCount);
		GameObject.Find("SubLevel0" + (levelCount - 1)).SetActive(false);

		scriptsOnOff1.TurnOnPlayerScripts();
		PL1.PHealth = PL1.PHealthMax;
		scriptsOnOff2.TurnOnPlayerScripts();
		PL2.PHealth = PL2.PHealthMax;
	}



	public void MovePlayersBackALevel()
	{
		//player1.transform.position = currentLevelObj.transform.position + previousSpawnLoc1;
		//player2.transform.position = currentLevelObj.transform.position + previousSpawnLoc2;

		player1.transform.position = previousLevelPlayerPositions1.Find(x => x.name == ("player1SpawnPosition" + (levelCount))).transform.position;	// player pos is = to playerposlist index (levelcount)
		player2.transform.position = previousLevelPlayerPositions2.Find(x => x.name == ("player2SpawnPosition" + (levelCount))).transform.position;

		levelCount -= 1;

		currentSpawnLoc1 = previousSpawnLoc1;
		currentSpawnLoc2 = previousSpawnLoc2;

		nextSpawnLoc1 = currentSpawnLoc1 - distBetweenLevels;
		nextSpawnLoc2 = currentSpawnLoc2 - distBetweenLevels;

		previousSpawnLoc1 = currentSpawnLoc1 + distBetweenLevels;
		previousSpawnLoc2 = currentSpawnLoc2 + distBetweenLevels;

		downLevelChanger.transform.position = downLevelChanger.transform.position + distBetweenLevels;

		upLevelChanger.transform.position = upLevelChanger.transform.position + distBetweenLevels;

		previousLevels.Find(x => x.name == ("SubLevel0" + (levelCount))).SetActive(true);

		Destroy(currentLevel);

		previousLevels.RemoveAt(levelCount + 1);

		Destroy(previousLevelPlayer1PosToDestroy);
		Destroy(previousLevelPlayer2PosToDestroy);

		previousLevelPlayerPositions1.RemoveAt(levelCount + 1);
		previousLevelPlayerPositions2.RemoveAt(levelCount + 1);

		currentLevel = GameObject.Find("SubLevel0" + levelCount);
		previousLevelPlayer1PosToDestroy = GameObject.Find("player1SpawnPosition" + levelCount);
		previousLevelPlayer2PosToDestroy = GameObject.Find("player2SpawnPosition" + levelCount);

		//previousLevel = GameObject.Find("SubLevel0" + (levelCount - 1));
	}
}
