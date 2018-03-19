using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulFunctions : MonoBehaviour {

	private new Camera camera;

	void Start () {
		camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
	}

	/// <summary>
	/// Gets the game object position.
	/// </summary>
	/// <returns>Returns a Vector2 with two values from 0f to 1f and
	/// uses the World to Viewport Point function to do so.
	/// Basically, (0,0) is the lower left, (1,1) is the top right;
	/// Simply put, 0.5 is the middle of the screen.</returns>
	/// <param name="thing">Thing.</param>
	public Vector2 GetGameObjectPosition (GameObject thing) {
		return (
		    new Vector2 (
			    (float)camera.WorldToViewportPoint (thing.transform.position).x,
			    (float)camera.WorldToViewportPoint (thing.transform.position).y
		    )
		);
	}

	/// Returns true if value is between min and max and false if it does not.
	public bool Between (int min, int max, int value, bool inclusive = false) {
		return this.Between ((float)min, (float)max, (float)value, inclusive);
	}

	/// Returns true if value is between min and max and false if it does not.
	public bool Between (float min, float max, float value, bool inclusive = false) {
		if (inclusive) {
			return (
			    (min <= value) &&
			    (value <= max)
			);
		} else {
			return (
			    (min < value) &&
			    (value < max)
			);
		}
	}
}
