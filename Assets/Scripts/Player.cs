using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool nudged;

	private GameObject playerSprite, gameSettings;
	private GameSettings gameSettingsData;

	private float playerX, playAreaLength;
	private float speedDelta, rotationDelta;
	private float totalSpeed, finalSpeed = 0f;
	private float nudgeSpeed, nudgeCounter = 0f;
	private float newRotation = 0f;

	// Use this for initialization
	void Start () {
		playerSprite = GameObject.Find ("PlayerSprite");
		gameSettings = GameObject.Find ("GameSettings");

		gameSettingsData = gameSettings.GetComponent<GameSettings> ();
		playAreaLength = 0.5f - gameSettingsData.deathZone - gameSettingsData.restZone;
	}
	
	// Update is called once per frame
	void Update () {
		playerX = UsefulFunctions.GetGameObjectPositionFromZero(this.gameObject);

		// Rotate if position outside set boundaries.
		if (UsefulFunctions.Between (gameSettingsData.deathZone - 0.5f, -gameSettingsData.restZone, playerX)) {
			rotationDelta = 1 - ((0.5f + playerX - gameSettingsData.deathZone) / playAreaLength);

		} else if (UsefulFunctions.Between (gameSettingsData.restZone, 0.5f - gameSettingsData.deathZone, playerX)) {
			rotationDelta = -(playerX - gameSettingsData.restZone) / playAreaLength;

		} else if (gameSettingsData.deathZone - 0.5f >= playerX || playerX >= 0.5f - gameSettingsData.deathZone) {
			return;
		}

		// Increases Speed based on how far from the middle.
		// Should this belong to Game Manager?

		// z60² + c = 0; (z for acceleration, c for value on 0)
		// (-0.1) || (0.1) == 1;
		// (-0.5) || (0.5) == 15.4;
		// for minAccelDelta = 0.4;
		speedDelta = (
		    (60f * playerX * playerX * gameSettingsData.accelAmp) +
			gameSettingsData.minAccelDelta
		);

		if (playerX < 0) { speedDelta *= -1;	}

		// If nudged, move the player a set time.
		if (nudged) {
			this.transform.Translate (nudgeSpeed, 0, 0);
			nudgeCounter += Time.deltaTime;

			if (nudgeCounter >= gameSettingsData.nudgeTime) {
				nudgeCounter = 0;
				nudged = false;
			}
		}

		// Applies both speed and rotation to the GameObject and Sprite respectively.
		totalSpeed = gameSettingsData.difficultyAmp * speedDelta;
		finalSpeed = totalSpeed * Time.deltaTime;
		this.transform.Translate (finalSpeed, 0, 0);

		newRotation = gameSettingsData.modMaxRotation * rotationDelta;
		playerSprite.transform.eulerAngles = new Vector3 (0, 0, newRotation);
	}

	/// Mock of a function that
	/// nudges the player randomly.
	public void NudgePlayerRandomly () {
		if (nudged)
			return;
		
		nudged = true;
		nudgeSpeed = Random.Range (0, 2) * 2 - 1;
		nudgeSpeed /= 10f;
	}
}
