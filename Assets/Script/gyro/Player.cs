using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private float x;
	private float y;
	private float z;
	public SocketManager2 socketManager2;

	void Awake()
	{
		Input.gyro.enabled = true;
	}
	// Use this for initialization
	void Start () {
		
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	// Update is called once per frame
	void Update () {
		
		// x = Input.acceleration.x * 5;
		// y = Input.acceleration.y * 5;
		// z = Input.acceleration.z * 5;

		x = Input.gyro.rotationRateUnbiased.x;
		y = Input.gyro.rotationRateUnbiased.y;
		z = Input.gyro.rotationRateUnbiased.z;
		socketManager2.SendJson(x,y,z); 
	}
}
