using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {

string HeroName = "Captain Basharat Wellington";
int HeroCombatPower = 60;
int HeroHealthMax = 100;
int HeroDamageTaken;
int HeroWeaponDamage = 15;


string FoeName;
public int FoeCombatPower = 0;
int FoeHealthMax = 80;
int FoeDamageTaken = 0;
int FoeWeaponDamage = 15;

public bool HeroIsDead = false;
int FoesDefeated = 0;
int CombatRoundCount = 0;
	
	void Awake () {
		//FoeCombatPower = Random.Range(50,71);
	}

	// Use this for initialization
	void Start ()
	{
		SpawnEnemy();
		CombatAnnounce ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.A)) {
			CombatRoundCount += 1;
			print ("* * *   * * *   COMBAT ROUND " + CombatRoundCount + "   * * *   * * *\n" + HeroName + " and " + FoeName + " clash!");

			Combat (HeroName, HeroCombatPower); //the hero always attempts to strike first

			//check if the hero killed the enemy; otherwise, the enemy gets to attack
			if (FoeHealthMax - FoeDamageTaken <= 0) {
				AnnounceFoeDeath ();
			} else
				Combat (FoeName, FoeCombatPower); //if still alive, the foe always attempts to strike second
			CheckForHeroDeath ();

			if (HeroIsDead == true) {
				GameOver ();
			} else {
				PrintHeroAndFoeHealth ();
				PrintInstructions ();
				//TODO This prints the hero/foe health and print instructions twice when any enemy after the first one spawns. Refactor this.
			}

		} else if (Input.GetKeyDown (KeyCode.L)) {
			print ("You pushed the 'L' key. Our hero falls on his sword and dies in a fit of pique.");
			HeroDamageTaken += (HeroHealthMax - HeroDamageTaken);
			CheckForHeroDeath ();
			GameOver ();

		} else if (Input.GetKeyDown (KeyCode.Y)) {
			print ("You pushed the 'Y' key. The enemy abruptly explodes in a welter of gore and debug commands.");
			FoeDamageTaken += (FoeHealthMax - FoeDamageTaken);
			AnnounceFoeDeath ();
		}
	}

	//print the names and combat power of the hero and the current foe
	void CombatAnnounce () {
		print ("***** A NEW FOE APPEARS!! *****\n" + 
			"Our daring hero, " + HeroName + ", faces down a deadly foe: " + FoeName + "!");
		print ("Our hero's combat power is " + HeroCombatPower + ", and the foe's combat power is " + FoeCombatPower + ".\n" +
		"That's a difference of " + ((HeroCombatPower - FoeCombatPower).ToString ()) + ". " + CompareHeroAndFoePower());
		CompareHeroAndFoePower();
		print(FoeName + " will deal " + FoeWeaponDamage + " damage if they land a blow on " + HeroName + ".");
		PrintHeroAndFoeHealth();
		PrintInstructions ();
		}

	//compare and print the Combat Power of the hero and the current foe; print the foe's damage
	string CompareHeroAndFoePower() {
		if (HeroCombatPower == FoeCombatPower) {
			return "They're evenly matched! This fight could go either way!";
			} 
		else if (HeroCombatPower > FoeCombatPower) {
			return "Our hero is stronger than the foe!";
			} 
		else if (HeroCombatPower < FoeCombatPower) {
			return "The foe is stronger than our hero! Watch out!";
			}
		else return null;
		}

	void PrintHeroAndFoeHealth() {
		print (HeroName + "'s Current Health: " + (HeroHealthMax - HeroDamageTaken).ToString() + "/" + HeroHealthMax + "\n" + FoeName + 
		"'s Current Health: " + (FoeHealthMax - FoeDamageTaken).ToString() + "/" + FoeHealthMax);
		}

	//Combat math: roll 1d100 under combat power
	//if under, it's a hit; if over, it's a miss 
	//if under AND from 1-5, it's a critical hit (x2 base damage) 
	void Combat (string name, int power)
	{
		int CombatRoll = Random.Range (1, 101);

		//critical hit
		if (CombatRoll < power && CombatRoll < 6) {
			print (name + " rolled " + CombatRoll + " under " + power + ". " + name + " CONNECTS WITH A DEVASTATING STRIKE!");
			if (name == HeroName) {
				FoeDamageTaken += (2 * HeroWeaponDamage);
				print (FoeName + " takes " + (2 * HeroWeaponDamage) + " damage!");
			} else if (name == FoeName) {
				HeroDamageTaken += (2 * FoeWeaponDamage);
				print (HeroName + " takes " + (2 * FoeWeaponDamage) + " damage!");
			}
		}

		//normal strike
		else if (CombatRoll <= power) {
			print (name + " rolled " + CombatRoll + " under " + power + ". " + name + " lands a blow!");
			if (name == HeroName) {
				FoeDamageTaken += HeroWeaponDamage;
				print (FoeName + " takes " + HeroWeaponDamage + " damage!");
			} else if (name == FoeName) {
				HeroDamageTaken += FoeWeaponDamage;
				print (HeroName + " takes " + FoeWeaponDamage + " damage!");
			}

		//fail to hit
		} else if (CombatRoll > power) {
			print (name + " rolled " + CombatRoll + " over " + power + ". " + name + " misses!");
		}
	}

	//If damage is greater than or equal to the hero's maximum health, print relevant text, number of foes defeated, and return true.
	void CheckForHeroDeath () {
		if (HeroHealthMax - HeroDamageTaken <= 0) {
			print(HeroName + " dies! Alas! Your deeds will be remembered.");
			print("You slew " + FoesDefeated + " foes before your untimely demise.");
			HeroIsDead = true;
		}
	}

	//Declare the enemy dead, give the player a current and max health boost, increment the number of foes defeated, and spawn/announce the next one.
	void AnnounceFoeDeath () {
		int HealthVictoryBonus = 10;
		HeroHealthMax += HealthVictoryBonus;
		HeroDamageTaken -= HealthVictoryBonus;
		int CombatPowerVictoryBonus = 2;
		HeroCombatPower += CombatPowerVictoryBonus;
		FoesDefeated = +1;
		print (FoeName + " is slain! You have defeated " + FoesDefeated + " enemies!\n" + 
				HeroName + " binds their wounds, gathers greater strength and prepares for the next challenge.");
		print(HeroName + "'s maximum and current health improved by " + HealthVictoryBonus + "!\n" + 
				HeroName + " has learned a few new tricks; their Combat Power increases from " + (HeroCombatPower - CombatPowerVictoryBonus) + " to " + 
				HeroCombatPower + ".");
		SpawnEnemy();
		CombatAnnounce ();
	}

	void PrintInstructions () {
		print("Press (A) to initiate a round of combat; press (L) to have our hero fall on his sword. Press (Y) to cheat and instantly slay the foe.");
	}

	void GameOver ()
	{
		print ("Game Over! If you'd like to play again, press (Spacebar).");
		//TODO Wrap the entire core game loop in a thing that lets me restart it after 1) the player is definitely dead and 2) the user hits restart.
		}

	//TODO Balance the difficulty of these enemy stats, or at least make lower-danger/damage tier/defense enemies spawn before higher ones.
	void SetEnemyStatsByTier (int danger, int damagetier, int defense)
	{
		if 		(danger == 1) {FoeCombatPower = Random.Range(15,26);} 
		else if (danger == 2) {FoeCombatPower = Random.Range(35,46);} 
		else if (danger == 3) {FoeCombatPower = Random.Range(55,65);}

		if 		(damagetier == 1) {FoeWeaponDamage = 2;}
		else if (damagetier == 2) {FoeWeaponDamage = 5;}
		else if (damagetier == 3) {FoeWeaponDamage = 10;}
		else if (damagetier == 4) {FoeWeaponDamage = 25;}
		else if (damagetier == 5) {FoeWeaponDamage = 45;}

		if 		(defense == 1) {FoeHealthMax = 30;}
		else if (defense == 2) {FoeHealthMax = 50;}
		else if (defense == 3) {FoeHealthMax = 70;}

	}

	void SpawnEnemy () {
	FoeDamageTaken = 0;
	int EnemyPicker = Random.Range(1,10);
		if (EnemyPicker == 1) {
			FoeName = "Animus Mold";
			SetEnemyStatsByTier(1,1,1);
		}
		if (EnemyPicker == 2) {
			FoeName = "Leering Svartælf";
			SetEnemyStatsByTier(1,1,1);
		}
		if (EnemyPicker == 3) {
			FoeName = "Feral Sporehound";
			SetEnemyStatsByTier(1,2,1);
		}
		if (EnemyPicker == 4) {
			FoeName = "Knocker Clan Sapper";
			SetEnemyStatsByTier(2,2,2);
		}
		if (EnemyPicker == 5) {
			FoeName = "Conquerer Legion Chainmaster";
			SetEnemyStatsByTier(2,3,2);
		}
		if (EnemyPicker == 6) {
			FoeName = "Bog Priest";
			SetEnemyStatsByTier(2,3,2);
		}
		if (EnemyPicker == 7) {
			FoeName = "Frothing Jotunkin";
			SetEnemyStatsByTier(3,4,3);
		}
		if (EnemyPicker == 8) {
			FoeName = "Seething Rune-Slave";
			SetEnemyStatsByTier(3,4,3);
		}
		if (EnemyPicker == 9) {
			FoeName = "Serpent-Priest Yddremel The Devoted";
			SetEnemyStatsByTier(3,5,3);
		}
	}

	}

