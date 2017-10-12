using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerCameraFollow : MonoBehaviour 
{
	[HeaderAttribute("Player 1 & 2 scripts")]
	public Controller2D target;
	public Controller2D targetTwo; // 2 Player mode
	public Player playerScript;
	public Player playerScriptTwo; // 2 Player mode

	[Header("Camera offset & smoothing")]
	public float verticalOffset;
	public float lookAheadDistX;
	public float lookSmoothTimeX;
	public float verticalSmoothTime;

	[Header("WiggleBox sizes ( ͡° ͜ʖ ͡°)")]
	public Vector2 focusAreaSize;
	public Vector2 focusAreaSizeTwo; // 2 Player mode

	FocusArea focusArea;
	FocusAreaTwo focusAreaTwo; // 2 Player mode

	float currentLookAheadX;
	float targetLookAheadX;
	float lookAheadDirX;
	float smoothLookVelocityX;
	float smoothVelocityY;

	[Header("Camera sizes")]
	public float minCamViewSize;
	public float maxCamViewSize;
	//public float smoothCamSize;
	public float camSizeSmoothTime;

	float camViewSize; // 2 Player mode
	public Camera cam;

	bool lookAheadStopped;

	float distBetweenWiggleBoxes;
	float camSizeVelocity;
	//float lastDistBetweenWiggleBoxes;
	//public float deltaOfDistBetweenPlayers;
	//float camSizeLeft;

	// [Header("Player GameObjects")]
	// public GameObject player1;
	// public GameObject player2;

	// GameObject playerMostLeft;
	// GameObject playerMostRight;
	


	void Start()
	{
		focusArea = new FocusArea (target.playerCollider.bounds, focusAreaSize);
		focusAreaTwo = new FocusAreaTwo (targetTwo.playerCollider.bounds, focusAreaSizeTwo); // 2 Player mode
		//cam = this.GetComponent<Camera>();
	}



	void LateUpdate()
	{
		focusArea.Update (target.playerCollider.bounds);
		focusAreaTwo.Update (targetTwo.playerCollider.bounds); // 2 Player mode
		//PlayerPositions();

		Vector2 focusPosition = ((focusArea.center + focusAreaTwo.centerTwo) / 2) + Vector2.up * verticalOffset; // For 2 players Add the vertical distance between the playesr divided by 2.

		if ((focusArea.velocity.x + focusAreaTwo.velocityTwo.x) / 2 != 0) // Look ahead, if both players are going in the same direction, or if 1 is going in 1 direction. multiply their directions together to know the final direction, + + = +, + / = +, + - = -, - - = -.
		{
			lookAheadDirX = Mathf.Sign(focusArea.velocity.x + focusAreaTwo.velocityTwo.x);
			
			if (Mathf.Sign(playerScript.directionalInput.x) == Mathf.Sign(playerScriptTwo.directionalInput.x) && Mathf.Sign(playerScript.directionalInput.x) == Mathf.Sign(focusArea.velocity.x) && (Mathf.Sign(playerScriptTwo.directionalInput.x) == Mathf.Sign(focusAreaTwo.velocityTwo.x)) && playerScript.directionalInput.x != 0 && playerScriptTwo.directionalInput.x != 0)
			{
				lookAheadStopped = false;
				targetLookAheadX = lookAheadDirX * lookAheadDistX;
			}
			else
			{
				if(!lookAheadStopped)
				{
					lookAheadStopped = true;
					targetLookAheadX = currentLookAheadX +(lookAheadDirX * lookAheadDistX - currentLookAheadX)/4f;
				}
			}
		}

		currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

		focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
		focusPosition += Vector2.right * currentLookAheadX;
		transform.position = (Vector3)focusPosition + Vector3.forward * -10;

		//targetLookAheadX = lookAheadDirX * lookAheadDistX;

		//deltaOfDistBetweenPlayers = distBetweenWiggleBoxes - lastDistBetweenWiggleBoxes;

		//camSizeLeft = (maxCamViewSize - cameraSize.orthographicSize);

		distBetweenWiggleBoxes = Mathf.Clamp(Vector2.Distance(focusArea.center, focusAreaTwo.centerTwo), minCamViewSize, maxCamViewSize);

		//cameraSize.orthographicSize = Mathf.SmoothDamp(distBetweenWiggleBoxes, lastDistBetweenWiggleBoxes/4, ref deltaOfDistBetweenPlayers, camSizeSmoothTime); // check mathfsmooth damp, on unity website
		cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, distBetweenWiggleBoxes, ref camSizeVelocity, camSizeSmoothTime); // check mathfsmooth damp, on unity website

		//lastDistBetweenWiggleBoxes = distBetweenWiggleBoxes;
	}



	void OnDrawGizmos()
	{
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		Gizmos.DrawCube (focusArea.center, focusAreaSize);

		Gizmos.color = new Color(0, 0, 1, 0.5f); // 2 Player mode
		Gizmos.DrawCube (focusAreaTwo.centerTwo, focusAreaSizeTwo); // 2 Player mode
	}



	struct FocusArea 
	{
		public Vector2 center;
		public Vector2 velocity;
		float left, right;
		float top, bottom;

		public FocusArea(Bounds targetBounds, Vector2 size)
		{
			left = targetBounds.center.x - size.x/2;
			right = targetBounds.center.x + size.x/2;
			bottom = targetBounds.min.y;
			top = targetBounds.min.y + size.y;

			velocity = Vector2.zero;
			center = new Vector2((left + right) / 2, (top + bottom) / 2);
		}

		public void Update(Bounds targetBounds)
		{
			float shiftX = 0;
			if (targetBounds.min.x < left)
			{
				shiftX = targetBounds.min.x - left;
			}
			else if (targetBounds.max.x > right)
			{
				shiftX = targetBounds.max.x - right;
			}
			left += shiftX;
			right += shiftX;

			float shiftY = 0;
			if (targetBounds.min.y < bottom)
			{
				shiftY = targetBounds.min.y - bottom;
			}
			else if (targetBounds.max.y > top)
			{
				shiftY = targetBounds.max.y - top;
			}
			bottom += shiftY;
			top += shiftY;
			center = new Vector2((left + right) / 2, (top + bottom) / 2);
			velocity = new Vector2(shiftX, shiftY);
		}
	}



	// 2 Player mode
		struct FocusAreaTwo
	{
		public Vector2 centerTwo;
		public Vector2 velocityTwo;
		float leftTwo, rightTwo;
		float topTwo, bottomTwo;

		public FocusAreaTwo(Bounds targetBoundsTwo, Vector2 sizeTwo)
		{
			leftTwo = targetBoundsTwo.center.x - sizeTwo.x/2;
			rightTwo = targetBoundsTwo.center.x + sizeTwo.x/2;
			bottomTwo = targetBoundsTwo.min.y;
			topTwo = targetBoundsTwo.min.y + sizeTwo.y;

			velocityTwo = Vector2.zero;
			centerTwo = new Vector2((leftTwo + rightTwo) / 2, (topTwo + bottomTwo) / 2);
		}

		public void Update(Bounds targetBoundsTwo)
		{
			float shiftXTwo = 0;
			if (targetBoundsTwo.min.x < leftTwo)
			{
				shiftXTwo = targetBoundsTwo.min.x - leftTwo;
			}
			else if (targetBoundsTwo.max.x > rightTwo)
			{
				shiftXTwo = targetBoundsTwo.max.x - rightTwo;
			}
			leftTwo += shiftXTwo;
			rightTwo += shiftXTwo;

			float shiftYTwo = 0;
			if (targetBoundsTwo.min.y < bottomTwo)
			{
				shiftYTwo = targetBoundsTwo.min.y - bottomTwo;
			}
			else if (targetBoundsTwo.max.y > topTwo)
			{
				shiftYTwo = targetBoundsTwo.max.y - topTwo;
			}
			bottomTwo += shiftYTwo;
			topTwo += shiftYTwo;
			centerTwo = new Vector2((leftTwo + rightTwo) / 2, (topTwo + bottomTwo) / 2);
			velocityTwo = new Vector2(shiftXTwo, shiftYTwo);
		}
	}

	// public void PlayerPositions()
	// {
	// 	if (player1.transform.position.x > player2.transform.position.x)
	// 	{
	// 		playerMostLeft = player2;
	// 		playerMostRight = player1;
	// 	}
	// 	else
	// 	{
	// 		playerMostLeft = player1;
	// 		playerMostRight = player2;
	// 	}
	// }

}
