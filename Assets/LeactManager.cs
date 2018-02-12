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
	public GameObject leftPalm;

	private MenuFabric fabrik;
	private Stack<int> menuHistory;

	private bool leftHandClickTrigger = true;

	// Use this for initialization
	void Start () {

		menuHistory = new Stack<int> ();
		menuHistory.Push (0);
		handListener = kinectmgr.GetComponent<SimpleHandListener> ();
		leapListener = leapmgr.GetComponent<SimpleLeapListener> ();

		fabrik = this.gameObject.GetComponent<MenuFabric>();

		rootMenu = fabrik.getRootMenu (canvas.transform);


	}
	
	// Update is called once per frame
	void Update () {

		gestureInfoRightScreen.text = getOnScreenText ();
		//Debug.Log ("LeftHandQuad:" + handListener.getLeftHandQuadrant (4));


		//spawn something if the kinect detects a click but leap no hand

		int quad = handListener.getLeftHandQuadrant(4);
		if (quad > 0) {


			rootMenu.setKinectAngle (handListener.getLeftHandAngle() );

		}

	

		if (handListener.isClickedLeft_m () & !leapListener.leftHandDetected () & leftHandClickTrigger & rootMenu != null) {
//		if (handListener.isClickedLeft_m () & leapListener.getLeftHandEnter()){
			//spawn a ball or something like this
//			leapListener.attachToLeftHand(anchor,agroup,objToSpawn);
//			rootMenu = fabrik.getRootMenu (canvas.transform);
			accessmenu(rootMenu.getPrevActiveIndex());
//			rootMenu = fabrik.next(rootMenu.id, rootMenu.getPrevActiveIndex(),canvas.transform);
			leftHandClickTrigger = false;
		} else if (handListener.isClickedLeft_m () & !leapListener.leftHandDetected () & leftHandClickTrigger & rootMenu == null) {
			//if the menu was destroyed ( to hide it) just create a new one
			rootMenu= fabrik.menu(menuHistory.Peek(),canvas.transform);
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


	
		else if (!handListener.isClickedLeft_m () & !leapListener.leftHandDetected () & !leftHandClickTrigger) {
			leftHandClickTrigger = true;
		
		}


		
	}
	private void accessmenu(int index){
		

			switch (index) {
			case 1:
				
			if (menuHistory.Count == 0) {

				rootMenu = fabrik.defaultMenu (canvas.transform);
				menuHistory.Push (rootMenu.id);

				} else {
			
					int tmp = menuHistory.Pop ();

				rootMenu = fabrik.next(rootMenu.id, tmp ,canvas.transform);
				}


				break;
		default:
			menuHistory.Push (rootMenu.id);
			rootMenu = fabrik.next(rootMenu.id, index,canvas.transform);
				
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
