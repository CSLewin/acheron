using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataController : MonoBehaviour {


// Some values to mess with
string PlayerName = "Nameless Vagabond";
int PlayerCurrentHealth = 15;
int PlayerMaximumHealth = 15;
int CombatSkill = 40;
int DiplomacySkill = 50;
int MechanicsSkill = 60;
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
			"Your current health is " + PlayerCurrentHealth + "/" + PlayerMaximumHealth + ".\n" +
			"Your skills are: \n" + 
			"\tCombat (" + CombatSkill + "%)\n" + 
			"\tDiplomacy (" + DiplomacySkill + "%)\n" + 
			"\tMechanics (" + MechanicsSkill + "%).\n";
		PlayerStatsDisplayer.text = PlayerStats;
	}

}
