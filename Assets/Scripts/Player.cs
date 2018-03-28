using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private GameObject playerSprite, settingsObject;
	private GameSettings settings;

	public bool standing = true;
	private bool nudged = false, dragged = false;

	private float playerX, sideLength, marginLeft, marginRight;
	private float speedDelta, rotationDelta;
	private float totalSpeed, finalSpeed = 0f;
	private float nudgeSpeed, nudgeCounter = 0f;
	private float dragSpeed, dragCounter = 0f;
	private float newRotation = 0f;

	private Animator animator;

	// Use this for initialization
	void Start () {
		this.playerSprite = GameObject.Find ("PlayerSprite");
		this.settingsObject = GameObject.Find ("GameSettings");
		this.settings = settingsObject.GetComponent<GameSettings> ();
		this.animator = gameObject.GetComponentInChildren <Animator> ();

		this.marginLeft = settings.deathZone - 0.5f;
		this.marginRight = 0.5f - settings.deathZone;
		this.sideLength = 0.5f - settings.deathZone - settings.restZone;
	}
	
	// Update is called once per frame
	void Update () {
		// Verify if player has already fallen, return if positive.
		if (!standing) return;

		// Update playerX.
		playerX = UsefulFunctions.GetGameObjectXFromCenter (this.gameObject);

		// Verify if player is falling right now, if so, fire it's anim.
		if (this.EvaluateFall ()) return;

		this.BalancePlayer ();
		this.RotatePlayer ();

		// If nudged, move the player a set time.
		if (nudged) {
			this.NudgePlayer ();
		}

		// If dragged, move the player a set time.
		if (dragged) {
			this.DragPlayer();
		}

		// Verify if the player fell.
	}

	private void BalancePlayer () {
		// Increases Speed based on how far from the middle.
		// z60x² + c = 0; (z for acceleration, c for value on 0)
		// (-0.1) || (0.1) == 1 for minAccelDelta = 0.4;
		if (playerX != 0f) {
			speedDelta = (
			    (60f * playerX * playerX * settings.accelAmp) +
			    settings.minAccelDelta
			);
		} else {
			speedDelta = 0f;
		}

		if (playerX < 0)
			speedDelta *= -1;

		// Applies both speed and rotation to the GameObject and Sprite respectively.
		totalSpeed = settings.difficultyAmp * speedDelta;
		finalSpeed = totalSpeed * Time.deltaTime;
		this.transform.Translate (finalSpeed, 0, 0);

		// If moving left, character has to face left.
		if (
			(totalSpeed > 0) && (settings.playerOriginalFacing != UsefulFunctions.Direction.Right) ||
			(totalSpeed < 0) && (settings.playerOriginalFacing != UsefulFunctions.Direction.Left)) 
		{
			playerSprite.transform.localScale = new Vector3 (-1f, 1f, 1f);
		} else {
			playerSprite.transform.localScale = new Vector3 (1f, 1f, 1f);
		}
	}

	private void RotatePlayer () {
		// Rotate if position outside set boundaries.
		if (UsefulFunctions.Between (-0.5f, -settings.restZone, playerX)) {
			rotationDelta = 
				Mathf.Clamp (
					1 - ((0.5f + playerX - settings.deathZone) / sideLength), 0f, 1f
				);
		} else if (UsefulFunctions.Between (settings.restZone, 0.5f, playerX)) {
			rotationDelta = 
				Mathf.Clamp (
					-(playerX - settings.restZone) / sideLength, -1f, 0f
				);
		} else {
			rotationDelta = 0;
		}

		newRotation = settings.modMaxRotation * rotationDelta;
		playerSprite.transform.eulerAngles = new Vector3 (0, 0, newRotation);
	}

	private bool EvaluateFall () {
		if (!UsefulFunctions.Between (marginLeft, marginRight, playerX)) {
			animator.enabled = false;
			standing = false;
			return true;
		}

		return false;
	}

	public void ReceiveNudge (float ammount, UsefulFunctions.Direction direction) {
		if (nudged)
			return;

		nudged = true;
		nudgeSpeed = ammount * settings.difficultyAmp / settings.nudgeResistance;

		if (direction == UsefulFunctions.Direction.Left)
			nudgeSpeed *= -1;
	}

	void NudgePlayer () {
		this.transform.Translate (nudgeSpeed, 0, 0);
		nudgeCounter += Time.deltaTime;

		if (nudgeCounter >= settings.nudgeDuration) {
			nudgeCounter = 0;
			nudged = false;
		}
	}

	public void ReceiveDrag (float ammount, UsefulFunctions.Direction direction) {
		dragged = true;
		dragSpeed = ammount * settings.accelAmp / settings.dragResistance;

		if (direction == UsefulFunctions.Direction.Left)
			dragSpeed *= -1;
	}

	void DragPlayer () {
		this.transform.Translate (dragSpeed, 0, 0);
		dragCounter += Time.deltaTime;

		if (dragCounter >= settings.dragDuration) {
			dragCounter = 0;
			dragged = false;
		}
	}

	public void ResetPlayer () {
		transform.position = new Vector3 (0f, 0f, 0f);
		animator.enabled = true;
		standing = true;
	}
}
