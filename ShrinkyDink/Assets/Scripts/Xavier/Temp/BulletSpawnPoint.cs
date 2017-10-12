using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPoint : MonoBehaviour 
{
	public Player playerScript;
	float xpos;

	void Update () 
	{
		if (playerScript.lastDirection < 0)
		{
			xpos = -0.75f;
			this.transform.localPosition = new Vector3(xpos, 0 , 0);
		}
		else
		{
			xpos = 0.75f; 
			this.transform.localPosition = new Vector3(xpos, 0 , 0);
		}
	}
}
