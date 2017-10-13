using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataController : MonoBehaviour {


// Some values to mess with
string PlayerName = "Nameless Vagabond";
int playerCurrentHealth = 15;
int playerMaximumHealth = 15;
int combatSkill = 40;
int diplomacySkill = 50;
int mechanicsSkill = 60;
// end of values to mess with (and visual separator)

string PlayerStats;

public Text PlayerStatsDisplayer; //only use this to display the player's stats on the screen.

//A simple percentile dicerolling function that retuns "pass" or "fail" and prints some appropriate text to the console.
public string Roll (int skill) {
		int dicecheck = Random.Range (1, 101);
		if (dicecheck <= skill) {
			print ("You rolled " + dicecheck + " under " + skill + ". Success!");
			return "pass";
		} else {
			print ("You rolled " + dicecheck + " over " + skill + "). Failure!");
			return "fail";
		}

	}

	// Use this for initialization
	void Start () {
		StartGame();
	}

	void StartGame () {
		PrintStats();
	}

	void PrintStats() {
		PlayerStats = 
			"You are known as '" + PlayerName + "'.\n" +
			"Current health: " + playerCurrentHealth + "/" + playerMaximumHealth + ".\n" +
			"Your skills are: \n" + 
			"\tCombat (" + combatSkill + "%)\n" + 
			"\tDiplomacy (" + diplomacySkill + "%)\n" + 
			"\tMechanics (" + mechanicsSkill + "%).\n";
		PlayerStatsDisplayer.text = PlayerStats;
	}

}
