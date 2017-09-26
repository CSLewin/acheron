using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataController : MonoBehaviour {

string PlayerName = "Nameless Vagabond";
int PlayerCurrentHealth = 15;
int PlayerMaximumHealth = 15;
int CombatSkill = 40;
int DiplomacySkill = 50;
int MechanicsSkill = 60;

int skill;
int dicecheck;

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

		//statblock
		print ("You are known as '" + PlayerName + "'. Your current health is " + PlayerCurrentHealth + "/" + PlayerMaximumHealth + ".\n" +
			"Your skills are: Combat (" + CombatSkill + "/100), Diplomacy (" + DiplomacySkill + "/100), and Mechanics (" + MechanicsSkill + "/100).");

		print ("You test your Combat Skill.");
		Roll(CombatSkill);

		print ("You test your Diplomacy Skill.");
		Roll(DiplomacySkill);

		print ("You test your Mechanics Skill.");
		Roll(MechanicsSkill);

	}

}
