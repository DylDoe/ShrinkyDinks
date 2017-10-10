using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour {

	//Music Manager selects from an array of audio clips to put into the AudioSource component

	public AudioClip[] audioClip;
	//private AudioSource currentSound;
	private new AudioSource audio;
	//public Canvas canvas;

	void Start ()
	{
		audio = GetComponent<AudioSource>();
		//canvas = GameObject.Find ("Canvas").GetComponent<Toggle> ();
		PlayMusic(0);
		//play music 0 on start
	}
	void Update ()
	{

	}


	void PlayMusic(int clip)
	{

		audio.clip = audioClip [clip];
		audio.Play ();
	}

	void  Triggered(int triggerID)
	{
		if(triggerID == 0)
		{
			//PlaySilence();
		}
		else if(triggerID == 1)
		{
			PlayMusic(0);
		}
		else if(triggerID == 2)
		{
			PlayMusic(1);
		}
		else if(triggerID == 3)
		{
			PlayMusic(2);
		}
		else if(triggerID == 4)
		{
			PlayMusic(3);
		}
	}
	//void PlaySilence()
	//{
	//	currentSound = null;
	//}



}