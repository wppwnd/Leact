using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap.Unity.Interaction;

public class LeactManager : MonoBehaviour {

	public KinectManager kinectmgr;
	private SimpleHandListener handListener;
	public LeapHandController leapmgr;
	private SimpleLeapListener leapListener;
	public UnityEngine.UI.Text gestureInfoRightScreen;
	public GameObject objToSpawn;

	public Anchor anchor;
	public AnchorGroup agroup;

	public GameObject leftPalm;

	// Use this for initialization
	void Start () {


		handListener = kinectmgr.GetComponent<SimpleHandListener> ();
		leapListener = leapmgr.GetComponent<SimpleLeapListener> ();
	}
	
	// Update is called once per frame
	void Update () {

		gestureInfoRightScreen.text = getOnScreenText ();


		//spawn something if the kinect detects a click but leap no hand
		if (handListener.isClickedLeft_m () & leapListener.getLeftHandEnter() & handListener.getLeftHandQuadrant ) {
			//spawn a ball or something like this
			leapListener.attachToLeftHand(anchor,agroup,objToSpawn);
		
		}

		
	}

	private string getOnScreenText(){
		string tmp = "";
		if (leapListener.leftHandDetected ()) {
			tmp = tmp + "left hand detected. ";
		}
		if (leapListener.rightHandDetected ()) {
			tmp = tmp + "right hand detected. ";
		}

		return tmp;

	}
}
