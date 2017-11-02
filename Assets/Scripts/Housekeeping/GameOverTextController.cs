using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTextController : MonoBehaviour {

	public Text gameOverText = null;

	// Use this for initialization
	void Start ()
	{
		gameOverText.text = Main.playerName + " has died!\n\n";

		gameOverText.text += "You defeated <color=white>" + Main.killcount + "</color> foes and traps before your death...\n" + 
		"...and in turn were slain by <color=white>" + Main.playerKilledBy +"</color>.\n\n";

		gameOverText.text += "Your bones are scattered by sightless vermin and your belongings are stolen by the desperate things that wander Acheron.\n\n";
	}

		
	}