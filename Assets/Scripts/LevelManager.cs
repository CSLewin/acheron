using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadGame() {
		SceneManager.LoadScene("Game");
		Debug.Log("Level load requested for main game");
	}
}
