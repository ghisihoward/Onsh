using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {
	
	[Range (0f, 1f)]
	public float restZone = 0.1f;

	[Range (0f, 1f)]
	public float deathZone = 0.15f;

	[Range (1f, 30f)]
	public float difficultyAmp = 1f;

	[Range (40f, 300f)]
	public float accelAmp = 40f;

	[Range (0f, 1f)]
	public float minAccelDelta = 0.4f;

	[Range (0f, 90f)]
	public float modMaxRotation = 40f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
