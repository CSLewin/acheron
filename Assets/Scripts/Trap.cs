using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap {

	// All traps have the following properties:
	protected string trapName; //A "hallway of scything blades" stands before you.
	private int trapDisarmDifficultyValue; //Secretly modifies result of Mechanics roll. -0, -15, -30
	private string trapDisarmDifficultyDescription; //This trap... 
		//At -0, "<color=white>looks simple to disarm</color>" 
		//At -15, "<color=yellow>is of concerning difficulty to bypass</color>" 
		//At -30, "<color=orange>is fiendishly complex to understand, let alone disable</color>"
	private string trapDamageDescription; //Like Creature weapon description. "Whirling blades" | "Crushing stone block" | "fusillade of envenomed darts" etc.
	private int trapDamage; //Numerical damage dealt
	private int trapMaxHealth; //How much damage the trap can endure before being smashed apart.
	private int damageTaken; //As in Creature

	// This is the object that'll get called with parameters in order to actually create a trap.
	public Trap (string trapName, int trapDisarmDifficultyValue, string trapDamageDescription, int trapDamage, int trapMaxHealth)
	{
		this.trapName = trapName;
		if (trapDisarmDifficultyValue == 0) 
			{
				this.trapDisarmDifficultyValue = 0;
				this.trapDisarmDifficultyDescription = "<color=white>looks simple to disarm (-0 to Mechanical Skill)</color>";
			}
		if (trapDisarmDifficultyValue == 1) 
			{
				this.trapDisarmDifficultyValue = -15;
			this.trapDisarmDifficultyDescription = "<color=yellow>is of concerning difficulty to bypass (-15% to Mechanical Skill)</color>";
			}
		if (trapDisarmDifficultyValue == 2) 
			{
				this.trapDisarmDifficultyValue = -30;
			this.trapDisarmDifficultyDescription = "<color=orange>is fiendishly complex to understand, let alone disable (-30% to Mechanical Skill)</color>";
			}
			//else throw new System.InvalidOperationException("trapDisarmDifficultyValue is not an acceptable value.");
		this.trapDamageDescription = trapDamageDescription;
		this.trapDamage = trapDamage;
		this.trapMaxHealth = trapMaxHealth;
		this.damageTaken = 0;
	}

	public bool isDead ()
	{
		return damageTaken >= trapMaxHealth;
	}

	public int getCurrentHealth ()
	{
		if (isDead ()) {
			return 0;
		} else {
			return trapMaxHealth - damageTaken;
		}
	}

	public int getMaxHealth ()
	{
		return trapMaxHealth;
	}

	protected void setMaxHealth (int maxHealth)
	{
		this.trapMaxHealth = maxHealth;
	}

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

	// functions for getting and setting trapDisarmDifficultyValue
	public int getDisarmDifficulty ()
	{
		return trapDisarmDifficultyValue;
	}

	protected void setDisarmDifficulty (int trapDisarmDifficultyValue)
	{
		this.trapDisarmDifficultyValue = trapDisarmDifficultyValue;
	}

	// functions for getting and setting trapDisarmDifficultyDescription
	public string getDisarmDifficultyDescription ()
	{
		return trapDisarmDifficultyDescription;
	}

	protected void setDisarmDifficultyDescription (string trapDisarmDifficultyDescription)
	{
		this.trapDisarmDifficultyDescription = trapDisarmDifficultyDescription;
	}

	// functions for getting and setting trap damage description and damage value
	public string getDamageDescription ()
	{
		return trapDamageDescription;
	}

	public void setDamageDescription (string trapDamageDescription)
	{
		this.trapDamageDescription = trapDamageDescription;
	}

	public int getTrapDamage ()
	{
		return trapDamage;
	}

	protected void setTrapDamage (int trapDamage)
	{
		this.trapDamage = trapDamage;
	}

	// Functions for miscellaneous creature stuff that still ought to be encapsulated
	public string getTrapName ()
	{
		//return creatureName;
		return trapName;
	}

}
