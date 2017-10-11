using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameActionDisplayController : MonoBehaviour {

string gameAction;
public Text gameActionDisplayController; //only use this to display the player's stats on the screen.

public static int diceroll (int dicepool, int sides) {
	int i = dicepool;
	int total = 0;
	string results = "";
	while (i > 0) 
		{
			int roll = Random.Range(1,sides+1);
			total = total + roll;
			i -= 1;
			results = results + roll.ToString() + " ";
		}
	print (results);
	return total;
		}

	// Use this for initialization
	void Start () {
		StartGame();

		//string testroll;

		//PlayerDataController.Roll(80) = testroll;
		//print ("Testroll from PlayerDataController.cs = " + testroll);
		print ("I should have rolled 3d6; I got a result of " + diceroll(3,6));

	}

	void StartGame () {
		PrintAction();
	}

	void PrintAction() {
		gameAction = 
			"Press (Spacebar) to attack the " + EnemyDataController.enemyStats;

		gameActionDisplayController.text = gameAction;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
