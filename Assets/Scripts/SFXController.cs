using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXController : MonoBehaviour {

public AudioClip foeDeath;
AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	void Update () {
		// Check every frame is foe.isDead() returns true. If it does, print to console "FOE IS DEAD, PLAY A SOUND EFFECT HERE."

		if (Main.foe.isDead()){
			foeDeath = 
			Debug.Log("FOE IS DEAD, PLAY A SOUND EFFECT HERE IN SFXCONTROLLER.");
			audioSource.PlayOneShot(foeDeath);

		}
		
	}
}
