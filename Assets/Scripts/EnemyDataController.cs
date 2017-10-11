using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDataController : MonoBehaviour {

public static string enemyStats;
public Text enemyStatsDisplayer; //only use this to display the player's stats on the screen.

public string enemyName = "Miserable Goblin";
public int enemyCurrentHealth;
public int enemyMaximumHealth;
public int defenseReduction;
public int counterattackSkill;
// end of values to mess with (and visual separator)

public string enemyWeaponName;
public int enemyWeaponMinDamage;
public int enemyWeaponMaxDamage;

	// Use this for initialization
	void Start () {
		StartGame();
	}

	void StartGame () {
		PrintStats();

		int testroll = GameActionDisplayController.diceroll(3,6);
		print("The " + enemyName + " strikes you for " + testroll + " damage!");

	}

	void PrintStats() {
		enemyStats = 
			"You face a terrible foe: " + enemyName + "\n" +
			"Current health: " + enemyCurrentHealth + "/" + enemyMaximumHealth + ".\n" +
			"It is armed with a " + enemyWeaponName + 
				" (" + enemyWeaponMinDamage + " to " + enemyWeaponMaxDamage + " damage).\n" +
			"\tDefense: " + defenseReduction + "\n" + 
			"\tCounterattack: " + counterattackSkill + "\n";
		enemyStatsDisplayer.text = enemyStats;
	}

	// TODO: Remove all these magic numbers and figure out how to randomize enemy equipment by tier.
	//private enum BasicWeapons {"club", "dirk", "hatchet", "mallet", "mace", "sickle", "shortspear", "rusty gauntlet"};
	//private BasicWeapons selectedBasicWeapon;
}
