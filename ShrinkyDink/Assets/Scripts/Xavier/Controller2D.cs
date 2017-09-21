using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour 
{
	public LayerMask collisionMask;

	const float skinWidth = 0.015f;
	const float distBetweenRays = 0.25f;
	[HideInInspector]
	public int horizontalRayCount;
	public int verticalRayCount;

	float horizontalRaySpacing;
	float verticalRaySpacing;

	[HideInInspector]
	public BoxCollider2D playerCollider;

	RaycastOrigins raycastOrigins;
	public CollisionInfo collisions;



	public virtual void Awake () 
	{
		playerCollider = GetComponent<BoxCollider2D>();
	}



	public virtual void Start ()
	{
		CalculateRaySpacing();
	}



	public void Move(Vector2 moveAmount)
	{
		UpdateRaycastOrigins();
		collisions.Reset();

		if (moveAmount.x != 0)
		{ HorizontalCollisions(ref moveAmount); }
		if (moveAmount.y !=0)
		{ VerticalCollisions (ref moveAmount); }

		transform.Translate (moveAmount);
	}



	void HorizontalCollisions(ref Vector2 moveAmount)
	{
		float directionX = Mathf.Sign(moveAmount.x);
		float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

		for (int i = 0; i < horizontalRayCount; i ++)
		{
			Vector2 rayOrigin = (directionX == -1)? raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

			if (hit)
			{
				moveAmount.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance; 

				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
			}
		}
	}



	void VerticalCollisions(ref Vector2 moveAmount)
	{
		float directionY = Mathf.Sign(moveAmount.y);
		float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i ++)
		{
			Vector2 rayOrigin = (directionY == -1)? raycastOrigins.bottomLeft:raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

			if (hit)
			{
				moveAmount.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance; 

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			}
		}
	}



	void UpdateRaycastOrigins()
	{
		Bounds bounds = playerCollider.bounds;
		bounds.Expand (skinWidth * -2);

		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}



	void CalculateRaySpacing()
	{
		Bounds bounds = playerCollider.bounds;
		bounds.Expand (skinWidth * -2);

		float boundsWidth = bounds.size.x;
		float boundsHeight = bounds.size.y;

		horizontalRayCount = Mathf.RoundToInt(boundsHeight/distBetweenRays);
		verticalRayCount = Mathf.RoundToInt(boundsWidth/distBetweenRays);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}



	struct RaycastOrigins
	{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}



	public struct CollisionInfo
	{
		public bool above, below;
		public bool left, right;

		public void Reset()
			{
				above = below = false;
				left = right = false;
			}
		
	}
	
}
