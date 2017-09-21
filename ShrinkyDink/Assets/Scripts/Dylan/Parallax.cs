using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
	public Transform[] backgrounds;

	//How much each background layer moves in relation to the player
	public float[] parallaxScales;
	public float smoothing;

	private Transform cam;
	private Vector3 previousCamPos;
	private float speedMultiplier = 0f;

	void Start () {
		cam = Camera.main.transform;

		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) 
		{
			speedMultiplier += 1;
			parallaxScales [i] = backgrounds [i].position.z * (speedMultiplier *-1);
	}
	}
	
	void LateUpdate () {
		for (int i = 0; i < backgrounds.Length; i++) 
		{
			//amount of movement
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
			//target pos for background to move to
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, backgroundTargetPos, smoothing * Time.deltaTime);
				 
		}
		previousCamPos = cam.position;
	}
}
