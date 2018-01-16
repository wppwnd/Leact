using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positioningOnRig : MonoBehaviour {
	public GameObject leftHandRigBody;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = leftHandRigBody.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.position = leftHandRigBody.transform.position;
		
	}
}
