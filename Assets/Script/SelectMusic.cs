using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		

		// if (Application.internetReachability == NetworkReachability.NotReachable){
			
		// 	//인터넷이 연결되어 있지 않을 경우 다음과 같이 실행하라\
		// 	label.text = "Not connect Internet";
		// 	// Debug.Log("Not connect Internet");
		// }else {

		// 	label.text = "Success connect Internet";
		// 	StartCoroutine(startScene() );
		// 	// Debug.Log("Success connect Internet");
			
		// 	//https://youtu.be/I4JX4CzfI6E
		// }
	}

	IEnumerator startScene(){
		yield return new WaitForSeconds(3.0f);
		SceneManager.LoadScene("Scene/main");
	}

	public void musicgame(){
		SceneManager.LoadScene("Scene/main");
	}

	public void gyrogame(){
		SceneManager.LoadScene("Scene/gyroGame");
	}
	
	
}
