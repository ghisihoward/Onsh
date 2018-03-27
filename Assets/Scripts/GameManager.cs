using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject playingHUD, pauseHUD;

	private GameObject playerObject, settingsObject, timerObject;
	private GameSettings settings;
	private Player playerScript;
	private Text timerText;

	private float innerTimer, totalTimer = 0;

	private enum GameState { Playing, Paused }
	private GameState currentState = GameState.Paused;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find ("Player");
		playerScript = playerObject.GetComponent<Player> ();

		settingsObject = GameObject.Find ("GameSettings");
		settings = settingsObject.GetComponent<GameSettings> ();

		timerObject = GameObject.Find ("Timer");
		timerText = timerObject.GetComponent<Text> ();

		playingHUD.SetActive (false);
		pauseHUD.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		// GameState (changes if player is on the deathzone)
		if (currentState == GameState.Playing) {
			// Timer
			innerTimer += Time.deltaTime;
			totalTimer += Time.deltaTime;
			timerText.text = totalTimer.ToString ("#0.0's'");

			// This random nudge has to be refactored later.
			if (innerTimer >= settings.nudgeInterval) {
				if (UsefulFunctions.Between (
					-settings.restZone, 
					settings.restZone,
					UsefulFunctions.GetGameObjectXFromCenter (playerObject))
				) {
					playerScript.ReceiveNudge (1, UsefulFunctions.GetRandomDirection ());
					innerTimer = 0;
				}
			}
		} else if (currentState == GameState.Paused) {
		}

		// TODO
		// Menu
		// Wave System
	}

	public void PlayButton () {
		currentState = GameState.Playing;
		playingHUD.SetActive (true);
		pauseHUD.SetActive (false);
	}

	public void MouseDrag (UsefulFunctions.Direction direction) {
		// Set on Game Manager for possible interaction on menus if in certain GameState.
		playerScript.ReceiveDrag (settings.dragImpact, direction);
	}
}
