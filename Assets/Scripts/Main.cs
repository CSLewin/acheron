using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	public Text displayText = null;
	public bool statsBeingDisplayed = false;


	Hero hero = new Hero("Captain Basharat Wellington", 60, "silver-chased tulwar", 15, 100, 0);
	Creature foe = new Creature("Test Goblin", 30, "poo-covered stick", 5, 35);



	void Awake () {
		displayText.text = null;
		}

	// Use this for initialization
	void Start () {

		foe = SpawnEnemy();
		Debug.Log("SpawnEnemy has selected " + foe.getName());

		AnnounceStats ();

		}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.A)) {
			CombatSequence ();
		}

		// Core combat loop: 
			// spawn enemy -> announce stats -> combat rounds -> 
				// (victory -> levelup -> spawn enemy) 
					// OR 
				// (defeat -> housekeeping -> restart/quit)
		}

	public void CombatSequence(){
		AnnounceStats ();

		//hero attacks foe
		Fight (hero, foe);
		if (foe.isDead()) {
			HeroVictory ();
		} else Fight (foe, hero);

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
				else throw new System.InvalidOperationException("You've somehow rolled an 11 on a d10. Go look at SpawnEnemy().");
		}

	public void AnnounceStats() {
		displayText.text = hero.getName () + " has a Combat Power of " + hero.getCombatPower () + " and " +
			"<color=#00ff00ff>" + hero.getCurrentHealth () + "/" + hero.getMaxHealth () + "</color>" + " health.\n\n";

		displayText.text += hero.getName () + "'s " + hero.getWeaponName () + " will deal " + "<b><color=red>" + hero.getWeaponDamage () + "</color></b>" + " damage on a successful strike.\n";
		displayText.text += "A critical hit from the weapon will deal " + "<b><color=red>" + 2 * hero.getWeaponDamage () + "</color></b>" + " damage.\n\n";

		displayText.text += "* * * * * * * * * *\n\n";

		displayText.text += foe.getName () + " has a Combat Power of " + foe.getCombatPower () + " and " +
			"<color=#00ff00ff>" + foe.getCurrentHealth () + "/" + foe.getMaxHealth () + "</color>" + " health.\n\n";

		displayText.text += foe.getName () + "'s " + foe.getWeaponName () + " will deal " + "<b><color=red>" + foe.getWeaponDamage () + "</color></b>" + " damage on a successful strike.\n";
		displayText.text += "A critical hit from the weapon will deal " + "<b><color=red>" + 2 * foe.getWeaponDamage () + "</color></b>" + " damage.\n\n";

		displayText.text += "* * * * * * * * * *\n\n";

		displayText.text += "Press (A) to initiate a round of combat.\n\n";
	}

	public void Fight (Creature attacker, Creature defender) {
		int roll = Random.Range (1, 101);
		if (roll <= 5 && roll <= attacker.getCombatPower ()) {
			displayText.text += attacker.getName () + " rolled " + roll + " under " + attacker.getCombatPower () + "--<b><color=orange>A DEVASTATING STRIKE!</color></b>\n";
			int damage = attacker.getWeaponDamage();
			defender.applyDamage(2 * damage);
			displayText.text += defender.getName () + " takes " + 2 * damage + " damage.\n\n";
		
		} else if (roll <= attacker.getCombatPower ()) {
			displayText.text += attacker.getName () + " rolled " + roll + " under " + attacker.getCombatPower () + "--a hit!\n";
			int damage = attacker.getWeaponDamage();
			defender.applyDamage(damage);
			displayText.text += defender.getName () + " takes " + damage + " damage.\n\n";
		
		} else if (roll > attacker.getCombatPower ()) {
			displayText.text += attacker.getName () + " rolled " + roll + " over " + attacker.getCombatPower () + ". " + attacker.getName() + " misses!\n\n";
		}
	}

	public void HeroVictory() {
		displayText.text = foe.getName () + " is slain!\n";
		hero.addDefeatedFoe ();
		displayText.text += hero.getName () + " has triumphed over " + hero.getFoesDefeated() + " foes.\n\n";
		int levelUpHealthBonus = 10;
		int levelUpCombatPowerBonus = 2;
		hero.levelUp (levelUpHealthBonus,levelUpCombatPowerBonus);
		displayText.text += hero.getName () + " binds their wounds, gathers greater strength and prepares for the next challenge.\n\n";
		displayText.text += hero.getName () + "'s maximum and current health improved by " + levelUpHealthBonus + "!\n\n";
		displayText.text += hero.getName () + " has learned a few new tricks; their Combat Power has improved by " + levelUpCombatPowerBonus + "!\n\n";
		displayText.text += "* * * * * * * * * *\n\n";
		displayText.text += "Press (A) to enter combat with a new foe!\n\n";
		foe = SpawnEnemy();
	}


	}
	
