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

		leftHandPalm = GameObject.FindGameObjectWithTag("LeftHand");

		if (frame.Hands.Count > 0) {
			
		
			handsDetected = true;

			List<Hand> hands = frame.Hands;

			for (int i = 0; i < hands.Count; i++) {
				if (hands [i].IsLeft) {
					leftDetected = true;

//					if (leftHandAttachment != null & leftHandPalm != null) {
//						//attach to hand and then delete variable
//						//Instantiate(leftHandAttachment, new Vector3(0,0,0) , Quaternion.identity);
//
//
//
////						Debug.Log ("left palm iss null " + (leftHandPalm == null));
////						GameObject tmpobj = Instantiate(leftHandAttachment, new Vector3(0,0,0) , Quaternion.identity);
////
////						tmpobj.transform.SetParent (leftHandPalm.transform);
//////						Instantiate(leftHandAttachment, new Vector3(0,0,0) , Quaternion.identity, leftHandPalm.transform);
////
////						leftHandAttachment = null;
//					}

				} else {
					rightDetected = true;

//					if (rightHandAttachment != null) {
//						//attach to hand and then delete variable
////						Instantiate(rightHandAttachment, hands [i].PalmPosition  , Quaternion.identity);
//						rightHandAttachment = null;
//					}
				}
			}


		} 
//		else {
//			handsDetected = false;
//			leftDetected = false;
//			rightDetected = false;
//		}
//



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

		if (leftHandPalm != null & !anchor.hasAnchoredObjects) {
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
}
