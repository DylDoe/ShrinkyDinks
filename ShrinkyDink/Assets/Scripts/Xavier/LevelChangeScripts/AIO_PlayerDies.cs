using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIO_PlayerDies : MonoBehaviour 
{
	public GameObject subLevelButtonTrigger;
	public GameObject player;
	public GameObject downLevelChangerBox;
	public bool isPlayerDead = false;



	void Start()
	{
		this.GetComponent<Button>().onClick.AddListener(PlayerDeath);
	}



	void PlayerDeath()
	{
		downLevelChangerBox.transform.position = player.transform.position;

		isPlayerDead = true;
	}
}
