using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoePicker : MonoBehaviour {

	public void SpawnEnemy () {
	int EnemyPicker = Random.Range(1,10);
		if (EnemyPicker == 1) {Creature foe = new Creature("Animus Mold", Random.Range(15,26), "moldy bodyslam", 2, 30);}
		if (EnemyPicker == 2) {Creature foe = new Creature("Leering Svartælf", Random.Range(15,26), "jagged glassine spear", 2, 30);}
		if (EnemyPicker == 3) {Creature foe = new Creature("Escaped Sporehound", Random.Range(15,26), "saw-toothed gills", 5, 35);}
		if (EnemyPicker == 4) {Creature foe = new Creature("Knocker Clan Sapper", Random.Range(35,46), "rocksalt blunderbuss", 5, 50);}
		if (EnemyPicker == 5) {Creature foe = new Creature("Conquerer Legion Chainmaster", Random.Range(35,46), "barbed bronze scourge", 10, 50);}
		if (EnemyPicker == 6) {Creature foe = new Creature("Bog Priest", Random.Range(35,46), "charmed and blackened claws", 10, 50);}
		if (EnemyPicker == 7) {Creature foe = new Creature("Frothing Jotunkin", Random.Range(55,65), "etched iron mallet", 25, 70);}
		if (EnemyPicker == 8) {Creature foe = new Creature("Seething Rune-Slave", Random.Range(55,65), "crushing grasp", 25, 70);}
		if (EnemyPicker == 9) {Creature foe = new Creature("Serpent-Priest Yddremel The Devoted", Random.Range(55,65), "crashing ophidian spellcraft", 45, 70);}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
