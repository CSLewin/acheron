using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {

// TODO
// Get FoesDefeated counter working (mess with scope of counter?)
// When you kill an enemy, create a new one
// When the player dies, ask if they want to restart the game

string HeroName = "Captain Basharat Wellington";
int HeroCombatPower;
int HeroHealthMax = 100;
int HeroDamageTaken;
int HeroWeaponDamage = 15;


string FoeName = "Serpent-Priest Yddremel";
public int FoeCombatPower = 0;
int FoeHealthMax = 80;
int FoeDamageTaken = 0;
int FoeWeaponDamage = 15;

int FoesDefeated = 0;
int CombatRoundCount = 0;

	// Use this for initialization
	void Awake ()
	{
		HeroCombatPower = Random.Range(50,71);
		FoeCombatPower = Random.Range(50,71);
	}

void CompareHeroAndFoePower() {
	if (HeroCombatPower == FoeCombatPower) {
		print ("They're evenly matched! This fight could go either way!");
		} else if (HeroCombatPower > FoeCombatPower) {
			print ("Our hero is stronger than the foe!");
		} else if (HeroCombatPower < FoeCombatPower) {
			print ("The foe is stronger than our hero! Watch out!");
		}
	}


	void Start ()
	{
		CombatAnnounce ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.A)) {
			CombatRoundCount += 1;
			print ("* * *   * * *   COMBAT ROUND " + CombatRoundCount + "   * * *   * * *\n" + 
				HeroName + " and " + FoeName + " clash!");

			Combat (HeroName, HeroCombatPower);
			Combat (FoeName, FoeCombatPower);

			PrintHeroAndFoeHealth ();
			BringOutYourDead ();
			print("Press (A) to initiate a round of combat; press (L) to have our hero fall on his sword. Press (Y) to cheat and instantly slay the foe.");

		} else if (Input.GetKeyDown (KeyCode.L)) {
			print ("You pushed the 'L' key. Our hero falls on his sword and dies in a fit of pique.");
			HeroDamageTaken += (HeroHealthMax - HeroDamageTaken);
			PrintHeroAndFoeHealth ();
			BringOutYourDead ();

		} else if (Input.GetKeyDown (KeyCode.Y)) {
			print ("You pushed the 'Y' key. The enemy abruptly explodes in a welter of gore and debug commands.");
			FoeDamageTaken += (FoeHealthMax - FoeDamageTaken);
			PrintHeroAndFoeHealth ();
			BringOutYourDead ();
			print("Press (A) to initiate a round of combat; press (L) to have our hero fall on his sword. Press (Y) to cheat and instantly slay the foe.");
		}

	}

	void CombatAnnounce () {
		print ("Our daring hero, " + HeroName + ", faces down a deadly foe: " + FoeName + "!");
		print ("Our hero's combat power is " + HeroCombatPower + ", and the foe's combat power is " + FoeCombatPower + ".\n" +
		"That's a difference of " + ((HeroCombatPower - FoeCombatPower).ToString ()) + ".");
		CompareHeroAndFoePower();
		print("Press (A) to initiate a round of combat; press (L) to have our hero fall on his sword. Press (Y) to cheat and instantly slay the foe.");
	}


	void PrintHeroAndFoeHealth() {
		print (HeroName + "'s Current Health: " + (HeroHealthMax - HeroDamageTaken).ToString() + "/" + HeroHealthMax);
		print (FoeName + "'s Current Health: " + (FoeHealthMax - FoeDamageTaken).ToString() + "/" + FoeHealthMax);
		}

	void Combat (string name, int power)
	{
		int CombatRoll = Random.Range (1, 101);

		//normal strike
		if (CombatRoll < power) {
			print (name + " rolled " + CombatRoll + " under " + power + ". " + name + " lands a blow!");
			if (name == HeroName) {
				FoeDamageTaken += HeroWeaponDamage;
				print (FoeName + " takes " + HeroWeaponDamage + " damage!");
			} else if (name == FoeName) {
				HeroDamageTaken += FoeWeaponDamage;
				print (HeroName + " takes " + FoeWeaponDamage + " damage!");}

		//fail to hit
		} else if (CombatRoll > power) {
			print (name + " rolled " + CombatRoll + " over " + power + ". " + name + " misses!");


		//critical hit
		} else if (CombatRoll == power) {
			print (name + " rolled " + CombatRoll + " exactly! " + name + " CONNECTS WITH A DEVASTATING STRIKE!");
			if (name == HeroName) {
				FoeDamageTaken += (2 * HeroWeaponDamage);
				print (FoeName + " takes " + (2 * HeroWeaponDamage) + " damage!");}
			else if (name == FoeName) {
				HeroDamageTaken += (2 * FoeWeaponDamage);
				print (HeroName + " takes " + (2 * FoeWeaponDamage) + " damage!");}
		}
	}

	void BringOutYourDead ()
	{
		CheckForFoeDeath();
		CheckForHeroDeath();
	}

	void CheckForFoeDeath ()
	{
		if (FoeHealthMax - FoeDamageTaken <= 0) {
			print (FoeName + " is slain!\n" + HeroName + " gathers strength and prepares for the next challenge.");
			int FoesDefeated = +1;
		}
	}

	void CheckForHeroDeath () {
		if (HeroHealthMax - HeroDamageTaken <= 0) {
		print(HeroName + " dies! Alas! Your deeds will be remembered.");
		print("You slew " + FoesDefeated + " foes before your untimely demise.");
		}
	}



	}
