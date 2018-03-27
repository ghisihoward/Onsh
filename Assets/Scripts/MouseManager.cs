using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	private bool lifted;
	private Vector2 clickBegin, clickEnd;

	private GameObject settingsObject;
	private GameSettings settings;

	private GameObject gameManagerObject;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		settingsObject = GameObject.Find ("GameSettings");
		settings = settingsObject.GetComponent<GameSettings> ();

		gameManagerObject = GameObject.Find ("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		lifted = false;

		if (Input.GetMouseButtonDown (0)) {
			clickBegin = CastRayToClick (Input.mousePosition.x, Input.mousePosition.y);
		}

		if (Input.GetMouseButtonUp (0)) {
			clickEnd = CastRayToClick (Input.mousePosition.x, Input.mousePosition.y);
			lifted = true;
		}

		if (lifted) {
			float dragDistance = (clickEnd.x - clickBegin.x) * 100;

			if (Mathf.Abs (dragDistance) > (float)settings.minDragDistance) {
				if (dragDistance > 0) {
					gameManager.MouseDrag (UsefulFunctions.Direction.Right);
				} else if (dragDistance < 0) {
					gameManager.MouseDrag (UsefulFunctions.Direction.Left);
				}
			}
		}
	}

	private Vector2 CastRayToClick (float mouseX, float mouseY) {
		return new Vector2 (
			Camera.main.ScreenToViewportPoint(new Vector3 (mouseX, mouseY, 0)).x,
			Camera.main.ScreenToViewportPoint(new Vector3 (mouseX, mouseY, 0)).y
		);
	}
}