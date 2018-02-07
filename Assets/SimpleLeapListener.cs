using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity.Interaction;

public class SimpleLeapListener : MonoBehaviour {

	private Controller controller;

	private bool handsDetected = false;
	private bool leftDetected = false;
	private bool rightDetected = false;
	private bool leftEnter = false;
	private bool rightEnter = false;



	private GameObject leftHandAttachment;
	private GameObject rightHandAttachment;

	private GameObject leftHandPalm; 
	private GameObject rightHandPalm;


	// Use this for initialization
	void Start () {

		controller = new Controller ();
		controller.Frame ();

		leftHandPalm = GameObject.FindGameObjectWithTag("LeftHand");
		rightHandPalm = GameObject.FindGameObjectWithTag("RightHand");
	}
	
	// Update is called once per frame
	void Update () {
		handsDetected = false;
		leftDetected = false;
		rightDetected = false;


		Frame frame = controller.Frame ();
		Frame frameLast = controller.Frame (1);




		leftHandPalm = GameObject.FindGameObjectWithTag("LeftHand");

		//array with boolean for left hand detected (entry 1) and right hand detected (entry 2) at actual frame
		bool[] leftRight = frameHandDetection (frame);

		leftDetected = leftRight [0];
		rightDetected  = leftRight [1];
		//array with boolean for left hand detected (entry 1) and right hand detected (entry 2) at LAST frame
		bool[] leftRightOld = frameHandDetection (frameLast);
		if (leftRightOld [0] == false & leftDetected == true) {
			//Debug.Log ("leftEnter SET");
			leftEnter = true;
		}
		else if(leftRightOld[0] == true & leftDetected == true){
			leftEnter=false;
		}
		else if(leftRightOld[0] == true & leftDetected==false){
			//hand leaves the Leap-FOV (put obj onto stack
			leftEnter=false;
		}


		if (leftRightOld [1] == false & rightDetected == true) {
			rightEnter = true;
		}
		else if(leftRightOld[1] == true & rightDetected == true){
			rightEnter=false;
		}
		else if(leftRightOld[1] == true & rightDetected==false){
			//hand leaves the Leap-FOV (put obj onto stack
			rightEnter=false;
		}


	}
	private bool[] frameHandDetection(Frame frame){
		bool[] detectedHands = new bool[2];
		if (frame.Hands.Count > 0) {


			handsDetected = true;

			List<Hand> hands = frame.Hands;

			for (int i = 0; i < hands.Count; i++) {
				if (hands [i].IsLeft) {
//					leftDetected = true;
					detectedHands [0] = true;


	

				} else {
//					rightDetected = true;
					detectedHands [1] = true;

				}
			}


		}

		return detectedHands;
	}

	public bool leftHandDetected(){
		return leftDetected;
	}

	public bool rightHandDetected(){
		return rightDetected;
	}

	public bool anyHandDetected(){
		return handsDetected;
	}

	public void attachToLeftHand(Anchor anchor, AnchorGroup agroup, GameObject obj){
	
		leftHandPalm = GameObject.FindGameObjectWithTag("LeftHand");
		Debug.Log ("attachToLeftHand entered");
		//if (leftHandPalm != null & !anchor.hasAnchoredObjects & leftEnter) {
		if (leftHandPalm != null & !anchor.hasAnchoredObjects ) {	
			Debug.Log ("left hand anchor has no objects");

			GameObject tmpobj = Instantiate (obj, new Vector3 (0, 0, 0), Quaternion.identity);

			//tmpobj.transform.SetParent (leftHandPalm.transform);


			AnchorableBehaviour ascript = tmpobj.GetComponent<AnchorableBehaviour> ();

			ascript.anchor = anchor;
			ascript.anchorGroup = agroup;

			ascript.TryAttach (true);

//			leftHandAttachment = obj;
		}
	
	}
	public void attachToRightHand(GameObject obj){


		rightHandAttachment = obj;

	}

	public bool getLeftHandEnter(){
		return leftEnter;
	}

	public bool getRightHandEnter(){
		return rightEnter;
	}

}
