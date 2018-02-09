using UnityEngine;
using Windows.Kinect;
using System.Collections;
using System.Collections.Generic;
using System;

public class SimpleHandListener : MonoBehaviour {

	private KinectSensor _Sensor;
	private BodyFrameReader _Reader;
	private Body[] _Data = null;
	private Boolean isClickedLeft = false;
	private Boolean isClickedRight= false;
	public KinectManager kinectmgr;


	public UnityEngine.UI.Text gestureInfo;
	public UnityEngine.UI.Text gestureInfoRight;


	// private bool to track if progress message has been displayed
	private bool progressDisplayed;
	private float progressGestureTime;

	// Use this for initialization
	void Start () {

		_Sensor = KinectSensor.GetDefault();

		if (_Sensor != null)
		{
			_Reader = _Sensor.BodyFrameSource.OpenReader();

			if (!_Sensor.IsOpen)
			{
				_Sensor.Open();
			}
		}


	}

	void OnApplicationQuit()
	{
		if (_Reader != null)
		{
			_Reader.Dispose();
			_Reader = null;
		}

		if (_Sensor != null)
		{
			if (_Sensor.IsOpen)
			{
				_Sensor.Close();
			}
			_Sensor = null;
		}
	}

	// Update is called once per frame
	void Update () {

		if (_Reader != null)
		{
			var frame = _Reader.AcquireLatestFrame();

			if (frame != null)
			{
				if (_Data == null)
				{
					_Data = new Body[_Sensor.BodyFrameSource.BodyCount];
				}

				frame.GetAndRefreshBodyData(_Data);

				frame.Dispose();
				frame = null;

				int idx = -1;
				for (int i = 0; i < _Sensor.BodyFrameSource.BodyCount; i++) 
				{
					if (_Data [i].IsTracked) 
					{
						idx = i;
					}
				}

				if (idx > -1)
				{
					//Vector3 relPos = GetRelativeJoint (4, 6);
					//Debug.Log ("x:" + relPos.x + "y:" + relPos.y + "z:" + relPos.z);
					int quad = getLeftHandQuadrant(4);


					if (_Data[idx].HandRightState == HandState.Open)
					{
						progressGestureTime = Time.realtimeSinceStartup;


						gestureInfoRight.text = "Right Hand open";
						if (Time.realtimeSinceStartup - progressGestureTime > 2.0f) 
						{
							gestureInfoRight.text = string.Empty;
						}
						isClickedRight = false;
					}

					else if (_Data[idx].HandRightState == HandState.Closed)
					{
						progressGestureTime = Time.realtimeSinceStartup;
						gestureInfoRight.text = "Right Hand closed";
						isClickedLeft = true;
					}

					else if (_Data[idx].HandRightState == HandState.Unknown)
					{
						progressGestureTime = Time.realtimeSinceStartup;
						gestureInfoRight.text = "Right Hand unknown";

						if (Time.realtimeSinceStartup - progressGestureTime > 2.0f) 
						{
							gestureInfoRight.text = string.Empty;
						}
						isClickedRight = false;

					}



					if (_Data[idx].HandLeftState == HandState.Open)
					{
						progressGestureTime = Time.realtimeSinceStartup;


						gestureInfo.text = "Left Hand open";
						if (Time.realtimeSinceStartup - progressGestureTime > 2.0f) 
						{
							gestureInfo.text = string.Empty;
						}
						isClickedLeft = false;
					}

					else if (_Data[idx].HandLeftState == HandState.Closed)
					{
						progressGestureTime = Time.realtimeSinceStartup;
						isClickedLeft = true;
						gestureInfo.text = "Left Hand closed";
					}

					else if (_Data[idx].HandLeftState == HandState.Unknown)
					{
						progressGestureTime = Time.realtimeSinceStartup;
						gestureInfo.text = "Left Hand unknown";

						if (Time.realtimeSinceStartup - progressGestureTime > 2.0f) 
						{
							gestureInfo.text = string.Empty;
						}
						isClickedLeft = false;

					}
				}
			}
		}
	}


	public Boolean isClickedLeft_m()
	{
		return isClickedLeft;
	}

	public Boolean isClickedRight_m()
	{
		return isClickedRight;
	}
	public int getLeftHandQuadrant(int quadCtr){
		//JointID Spine_shoulder = 20
		//JointID WristLeft = 6

		Vector3 relPosLeft = GetRelativeJoint (20, 6);

		float x = relPosLeft.x;
		float y = relPosLeft.y;

		double degree = System.Math.Atan2 (x, y) * 180 / System.Math.PI;
		if (degree < 0) {
			degree = 360 + degree;
		}

		//Debug.Log ("degree:" + degree);

		double degPerQuad = 360 / (double) quadCtr;
		double degPhase = degPerQuad / 2;

		int quadrant = (int) ( ((degree + degPhase) % 360) / degPerQuad) + 1;

		//Debug.Log ("Quadrant:" + quadrant);

		return 0;
	}

	public int getRightHandQuadrant(){
		return 0;
	}

	private Vector3 GetRelativeJoint(int jointOrigin, int jointTarget){
	 


		if (kinectmgr != null) {
			Debug.Log ("KinectMgr is null");
		

			List<long> UserID = kinectmgr.GetAllUserIds ();

			if (UserID.Count != 0) {
			

				Vector3 jointOriginVec = kinectmgr.GetJointKinectPosition (UserID [0], jointOrigin);
				Vector3 jointTargetVec = kinectmgr.GetJointKinectPosition (UserID [0], jointTarget);

				return (jointTargetVec - jointOriginVec);


			} 
		}

			return new Vector3 (0, 0, 0);

		


	}

}