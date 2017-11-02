using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	public Text displayText = null;

	Hero hero = new Hero("Captain Basharat Wellington", 70, 40, 55, "silver-chased tulwar", 15, 100, 0);
	Creature foe = new Creature("Test Goblin", 30, "poo-covered test stick", 5, 35);
	Trap trap = new Trap("Test Trap Description", 1, "test trap damage name", 1, 20);

	private int combatRound = 0;
	public static int killcount;
	public static string playerKilledBy = "DEBUG_VALUE";
	public static string playerName = "DEBUG_VALUE"; 
	public string encounterType;
	public bool trapTriggered;

	void Awake () {
		displayText.text = null;
		playerName = hero.getName();
		}

	// Use this for initialization
	void Start () {

		EncounterPicker();



		}
	
	// Update is called once per frame
	void Update ()
	{

		//TODO: There is probably a more elegant way to pick between encounter types. 
		if (encounterType == "enemy") {
			if (Input.GetKeyDown (KeyCode.A)) {CombatSequence ();}

		} else if (encounterType == "trap" && !trapTriggered) {
			if (Input.GetKeyDown (KeyCode.A)) {DisarmTrapSequence ();}
			if (Input.GetKeyDown (KeyCode.L)) {SmashTrapSequence ();}

		} else if (encounterType == "trap" && trapTriggered) {
			//if you found a trap, tried to disarm it, and failed, you must finish smashing it to procede.
			if (Input.GetKeyDown (KeyCode.L)) {SmashTrapSequence ();}
		}
	}

	public string EncounterPicker ()
	{
		int picker = Random.Range (1, 3);
		Debug.Log("1 is a monster, 2 is a trap. EncounterPicker picked " + picker);
		if (picker == 1) 
			{
				encounterType = "enemy";
				foe = SpawnEnemy();
				displayText.text += foe.getName() + " suddenly approaches, ready to do battle.\n\n";
				Debug.Log("SpawnEnemy has selected " + foe.getName());
				DisplayStats();
				return encounterType;
			}
		else if (picker == 2) 
			{
				encounterType = "trap";
				trap = SpawnTrap ();
				Debug.Log("SpawnTrap has selected '" + trap.getTrapName() + "'");
				AnnounceHeroStats ();
				displayText.text += hero.getName() + " notices " + trap.getTrapName() + ". This is clearly a trap! The mechanism " + trap.getDisarmDifficultyDescription() + ".\n\n" + 

				"Press (A) to try to disarm it.\n" + 
				"Beware! Success guarantees safety; failure may result in GREIVOUS harm.\n\n" + 

				"Press (L) to draw your weapon and smash the mechanism instead.\n" + 
				"This will certainly activate the trap, but you'll be ready for it and <i>might</i> suffer lesser harm in the process.\n\n";
				return encounterType;
			}
		else throw new System.InvalidOperationException("EncounterPicker failed to pick either a Trap or an Enemy. How?");
	}

	public void CombatSequence () {
		//clear the UI 
		displayText.text = null;
		combatRound++;
		displayText.text += "* * * * ROUND " + combatRound + " * * * *\n\n";
		//hero attacks foe
		Fight (hero, foe);
		if (foe.isDead ()) {
			//if foe is slain, clear the UI, announce foe slain, level up the hero, and display the new hero stats
			HeroVictory ();
			//update and display stats, then wait for a button press to start Combat Sequence over.
			//DisplayStats ();
		} else {
			Fight (foe, hero); //if foe is NOT dead, foe attacks hero;
			if (hero.isDead ()) {
				playerKilledBy = foe.getName();
				GameOver();
			} else DisplayStats ();
		}
	}

	public void DisarmTrapSequence ()
	{
		displayText.text = null;
		combatRound++;
		displayText.text += "* * * * ROUND " + combatRound + " * * * *\n\n";
		TrapDisarm (trap);
		if (trap.isDead ()) {
			trapTriggered = false;
			HeroVictory ();
		} else {
			// Hero tried to disarm the trap and failed to do so. Check if the trap damage killed the hero and go to Game Over scene if it did. 
			// Otherwise, go up one level out of DisarmTrapSequence to Update, where you'll now only have the option to Smash the trap.
			// Things that need to show up specifically when the hero is trying to smash the trap should go into SmashTrapSequence().
			if (hero.isDead ()) 
				{
					playerKilledBy = trap.getDamageDescription();
					GameOver ();
				}  //else DisplayStats (); // I've commented this DisplayStats() out because it was showing a TEST GOBLIN result if you tried to disarm a trap and failed. I'm leaving it here as reference and example to myself.
		}
	}

	public void TrapDisarm (Trap trap) {
		// Note: The way that difficult traps make the disarm check harder is by subtracting a negative number (their difficulty value) from the d100 roll, on which it is better to roll a low number.
		// This is needlessly complicated, but the code works, and so it remains as such for now. If you wanted to clear this up, add trap.getDisarmDifficulty to disarmAttempt rather than subtracting it AND
		// change trapDisarmDifficultyValue to a positive number; this would make it so that you're trying to roll a low number on the MechanicalSkill check but the trap is *adding* to your final result.
		// Six of one, half a dozen of another.

		int disarmAttempt = (Random.Range (1, 101) - trap.getDisarmDifficulty());
		Debug.Log("Hero's disarm attempt base roll was " + (disarmAttempt + trap.getDisarmDifficulty()) + " with an additional penalty of " + trap.getDisarmDifficulty() + " for a result of " + disarmAttempt);
		if (disarmAttempt <= hero.getMechanicalSkill ()) {
			displayText.text += hero.getName () + " rolled " + disarmAttempt + " under " + hero.getCombatSkill () + ". The trap is disarmed!\n";
			trap.applyDamage(trap.getMaxHealth()); //kill the trap on a successful disarm attempt

		} else if (disarmAttempt > hero.getMechanicalSkill  ()) {
			displayText.text += hero.getName () + " rolled " + disarmAttempt + " over " + hero.getMechanicalSkill () + "--the trap springs to life mere inches away!\n\n";
			trapTriggered = true;
			int trapDamageMultiplier = (Random.Range(1,4));
			int trapDamage = trapDamageMultiplier * trap.getTrapDamage();
			Debug.Log("Trap base damage of " + trap.getTrapDamage() + " is multiplied by " + trapDamageMultiplier + " for a total of " + trapDamageMultiplier * trap.getTrapDamage());
			hero.applyDamage(trapDamage);
			displayText.text += "A " + trap.getDamageDescription() + " inflicts " + trapDamage + " damage to " + hero.getName() + ".\n\n" + 

			"The trap is sprung and " + hero.getName() + " must smash it before they can scramble free!\n\n" + 

			"Press (L) to draw your weapon and smash the mechanism!\n\n";
		}
	}

	public void SmashTrapSequence ()
	{
		//clear the UI 
		displayText.text = null;
		combatRound++;
		displayText.text += "* * * * ROUND " + combatRound + " * * * *\n\n";
		trapTriggered = true;

		// The hero attempts to damage the trap.
		Smash (trap, hero);

		// The trap doesn't roll to attack; it automatically deals its damage every round!
		int trapDamage = trap.getTrapDamage ();
		hero.applyDamage (trapDamage);
		displayText.text += "A " + trap.getDamageDescription () + " inflicts " + trapDamage + " damage to " + hero.getName () + ".\n\n";

		// End this violence if either the hero or the trap are slain.
		if (trap.isDead ()) {
			trapTriggered = false;
			HeroVictory ();
		}  else {
			if (hero.isDead ()) 
				{
					playerKilledBy = trap.getDamageDescription();
					GameOver ();
				}  else DisplayStats ();
		}
	}

	public Creature SpawnEnemy ()
	{
		int EnemyPicker = Random.Range ((hero.getFoesDefeated()/2), hero.getFoesDefeated());
		if (EnemyPicker < 1) {EnemyPicker = 1;}
		if (EnemyPicker > 9) {EnemyPicker = 9;}
		Debug.Log("EnemyPicker rolled a " + EnemyPicker);
			if (EnemyPicker == 1) {return new Creature("Animus Mold", Random.Range(15,26), "moldy bodyslam", 2, 30);}
			if (EnemyPicker == 2) {return new Creature("Leering Svartælf", Random.Range(15,26), "jagged glassine spear", 2, 30);}
			if (EnemyPicker == 3) {return new Creature("Escaped Sporehound", Random.Range(15,26), "saw-toothed gills", 5, 45);}
			if (EnemyPicker == 4) {return new Creature("Knocker Clan Sapper", Random.Range(35,46), "rocksalt blunderbuss", 5, 45);}
			if (EnemyPicker == 5) {return new Creature("Conquerer Legion Chainmaster", Random.Range(35,46), "barbed bronze scourge", 10, 60);}
			if (EnemyPicker == 6) {return new Creature("Bog Priest", Random.Range(35,46), "charmed and blackened claws", 10, 60);}
			if (EnemyPicker == 7) {return new Creature("Frothing Jotunkin", Random.Range(55,65), "etched iron mallet", 25, 75);}
			if (EnemyPicker == 8) {return new Creature("Seething Rune-Slave", Random.Range(55,65), "crushing grasp", 25, 75);}
			if (EnemyPicker == 9) {return new Creature("Serpent-Priest Yddremel", Random.Range(55,65), "crashing ophidian spellcraft", 45, 90);}
				else throw new System.InvalidOperationException("EnemyPicker in SpawnEnemy() is returning something out of bounds.");
		}

	public Trap SpawnTrap ()
	{
		int TrapPicker = Random.Range (1, 4);
			if (TrapPicker == 1) {return new Trap ("a flagstone hallway marred by deep gouges in the stone", 0, "razored bronze crescent", Random.Range (3, 19), 20);}
			if (TrapPicker == 2) {return new Trap ("a panel on the wall covered in suspicious holes", 1, "toxin-coated dart", Random.Range (3, 19), 20);}
			if (TrapPicker == 3) {return new Trap ("cracks on the floor, above which is a bit of ceiling constructed from a single, large block", 2, "plummeting stone block", Random.Range (3, 19), 20);}
			// This list should have a total of nine traps
				else throw new System.InvalidOperationException("TrapPicker in SpawnTrap() is returning something out of bounds.");
	}

	public void AnnounceHeroStats ()
	{
		displayText.text += hero.getName () + "\nCombat Skill " + hero.getCombatSkill () + "% | Mechanics Skill " + hero.getMechanicalSkill() + "% | Diplomacy Skill " + hero.getDiplomacySkill() + "% | " + 
			"<color=#00ff00ff>" + hero.getCurrentHealth () + "/" + hero.getMaxHealth () + "</color>" + " health\n";
		displayText.text += "Their " + hero.getWeaponName () + " will strike for " + "<b><color=red>" + hero.getWeaponDamage () + "</color></b> damage " +
			"(or <b><color=red>" + 2 * hero.getWeaponDamage () + "</color></b> on a critical hit.)\n\n";
	}

	public void AnnounceStats (Creature creature)
	{
		displayText.text += creature.getName () + "\nCombat Skill " + creature.getCombatSkill () + "% | " +
			"<color=#00ff00ff>" + creature.getCurrentHealth () + "/" + creature.getMaxHealth () + "</color>" + " health\n";
		displayText.text += creature.getName() + "'s " + creature.getWeaponName () + " will strike for " + "<b><color=red>" + creature.getWeaponDamage () + "</color></b> damage " +
			"(or <b><color=red>" + 2 * creature.getWeaponDamage () + "</color></b> on a critical hit.)\n\n";
	}

	public void AnnounceTrapStats (Trap trap)
	{
		string announceTrapDamage = trap.getTrapDamage().ToString();
		displayText.text += "The deadly trap: <color=#00ff00ff>" + trap.getCurrentHealth() + "/" + trap.getMaxHealth() + "</color>" + " health\n";
		displayText.text += hero.getName() + " will be struck by " + trap.getDamageDescription() + " every round for <b><color=red>" + announceTrapDamage + "</color></b> damage.\n\n";
	}

	public void DisplayStats() {
		displayText.text += "* * * * * * * * * *\n";
		AnnounceHeroStats();

		if (encounterType == "enemy") 
			{
				AnnounceStats (foe);
				displayText.text += "Press (A) to attack " + foe.getName() + "!\n\n";
			} 
		else if (encounterType == "trap") 
			{
				AnnounceTrapStats (trap);
				displayText.text += "Press (L) to smash the mechanism!\n\n";
			}

		
		displayText.text += "* * * * * * * * * *\n";
	}

	public void Fight (Creature attacker, Creature defender) {
		int roll = Random.Range (1, 101);
		if (roll <= 5 && roll <= attacker.getCombatSkill ()) {
			displayText.text += attacker.getName () + " rolled " + roll + " under " + attacker.getCombatSkill () + "--<b><color=orange>A DEVASTATING STRIKE!</color></b>\n";
			int damage = attacker.getWeaponDamage();
			defender.applyDamage(2 * damage);
			displayText.text += defender.getName () + " takes " + 2 * damage + " damage.\n\n";
		
		} else if (roll <= attacker.getCombatSkill ()) {
			displayText.text += attacker.getName () + " rolled " + roll + " under " + attacker.getCombatSkill () + "--a hit!\n";
			int damage = attacker.getWeaponDamage();
			defender.applyDamage(damage);
			displayText.text += defender.getName () + " takes " + damage + " damage.\n\n";
		
		} else if (roll > attacker.getCombatSkill ()) {
			displayText.text += attacker.getName () + " rolled " + roll + " over " + attacker.getCombatSkill () + "--a miss!\n\n";
		}
	}

	public void Smash (Trap trap, Creature hero) {
		int roll = Random.Range (1, 101);
		if (roll <= 5 && roll <= hero.getCombatSkill ()) {
			displayText.text += hero.getName () + " rolled " + roll + " under " + hero.getCombatSkill () + "--<b><color=orange>A DEVASTATING STRIKE!</color></b>\n";
			int damage = hero.getWeaponDamage();
			trap.applyDamage(2 * damage);
			displayText.text += "The deadly mechanism takes " + 2 * damage + " damage.\n\n";
		
		} else if (roll <= hero.getCombatSkill ()) {
			displayText.text += hero.getName () + " rolled " + roll + " under " + hero.getCombatSkill () + "--a hit!\n";
			int damage = hero.getWeaponDamage();
			trap.applyDamage(damage);
			displayText.text += "The deadly mechanism takes " + damage + " damage.\n\n";
		
		} else if (roll > hero.getCombatSkill ()) {
			displayText.text += hero.getName () + " rolled " + roll + " over " + hero.getCombatSkill () + "--a miss!\n\n";
		}
	}



	public void HeroVictory ()
	{
		displayText.text = null;
		if (encounterType == "enemy") {
			displayText.text = foe.getName () + " is slain!\n";
		} else if (encounterType == "trap") {
			displayText.text = "The trap is defeated!\n";
		}

		hero.addDefeatedFoe ();
		if (hero.getFoesDefeated () == 1) {
			displayText.text += hero.getName () + " has triumphed over a single challenge.\n\n";
		} else displayText.text += hero.getName () + " has triumphed over " + hero.getFoesDefeated () + " foes.\n\n";
		killcount= hero.getFoesDefeated();

		int levelUpHealthBonus = 10;
		int levelUpCombatSkillBonus = 2;
		int levelUpMechanicalSkillBonus = 2;
		int levelUpDiplomacySkillBonus = 2;
		hero.levelUp (levelUpHealthBonus,levelUpCombatSkillBonus,levelUpMechanicalSkillBonus,levelUpDiplomacySkillBonus);
		displayText.text += "Victory in Acheron earns power--wounds knit and skills are honed.\n\n";
		displayText.text += hero.getName () + "'s maximum and current health improve by " + "<color=#00ff00ff>" + levelUpHealthBonus + "</color>.\n" + 
		"Combat Skill +" + levelUpCombatSkillBonus + "%! Mechanical Skill +" + levelUpMechanicalSkillBonus + "%! Diplomacy Skill +" + levelUpDiplomacySkillBonus + "%!\n\n";

		EncounterPicker();

		//DisplayStats ();
		}

	public void GameOver ()	{
		SceneManager.LoadScene("GameOver");
	}


	}