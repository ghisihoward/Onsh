using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UsefulFunctions {

	private static Camera camera;

	/// <summary>
	/// Gets the game object position.
	/// </summary>
	/// <returns>Returns a Vector2 with two values from 0f to 1f and
	/// uses the World to Viewport Point function to do so.
	/// Basically, (0,0) is the lower left, (1,1) is the top right;
	/// Simply put, 0.5 is the middle of the screen.</returns>
	/// <param name="thing">GameObject.</param>
	public static Vector2 GetGameObjectPosition (GameObject thing) {
		camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		return (
		    new Vector2 (
				(float)camera.WorldToViewportPoint (thing.transform.position).x,
				(float)camera.WorldToViewportPoint (thing.transform.position).y
		    )
		);
	}

	/// <summary>
	/// Gets the accurate game object position.
	/// </summary>
	/// <returns>The game object position after setting 0 as the center of the screen.</returns>
	/// <param name="thing">GameObject.</param>
	public static float GetGameObjectPositionFromZero (GameObject thing) {
		return GetGameObjectPosition (thing)[0] - 0.5f;
	}

	/// Returns true if value is between min and max and false if it does not.
	public static bool Between (int min, int max, int value, bool inclusive = false) {
		return Between ((float)min, (float)max, (float)value, inclusive);
	}

	/// Returns true if value is between min and max and false if it does not.
	public static bool Between (float min, float max, float value, bool inclusive = false) {
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
