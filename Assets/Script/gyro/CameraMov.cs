using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour {


	private int initialOrientationX; 
    private int initialOrientationY; 
    private int initialOrientationZ; 

	public SocketManager2 socketManager2;
    

    private bool check = true;

    void Start() 
    { 
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Input.gyro.enabled = true; 
        Input.gyro.updateInterval = 0.01f; 

        initialOrientationX = (int)Input.gyro.rotationRateUnbiased.x; 
        initialOrientationY = (int)Input.gyro.rotationRateUnbiased.y; 
        initialOrientationZ = (int)-Input.gyro.rotationRateUnbiased.z; 

		socketManager2.Sendinit(initialOrientationX,initialOrientationY,initialOrientationZ);
    } 


    void Update() 
    { 
        if(check) {

            transform.Rotate(initialOrientationX - Input.gyro.rotationRateUnbiased.x, 
                        initialOrientationY - Input.gyro.rotationRateUnbiased.y, 
                        initialOrientationZ + Input.gyro.rotationRateUnbiased.z);
						 
		    socketManager2.SendJson(initialOrientationX - Input.gyro.rotationRateUnbiased.x,
		 					initialOrientationY - Input.gyro.rotationRateUnbiased.y, 
                        	initialOrientationZ + Input.gyro.rotationRateUnbiased.z);

        }
        

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home))
            {
                //home button
            }
            else if(Input.GetKey(KeyCode.Escape))
            {
                //back button
            }
            else if(Input.GetKey(KeyCode.Menu))
            {
                //menu button
            }
        }  

       

    }

   
    void OnApplicationPause(bool pauseStatus)
    {

        if(pauseStatus) {

            check = false;
            socketManager2.SendPause();

        }else {

            check = true;

        }

    }
    void OnApplicationQuit()
    {
        check = false;
        socketManager2.SendStop();
        
    }




}
