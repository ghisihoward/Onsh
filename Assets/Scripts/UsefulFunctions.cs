using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulFunctions : MonoBehaviour {

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
