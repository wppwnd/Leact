using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap.Unity.Interaction;

public class leapHandVisibility : MonoBehaviour {

	public LeapHandController leapmgr;

	private SimpleLeapListener leapListener;

	// Use this for initialization
	void Start () {
		
		leapListener = leapmgr.GetComponent<SimpleLeapListener> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
