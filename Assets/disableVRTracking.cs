﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableVRTracking : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UnityEngine.VR.InputTracking.disablePositionalTracking = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
