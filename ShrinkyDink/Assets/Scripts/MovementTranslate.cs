using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTranslate : MonoBehaviour 
{


	void Start () 
	{
		
	}
	

	void Update () 
	{
		transform.Translate(Vector3.right * Time.deltaTime);
	}
}
