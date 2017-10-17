using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay2 : MonoBehaviour {

	public Text levelText;
	private float startLevel;
	bool higherLevel = false;

	// Use this for initialization
	void Start () 
	{
		startLevel = 0f;
	}

	// Update is called once per frame
	void Update () 
	{
		GameObject player = GameObject.Find("Player2");
		PlayerLevel level = player.GetComponent<PlayerLevel>();
		float levelF = level.PLevel; 
		float LevelC = startLevel =+ levelF;

		if (LevelC > levelF) {
			higherLevel = true;		
		} else {
			higherLevel = false;
		}

		if (higherLevel = true){
			string levelD = ((startLevel += levelF)*0.5f).ToString();
			levelText.text = "Lv." + levelD;}
			//minutes + ":" + seconds;
	}
		
}
