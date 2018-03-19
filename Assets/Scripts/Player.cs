using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool nudged;

	private GameObject playerSprite, gameSettings;
	private GameSettings gameSettingsData;
	private UsefulFunctions helper;

	private float playerX, playAreaLength;
	private float speedDelta, rotationDelta;
	private float horizontalSpeed, totalSpeed, finalSpeed = 0f;
	private float newRotation = 0;

	// Use this for initialization
	void Start () {
		playerSprite = GameObject.Find ("PlayerSprite");
		gameSettings = GameObject.Find ("GameSettings");

		helper = gameSettings.GetComponent<UsefulFunctions> ();
		gameSettingsData = gameSettings.GetComponent<GameSettings> ();
		playAreaLength = 0.5f - gameSettingsData.deathZone - gameSettingsData.restZone;
	}
	
	// Update is called once per frame
	void Update () {
		playerX = helper.GetGameObjectPosition (this.gameObject) [0];


		// Rotate if position outside set boundaries.
		if (helper.Between (gameSettingsData.deathZone, 0.5f - gameSettingsData.restZone, playerX)) {
			rotationDelta = 1 - ((playerX - gameSettingsData.deathZone) / playAreaLength);

		} else if (helper.Between (0.5f + gameSettingsData.restZone, 1f - gameSettingsData.deathZone, playerX)) {
			rotationDelta = -(playerX - gameSettingsData.restZone - 0.5f) / playAreaLength;

		} else if (playerX <= gameSettingsData.deathZone || playerX >= 1 - gameSettingsData.deathZone) {
			return;
		}

		//Increases Speed based on how far from the middle.
		//This probably belongs to Game Manager.

		// (0.4) || (0.6) == 1;
		// 4zx² - 4zx + z + c = 0; (z for acceleration, c for value on 0.5)
		speedDelta = (
		    (4f * playerX * playerX * gameSettingsData.accelAmp) -
		    (4f * playerX * gameSettingsData.accelAmp) +
		    1 * gameSettingsData.accelAmp + gameSettingsData.minAccelDelta
		);


		// Applies both speed and rotation to the GameObject and Sprite respectively.
		totalSpeed = horizontalSpeed * Time.deltaTime * speedDelta;
		finalSpeed = totalSpeed * gameSettingsData.difficultyAmp;
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
		horizontalSpeed = Random.Range (0, 2) * 2 - 1;
	}
}
