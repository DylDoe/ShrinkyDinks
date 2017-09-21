using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
 

	public void StartButton(string newGameLevel)
	{
		SceneManager.LoadScene (newGameLevel);
	}

	public void QuitButton ()
	{
		Application.Quit ();
	}
		
}
