using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[Range (0f, 1f)]
	public float restZone = 0.1f;

	[Range (0f, 1f)]
	public float deathZone = 0.15f;

	private GameObject player;
	private Player playerScript;
	private Camera camera;
	private UsefulFunctions helper;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		player = GameObject.Find ("Player");

		playerScript = player.GetComponent<Player> ();
		helper = this.GetComponent<UsefulFunctions> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Checks if player is on center
		// Use World to Viewport Point (Close to 0,5);
		// if it is, nudges the player on a random direction on random timespaces
		if (helper.Between (0.5f - deathZone, 0.5f + deathZone, playerScript.getPlayerPosition () [0], true)) {
			playerScript.NudgePlayerRandomly ();
			
		}
	}

}
