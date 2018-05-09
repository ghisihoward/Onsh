using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	private bool lifted;
	private Vector2 clickStart, clickEnd;

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
			clickStart = CastRayToClick (Input.mousePosition.x, Input.mousePosition.y);
		}

		if (Input.GetMouseButtonUp (0)) {
			clickEnd = CastRayToClick (Input.mousePosition.x, Input.mousePosition.y);
			lifted = true;
		}

		if (lifted) {
			EvalDragX (clickStart.x, clickEnd.x);
		}
	}

	private Vector2 CastRayToClick (float mouseX, float mouseY) {
		return new Vector2 (
			Camera.main.ScreenToViewportPoint(new Vector3 (mouseX, mouseY, 0)).x,
			Camera.main.ScreenToViewportPoint(new Vector3 (mouseX, mouseY, 0)).y
		);
	}

	private void EvalDragX (float start, float end) {
		float dragDistance = (end - start) * 100;

		if (Mathf.Abs (dragDistance) > (float)settings.minDragDistance) {
			if (dragDistance > 0) {
				gameManager.MouseDrag (UsefulFunctions.Direction.Right);
			} else if (dragDistance < 0) {
				gameManager.MouseDrag (UsefulFunctions.Direction.Left);
			}
		}
	}
}