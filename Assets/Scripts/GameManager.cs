using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private GameObject player, gameSettings;
	private GameSettings gameSettingsData;
	private Player playerScript;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerScript = player.GetComponent<Player> ();

		gameSettings = GameObject.Find ("GameSettings");
		gameSettingsData = gameSettings.GetComponent<GameSettings> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Checks if player is on center
		// Use World to Viewport Point (Close to 0,5);
		// if it is, nudges the player on a random direction on random timespaces
		//if (UsefulFunctions.Between (
		//	    0.5f - gameSettingsData.deathZone, 
		//	    0.5f + gameSettingsData.deathZone, 
		//		UsefulFunctions.GetGameObjectPosition (player) [0], 
		//	    true
		//    )) {
		//	playerScript.NudgePlayerRandomly ();
		//}
	}

}
