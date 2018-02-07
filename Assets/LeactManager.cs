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
//		if (handListener.isClickedLeft_m()) {
//			//Debug.Log ("isClickedLeft true");
//		}
//
//		if (handListener.isClickedRight_m()) {
//			//Debug.Log ("isClickedRight true");
//		}
//
//		if (leapListener.leftHandDetected()) {
//			Debug.Log ("left leaphand detected");
//		}
//
//		if (leapListener.rightHandDetected()) {
//			Debug.Log ("Right leaphand detected");
//		}

		gestureInfoRightScreen.text = getOnScreenText ();


		//spawn something if the kinect detects a click but leap doesnt
		if (handListener.isClickedLeft_m () & !leapListener.leftHandDetected ()) {
			//spawn a ball or something like this
//			leapListener.attachToLeftHand(objToSpawn);
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
