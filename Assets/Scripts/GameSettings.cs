using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

	[Range (0f, 1f)]
	public float restZone = 0.1f;

	[Range (0f, 1f)]
	public float deathZone = 0.15f;

	[Range (1f, 10f)]
	public float difficultyAmp = 1f;

	[Range (3f, 20f)]
	public float accelAmp = 3;

	[Range (0f, 1f)]
	public float minAccelDelta = 0.4f;

	[Range (0f, 90f)]
	public float modMaxRotation = 40f;

	[Range (0f, 2f)]
	public float nudgeDuration = 0.5f;

	[Range (0f, 1000f)]
	public float nudgeResistance = 100f;

	[Range (0f, 5f)]
	public float nudgeInterval = 3f;

	public UsefulFunctions.Directions playerOriginalFacing = UsefulFunctions.Directions.Right;
}
