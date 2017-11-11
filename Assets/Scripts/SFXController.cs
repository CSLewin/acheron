using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour {

private bool canRunFoeDeathAudio = true;

private AudioSource audioFoeDeath;

public AudioClip clipGenericFoeDeath;

	void Awake () {
		audioFoeDeath = GetComponent<AudioSource>();
	}

	void Update () {
		// Check every frame is foe.isDead() returns true. If it does, print to console "FOE IS DEAD, PLAY A SOUND EFFECT HERE."

		if (Main.foe.isDead() && canRunFoeDeathAudio) {
			canRunFoeDeathAudio = false;
			audioFoeDeath.PlayOneShot(clipGenericFoeDeath, 1.0f);
		}

		// Here in update, but outside the foedeath loop, have Main.cs tell SFXController.cs to set canRunFoeDeathAudio to true.
		// This design pattern is going to get ugly later.

	}
}
