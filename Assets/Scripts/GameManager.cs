using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject playingHUD, pauseHUD;
	public Text pauseText2, pauseText4;

	private float innerTimer = 0f, totalTimer = 0f, recordTimer = 0f;

	private GameObject playerObject, settingsObject, timerObject;
	private Player playerScript;
	private GameSettings settings;
	private Text timerText;

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
			// Check if player is standing
			if (!playerScript.standing) {
				this.PlayerCaiu ();
			}

			// Timer
			innerTimer += Time.deltaTime;
			totalTimer += Time.deltaTime;
			timerText.text = totalTimer.ToString ("#0.00's'");

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
		// Wave System
	}

	public void PlayerCaiu () {
		currentState = GameState.Paused;
		playingHUD.SetActive (false);
		pauseHUD.SetActive (true);

		if (totalTimer > recordTimer) recordTimer = totalTimer;
		
		pauseText2.text = totalTimer.ToString ("#0.00's'");
		pauseText4.text = recordTimer.ToString ("#0.00's'");
	}

	public void PlayButton () {
		currentState = GameState.Playing;
		playingHUD.SetActive (true);
		pauseHUD.SetActive (false);

		playerScript.ResetPlayer ();

		innerTimer = 0;
		totalTimer = 0;
	}

	public void MouseDrag (UsefulFunctions.Direction direction) {
		// Set on Game Manager for possible interaction on menus if in certain GameState.
		playerScript.ReceiveDrag (settings.dragImpact, direction);
	}
}
