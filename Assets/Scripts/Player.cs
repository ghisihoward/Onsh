using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool nudged;
	public int amplitude = 1;

	public float modMaxRotation = 40f;

	private Camera camera;
	private GameObject playerSprite;

	private float horizontalSpeed = 0f;
	private float deathZone, restZone;
	private float playAreaLength;
	private float playerPosition;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		playerSprite = GameObject.Find ("PlayerSprite");
		deathZone = GameObject.Find ("GameManager").GetComponent<GameManager> ().deathZone;
		restZone = GameObject.Find ("GameManager").GetComponent<GameManager> ().restZone;
		playAreaLength = 0.5f - deathZone - restZone;
		Vector2 playArea = new Vector2 (0f + deathZone, 0.5f - restZone);
	}
	
	// Update is called once per frame
	void Update () {
		// rotates and keeps moving
		// if lost balance, falls
		this.transform.Translate (
			horizontalSpeed * Time.deltaTime * amplitude, 0, 0
		);

		playerPosition = getPlayerPosition () [0];
		if (playerPosition < (0.5f - restZone) &&
		    playerPosition > deathZone) {

			float newRotation = 
				modMaxRotation - (
				    modMaxRotation * (
				        (playerPosition - restZone - deathZone) / playAreaLength
				    )
				);
			playerSprite.transform.eulerAngles = new Vector3 (0, 0, newRotation);
		}
	}

	/// Mock of a function that
	/// nudges the player randomly.
	public void NudgePlayerRandomly () {
		if (nudged)
			return;
		
		nudged = true;
		horizontalSpeed = Random.Range (0, 2) * 2 - 1;
	}

	/// Returns a Vector2 with two values from 0f to 1f and
	/// uses the World to Viewport Point function to do so.
	/// Basically, (0,0) is the lower left, (1,1) is the top right;
	/// Simply put, 0.5 is the middle of the screen.
	public Vector2 getPlayerPosition () {
		return (
		    new Vector2 (
			    (float)camera.WorldToViewportPoint (this.transform.position).x,
			    (float)camera.WorldToViewportPoint (this.transform.position).y
		    )
		);
	}
}
