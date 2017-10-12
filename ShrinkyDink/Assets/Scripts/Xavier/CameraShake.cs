using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour 
{
	public float shakeTimer;
	public float shakeAmount;

	public float shakeDuration;
	public float shakePower;



	void Update () 
	{
		if (shakeTimer >= 0)
		{
			Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;

			transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);

			shakeTimer -= Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.V))
		{
			ShakeCamera(shakePower, shakeDuration);
		}
	}



	public void ShakeCamera(float shakePower, float shakeDuration)
	{
		shakeAmount = shakePower;
		shakeTimer = shakeDuration;
	}
}
