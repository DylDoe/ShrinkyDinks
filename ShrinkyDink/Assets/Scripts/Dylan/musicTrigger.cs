using UnityEngine;
using System.Collections;

public class musicTrigger : MonoBehaviour 
{
	//--- TRIGGER VARIABLES
	[Header("TRIGGER SETTINGS")]
	[Tooltip("Target object that is triggered (usually a manager)")]
	public GameObject targetManager; //Single target object that is triggered (usually a manager)
	[Tooltip("Is the target triggered only once of multiple times")]
	public bool  triggerOnce = true;
	public int  triggerID = 0;
	private bool  canBeTriggered = true;

	//--- USER VARIABLES
	[Header("USER SETTINGS")]
	[Tooltip("Name of the users triggering the zone")]
	public string[] userNameList;
	private bool  userIsUsingTrigger = false; //is the right user entering the trigger zone?

	//--- UTILITY VARIABLES
	private int i = 0; // for loops

	//----------------------------------------------
	//--- TRIGGER FUNCTIONS
	void  OnTriggerEnter ( Collider user)
	{
		if (canBeTriggered)
		{
			//check if the user has the right name
			i = 0; 
			while (i < userNameList.Length)
			{
				if(user.gameObject.name == userNameList[i]){userIsUsingTrigger = true;}
				i++;
			}

			//if so trigger the target manager
			if(userIsUsingTrigger)
			{
				userIsUsingTrigger = false;
				if(triggerOnce) {canBeTriggered = false;}
				targetManager.SendMessage("Triggered",triggerID);
			}
		}
	}
}
