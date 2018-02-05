using UnityEngine;
using Windows.Kinect;
using System.Collections;
using System;


public class SimpleHandListener : MonoBehaviour {

	private KinectSensor _Sensor;
	private BodyFrameReader _Reader;
	private Body[] _Data = null;

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

					if (_Data[idx].HandRightState == HandState.Open)
					{
						progressGestureTime = Time.realtimeSinceStartup;
						//						float angley =
						//							(float)(_Data[idx].Joints[JointType.HandLeft].Position.X);
						//						float anglex =
						//							(float)(_Data[idx].Joints[JointType.HandLeft].Position.Y);
						//						float anglez =
						//							(float)(_Data[idx].Joints[JointType.HandLeft].Position.Z);
						//
						//						this.gameObject.transform.rotation =
						//							Quaternion.Euler(
						//								this.gameObject.transform.rotation.x + anglex * 100,
						//								this.gameObject.transform.rotation.y + angley * 100,
						//								this.gameObject.transform.rotation.z + anglez * 100);

						gestureInfoRight.text = "Right Hand open";
						if (Time.realtimeSinceStartup - progressGestureTime > 2.0f) 
						{
							gestureInfoRight.text = string.Empty;
						}
					}

					else if (_Data[idx].HandRightState == HandState.Closed)
					{
						progressGestureTime = Time.realtimeSinceStartup;
						gestureInfoRight.text = "Right Hand closed";
					}

					else if (_Data[idx].HandRightState == HandState.Unknown)
					{
						progressGestureTime = Time.realtimeSinceStartup;
						gestureInfoRight.text = "Right Hand unknown";

						if (Time.realtimeSinceStartup - progressGestureTime > 2.0f) 
						{
							gestureInfoRight.text = string.Empty;
						}

					}



					if (_Data[idx].HandLeftState == HandState.Open)
					{
						progressGestureTime = Time.realtimeSinceStartup;
//						float angley =
//							(float)(_Data[idx].Joints[JointType.HandLeft].Position.X);
//						float anglex =
//							(float)(_Data[idx].Joints[JointType.HandLeft].Position.Y);
//						float anglez =
//							(float)(_Data[idx].Joints[JointType.HandLeft].Position.Z);
//
//						this.gameObject.transform.rotation =
//							Quaternion.Euler(
//								this.gameObject.transform.rotation.x + anglex * 100,
//								this.gameObject.transform.rotation.y + angley * 100,
//								this.gameObject.transform.rotation.z + anglez * 100);

						gestureInfo.text = "Left Hand open";
						if (Time.realtimeSinceStartup - progressGestureTime > 2.0f) 
						{
							gestureInfo.text = string.Empty;
						}
					}

					else if (_Data[idx].HandLeftState == HandState.Closed)
					{
						progressGestureTime = Time.realtimeSinceStartup;
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

					}
				}
			}
		}
	}
}