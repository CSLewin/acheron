using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

	private string creatureName;
	private int combatPower;
	private int weaponDamage;
	private int maxHealth;
	private int damageTaken;

	private int foesDefeated;

	//TODO Move these to a class extension of Creature called Hero. Or use them to level up enemies...?
	//But actually, these are a leaky abstraction and should be turned into something smarter.
	public static int HEALTH_VICTORY_BONUS = 10;
	public static int COMBAT_VICTORY_BONUS = 2;


	public Creature (string name, int combatPower, int weaponDamage, int maxHealth)
	{
		this.creatureName = name;
		this.combatPower = combatPower;
		this.weaponDamage = weaponDamage;
		this.maxHealth = maxHealth;
		this.damageTaken = 0;
	}

	public bool isDead () {
		return damageTaken >= maxHealth;
	}

	public void applyDamage (int damage) {
		this.damageTaken += damage;
	}

	public void healDamage (int damage)	{
		this.damageTaken -= damage;
		if (damageTaken < 0) {
			damageTaken = 0;
		}
	}

	public int getCurrentHealth () {
		if (isDead ()) {
			return 0;
		} else {
			return maxHealth - damageTaken;
		}
	}

	public string getName () {
		return creatureName;
	}

	public int getCombatPower() {
		return combatPower;
	}

	public int getWeaponDamage () {
		return weaponDamage;
	}

	public int getMaxHealth() {
		return maxHealth;
	}

	protected void setMaxHealth (int maxHealth) {
		this.maxHealth = maxHealth;
	}

	protected void setCombatPower (int combatPower) {
		this.combatPower = combatPower;
	}

	public void levelUp () {
		this.setMaxHealth(getMaxHealth() + HEALTH_VICTORY_BONUS);
		this.healDamage(HEALTH_VICTORY_BONUS);
		this.setCombatPower(getCombatPower() + COMBAT_VICTORY_BONUS);
	}

	public void addDefeatedFoe() {
		foesDefeated++;
	}

	public int getFoesDefeated () {
		return foesDefeated;
	}

}
