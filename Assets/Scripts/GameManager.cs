using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private GameObject player, settingsObject;
	private GameSettings settings;
	private Player playerScript;

	private float innerTimer, totalTimer = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerScript = player.GetComponent<Player> ();

		settingsObject = GameObject.Find ("GameSettings");
		settings = settingsObject.GetComponent<GameSettings> ();
	}
	
	// Update is called once per frame
	void Update () {
		innerTimer += Time.deltaTime;
		totalTimer += Time.deltaTime;

		if (innerTimer >= settings.nudgeInterval) {

			// TODO
			// This random nudge thing should be refactored later.
			playerScript.ReceiveNudge (1, UsefulFunctions.GetRandomDirection ());
			innerTimer = 0;
		}

		// TODO
		// Timer
		// Wave System
		// Game States (changes if player is on the deathzone)
		// Menu
	}

	public void MouseDrag (UsefulFunctions.Direction direction){
		// TODO
		// Act on Drag
		playerScript.ReceiveDrag (settings.dragImpact, direction);
	}
}
