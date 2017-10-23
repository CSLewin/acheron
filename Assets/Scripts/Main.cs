using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	public Text displayText = null;

	Hero hero = new Hero("Captain Basharat Wellington", 60, "silver-chased tulwar", 15, 100, 0);
	Creature foe = new Creature("Test Goblin", 30, "poo-covered stick", 5, 35);

	private int combatRound = 0;
	public static int killcount;
	public static string playerKilledBy = "**DEBUGVALUE**";
	public static string playerName = "**DEBUGVALUE**"; 

	void Awake () {
		displayText.text = null;
		playerName = hero.getName();
		}

	// Use this for initialization
	void Start () {

		foe = SpawnEnemy();
		Debug.Log("SpawnEnemy has selected " + foe.getName());

		DisplayStats();

		}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.A)) {
			CombatSequence ();
			}

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


	public Creature SpawnEnemy () {
		int EnemyPicker = Random.Range(1,10);
			if (EnemyPicker == 1) {return new Creature("Animus Mold", Random.Range(15,26), "moldy bodyslam", 2, 30);}
			if (EnemyPicker == 2) {return new Creature("Leering Svartælf", Random.Range(15,26), "jagged glassine spear", 2, 30);}
			if (EnemyPicker == 3) {return new Creature("Escaped Sporehound", Random.Range(15,26), "saw-toothed gills", 5, 35);}
			if (EnemyPicker == 4) {return new Creature("Knocker Clan Sapper", Random.Range(35,46), "rocksalt blunderbuss", 5, 50);}
			if (EnemyPicker == 5) {return new Creature("Conquerer Legion Chainmaster", Random.Range(35,46), "barbed bronze scourge", 10, 50);}
			if (EnemyPicker == 6) {return new Creature("Bog Priest", Random.Range(35,46), "charmed and blackened claws", 10, 50);}
			if (EnemyPicker == 7) {return new Creature("Frothing Jotunkin", Random.Range(55,65), "etched iron mallet", 25, 70);}
			if (EnemyPicker == 8) {return new Creature("Seething Rune-Slave", Random.Range(55,65), "crushing grasp", 25, 70);}
			if (EnemyPicker == 9) {return new Creature("Serpent-Priest Yddremel The Devoted", Random.Range(55,65), "crashing ophidian spellcraft", 45, 70);}
				else throw new System.InvalidOperationException("You've somehow rolled an 11 on a d10. This is a problem. Go look at SpawnEnemy().");
		}

	public void AnnounceStats(Creature creature) {
		displayText.text += creature.getName () + " has a Combat Power of " + creature.getCombatPower () + " and " +
			"<color=#00ff00ff>" + creature.getCurrentHealth () + "/" + creature.getMaxHealth () + "</color>" + " health.\n";
		displayText.text += "Their " + creature.getWeaponName () + " will strike for " + "<b><color=red>" + creature.getWeaponDamage () + "</color></b>" + 
			" (or <b><color=red>" + 2 * creature.getWeaponDamage () + "</color></b> on a critical hit.)\n\n";
	}

	public void DisplayStats() {
		displayText.text += "* * * * * * * * * *\n";
		AnnounceStats (hero);

		AnnounceStats (foe);

		displayText.text += "* * * * * * * * * *\n";

		displayText.text += "Press (A) to attack " + foe.getName() + "!\n\n";
	}

	public void Fight (Creature attacker, Creature defender) {
		int roll = Random.Range (1, 101);
		if (roll <= 5 && roll <= attacker.getCombatPower ()) {
			displayText.text += attacker.getName () + " rolled " + roll + " under " + attacker.getCombatPower () + "--<b><color=orange>A DEVASTATING STRIKE!</color></b>\n";
			int damage = attacker.getWeaponDamage();
			defender.applyDamage(2 * damage);
			displayText.text += defender.getName () + " takes " + 2 * damage + " damage.\n\n";
			AudioSource.PlayClipAtPoint
		
		} else if (roll <= attacker.getCombatPower ()) {
			displayText.text += attacker.getName () + " rolled " + roll + " under " + attacker.getCombatPower () + "--a hit!\n";
			int damage = attacker.getWeaponDamage();
			defender.applyDamage(damage);
			displayText.text += defender.getName () + " takes " + damage + " damage.\n\n";
		
		} else if (roll > attacker.getCombatPower ()) {
			displayText.text += attacker.getName () + " rolled " + roll + " over " + attacker.getCombatPower () + "--a miss!\n\n";
		}
	}

	public void HeroVictory ()
	{
		displayText.text = null;
		displayText.text = foe.getName () + " is slain!\n";
		hero.addDefeatedFoe ();
		if (hero.getFoesDefeated () == 1) {
			displayText.text += hero.getName () + " has triumphed over a single foe.\n\n";
		} else displayText.text += hero.getName () + " has triumphed over " + hero.getFoesDefeated () + " foes.\n\n";
		killcount= hero.getFoesDefeated();
		int levelUpHealthBonus = 10;
		int levelUpCombatPowerBonus = 2;
		hero.levelUp (levelUpHealthBonus,levelUpCombatPowerBonus);
		displayText.text += "Victory in Acheron brings power--wounds knit and skills are honed.\n\n";
		displayText.text += hero.getName () + "'s maximum and current health improve by " + "<color=#00ff00ff>" + levelUpHealthBonus + "</color>" + ". Combat Power improves by " + levelUpCombatPowerBonus + "!\n\n";
		foe = SpawnEnemy ();
		displayText.text += foe.getName() + " suddenly approaches, ready to do battle.\n\n";
		DisplayStats ();
		}

	public void GameOver ()	{
		SceneManager.LoadScene("GameOver");
	}


	}