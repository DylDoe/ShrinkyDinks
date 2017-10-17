using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSelfInactive : MonoBehaviour 
{

	public float timer;
	public float timeSetSelfInactive;

	void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= timeSetSelfInactive)
		{
			this.gameObject.SetActive(false);	
			timer = 0.0f;
		}
	}

}
