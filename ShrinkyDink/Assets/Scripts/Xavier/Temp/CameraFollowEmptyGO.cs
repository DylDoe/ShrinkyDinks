using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowEmptyGO : MonoBehaviour 
{
	public GameObject other;

	

	void Update () 
	{
		this.transform.position = other.transform.position;
	}
}
