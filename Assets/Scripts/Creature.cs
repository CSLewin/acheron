using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature {

	// All Creatures have the following properties:
	protected string creatureName;
	private int combatSkill;
	private string weaponName;
	private int weaponDamage;
	private int maxHealth;
	private int damageTaken;

	// This is the object that'll get called with parameters in order to actually create a monster.
	public Creature (string name, int combatSkill, string weaponName, int weaponDamage, int maxHealth)	
	{
		this.creatureName = name;
		this.combatSkill = combatSkill;
		this.weaponName = weaponName;
		this.weaponDamage = weaponDamage;
		this.maxHealth = maxHealth;
		this.damageTaken = 0;
	}

	// These are the functions you can use on/with a creature.
	public bool isDead ()
	{
		return damageTaken >= maxHealth;
	}

	// functions for dealing with damage
	public void applyDamage (int damage)
	{
		this.damageTaken += damage;
	}

	public void healDamage (int damage)
	{
		this.damageTaken -= damage;
		if (damageTaken < 0) {
			damageTaken = 0;
		}
	}

	// functions for getting and setting current and maximum health values
	public int getCurrentHealth ()
	{
		if (isDead ()) {
			return 0;
		} else {
			return maxHealth - damageTaken;
		}
	}

	public int getMaxHealth ()
	{
		return maxHealth;
	}

	protected void setMaxHealth (int maxHealth)
	{
		this.maxHealth = maxHealth;
	}

	// functions for getting and setting combat power
	public int getCombatSkill ()
	{
		return combatSkill;
	}

	protected void setCombatSkill (int combatSkill)
	{
		this.combatSkill = combatSkill;
	}

	// functions for getting and setting weapon name and damage
	public string getWeaponName ()
	{
		return weaponName;
	}

	public void setWeaponName (string weaponName)
	{
		this.weaponName = weaponName;
	}

	public int getWeaponDamage ()
	{
		return weaponDamage;
	}

	protected void setWeaponDamage (int weaponDamage)
	{
		this.weaponDamage = weaponDamage;
	}

	// Functions for miscellaneous creature stuff that still ought to be encapsulated
	public string getName ()
	{
		//return creatureName;
		return "<b><color=blue>" + creatureName + "</color></b>"; // always display a creature's name in bold
	}

}


// Hero is a subclass of creature, inheriting all the properties thereof and having at least one additional property.
public class Hero : Creature {

	private int foesDefeated;
	private int mechanicalSkill;
	private int diplomacySkill;

	public Hero (string name, int combatSkill, int mechanicalSkill, int diplomacySkill, string weaponName, int weaponDamage, int maxHealth, int foesDefeated)
		: base (name, combatSkill, weaponName, weaponDamage, maxHealth)
	{
		this.creatureName = "<color=#f7d171>" + name + "</color>"; // While all creature names are bold, the hero's name should always be a fancy gold color.
		this.mechanicalSkill = mechanicalSkill;
		this.diplomacySkill = diplomacySkill;
		this.foesDefeated = 0;
	}

	public int getMechanicalSkill ()
	{
		return mechanicalSkill;
	}

	protected void setMechanicalSkill (int mechanicalSkill)
	{
		this.mechanicalSkill = mechanicalSkill;
	}

	public int getDiplomacySkill ()
	{
		return diplomacySkill;
	}

	protected void setDiplomacySkill (int diplomacySkill)
	{
		this.diplomacySkill = diplomacySkill;
	}

	public void addDefeatedFoe ()
	{
		foesDefeated++;
	}

	public int getFoesDefeated ()
	{
		return foesDefeated;
	}

	public void levelUp (int healthBonus, int combatSkillBonus, int mechanicalSkillBonus, int diplomacySkillBonus)
	{
		this.setMaxHealth (getMaxHealth () + healthBonus);
		this.healDamage (healthBonus);
		this.setCombatSkill (getCombatSkill () + combatSkillBonus);
		this.setMechanicalSkill (getMechanicalSkill () + mechanicalSkillBonus);
		this.setDiplomacySkill (getDiplomacySkill () + diplomacySkillBonus);
	}

}