using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap.Unity.Interaction;

public class leapHandVisibility : MonoBehaviour {

	private LeapHandController leapmgr;

	private SimpleLeapListener leapListener;
	private bool active=false;

	// Use this for initialization
	void Start () {
		

		GameObject obj = GameObject.FindGameObjectWithTag ("LeapHandController");
		leapmgr = obj.GetComponent<LeapHandController> ();
		leapListener = leapmgr.GetComponent<SimpleLeapListener> ();


		
	}
	
	// Update is called once per frame
	void Update () {

		//wenn hand nicht visible
		//dann mache GameObject inactiv


		AnchorableBehaviour abehave = this.gameObject.GetComponent<AnchorableBehaviour> ();



		bool attached = abehave.isAttached;
		if (attached & !leapListener.leftHandDetected ()) {
			Debug.Log ("leapHandvisibility -> is attached to anchor and no leaphand detected");

//			this.gameObject.SetActive (false);


			setVisibility (false);



			active = false;
		} else if(! active) {
//			this.gameObject.SetActive (true);

			setVisibility (true);

			active = true;
		} 
		
	}
	private void setVisibility(bool status){
	
//		renderer[] = this.gameObject.GetComponentsInChildren<Renderer>();

		foreach(Renderer elem in this.gameObject.GetComponentsInChildren<Renderer>()){
			elem.enabled = status;
		}
	
	}


}
