using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDesctruct : MonoBehaviour 
{
	public float timeBeforeDestroy = 5f;

	void Start () 
	{
		Destroy(this.gameObject, timeBeforeDestroy);
	}
}
