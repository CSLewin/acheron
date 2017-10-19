using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	public Text displayText = null;
	public bool statsBeingDisplayed = false;


	Creature.Hero hero = new Creature.Hero("Captain Basharat Wellington", 60, "silver-chased tulwar", 15, 100, 0);
	Creature foe = new Creature("Test Goblin", 30, "poo-covered stick", 5, 35);

	void Awake () {
		displayText.text = null;
		}

	// Use this for initialization
	void Start () {

		foe = SpawnEnemy();

		Debug.Log("This line runs BEFORE I try to run SpawnEnemy()");
		SpawnEnemy();
		Debug.Log("This line runs AFTER I try to run SpawnEnemy()");

		displayText.text = hero.getName () + " has a Combat Power of " + hero.getCombatPower () + " and " +
		"<color=#00ff00ff>" + hero.getCurrentHealth () + "/" + hero.getMaxHealth () + "</color>" + " health.\n\n";

		displayText.text += hero.getName () + "'s " + hero.getWeaponName () + " will deal " + "<b><color=red>" + hero.getWeaponDamage () + "</color></b>" + " damage on a successful strike.\n";
		displayText.text += "A critical hit from the weapon will deal " + "<b><color=red>" + 2 * hero.getWeaponDamage () + "</color></b>" + " damage.\n\n";

		displayText.text += "* * * * * * * * * *\n\n";

		displayText.text += foe.getName () + " has a Combat Power of " + foe.getCombatPower () + " and " +
			"<color=#00ff00ff>" + foe.getCurrentHealth () + "/" + foe.getMaxHealth () + "</color>" + " health.\n\n";

		displayText.text += foe.getName () + "'s " + foe.getWeaponName () + " will deal " + "<b><color=red>" + foe.getWeaponDamage () + "</color></b>" + " damage on a successful strike.\n";
		displayText.text += "A critical hit from the weapon will deal " + "<b><color=red>" + 2 * foe.getWeaponDamage () + "</color></b>" + " damage.\n\n";

		}
	
	// Update is called once per frame
	void Update () {
		// Core combat loop: 
			// spawn enemy -> announce stats -> combat rounds -> 
				// (victory -> levelup -> spawn enemy) 
					// OR 
				// (defeat -> housekeeping -> restart/quit)
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
		//print("I ran SpawnEnemy.");
		//Debug.Log("SpawnEnemy has selected " + foe.getName());
		}


	}
	
