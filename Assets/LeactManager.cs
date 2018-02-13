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
	public RMF_RadialMenu rootMenu;
	//parent canvas where the menu needs to be
	public GameObject canvas;
	public Anchor anchor;
	public AnchorGroup agroup;
//	public GameObject leftPalm;

	private MenuFabric fabrik;

	private float cooldownTime = 0.5f;
	private float cooldownStart;


	private bool leftHandClickTrigger = true;

	// Use this for initialization
	void Start () {

		handListener = kinectmgr.GetComponent<SimpleHandListener> ();
		leapListener = leapmgr.GetComponent<SimpleLeapListener> ();

		fabrik = this.gameObject.GetComponent<MenuFabric>();

//		rootMenu = fabrik.getRootMenu (canvas.transform);


	}
	
	// Update is called once per frame
	void Update () {

		gestureInfoRightScreen.text = getOnScreenText ();
		//Debug.Log ("LeftHandQuad:" + handListener.getLeftHandQuadrant (4));


		//spawn something if the kinect detects a click but leap no hand

		int quad = handListener.getLeftHandQuadrant(4);
		if (quad > 0) {


			if (rootMenu != null) {
				rootMenu.setKinectAngle (handListener.getLeftHandAngle ());
			}

		}

	

		if (handListener.isClickedLeft_m () & !leapListener.leftHandDetected () & leftHandClickTrigger & rootMenu != null) {
//		if (handListener.isClickedLeft_m () & leapListener.getLeftHandEnter()){
			//spawn a ball or something like this
//			leapListener.attachToLeftHand(anchor,agroup,objToSpawn);
//			rootMenu = fabrik.getRootMenu (canvas.transform);
			accessmenu (rootMenu.getPrevActiveIndex ());

//			rootMenu = fabrik.next(rootMenu.id, rootMenu.getPrevActiveIndex(),canvas.transform);
			leftHandClickTrigger = false;
			cooldownStart = Time.realtimeSinceStartup;
		} else if (handListener.isClickedLeft_m () & (!leapListener.leftHandDetected() || !leapListener.rightHandDetected()) & leftHandClickTrigger & rootMenu == null) {
			//if the menu was destroyed ( to hide it) just create a new one
			rootMenu = fabrik.showMenu(canvas.transform);
			leftHandClickTrigger = false;
			cooldownStart = Time.realtimeSinceStartup;
		}


//		else if(handListener.isClickedLeft_m () & !leapListener.leftHandDetected () & (rootMenu.getPrevActiveIndex()==2) & leftHandClickTrigger){
//			accessmenu (2);
//			leftHandClickTrigger = false;
//		} else if(handListener.isClickedLeft_m () & !leapListener.leftHandDetected () & (rootMenu.getPrevActiveIndex()==3) & leftHandClickTrigger){
//			accessmenu (3);
//			leftHandClickTrigger = false;
//		} else if(handListener.isClickedLeft_m () & !leapListener.leftHandDetected () & (rootMenu.getPrevActiveIndex()==4) & leftHandClickTrigger){
//			accessmenu (4);
//			leftHandClickTrigger = false;
//		} 

		else if (!handListener.isClickedLeft_m () & !leapListener.leftHandDetected () & !leftHandClickTrigger & ((Time.realtimeSinceStartup - cooldownStart) >= cooldownTime)) {
			leftHandClickTrigger = true;

		
		}

		/*menu selected and hand gets detected by leap (NO closed hand)*/
		else if (!handListener.isClickedLeft_m () &  (leapListener.leftHandDetected() || leapListener.rightHandDetected())& rootMenu != null) {
			
			fabrik.destroyPrev ();
		} 

		/*menu selected and hand gets detected by leap (WITH closed hands)*/
		else if (handListener.isClickedLeft_m () & leapListener.getLeftHandEnter () & rootMenu != null & objToSpawn != null) {
			//instantiate?
			//leapListener.attachToLeftHand(anchor,agroup,objToSpawn);
		}
			
	}


	private void accessmenu(int index){
		
			switch (index) {
		case 1:
			
				rootMenu = fabrik.prevMenu (canvas.transform);


				break;
		default:

			fabrik.highlightKlick (index);
			
			int nextMenu = fabrik.next (rootMenu.id, index);
			if (!fabrik.isNullInteger (nextMenu)) {

				if (fabrik.isMenu (nextMenu)) {
			
					rootMenu = fabrik.menu (nextMenu, canvas.transform);

			
				} else {
					Debug.Log ("accessmenu() -> Element is a object and not a menu");
					objToSpawn = fabrik.getObject (nextMenu);
					if (objToSpawn != null) {
						leapListener.attachToLeftHand (anchor, agroup, objToSpawn);
					}
				}	

			}
			break;
		}
	}
//		Debug.Log ("stacksize: " + menuHistory.Count);
	


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
