using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchScreen : MonoBehaviour {

	public Image[] imageArr;
	private SocketManager socketManager;
	public List<int> idxList = new List<int>();
	private bool check = false;
	// Use this for initialization

	void Start () {

		socketManager = GameObject.Find("SocketManager").GetComponent<SocketManager>();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(check){

			socketManager.SendJson(idxList);
		}

	}

	public void exitPoint(int idx){
		// Debug.Log("exit Point");
		check = false;
		imageArr[idx].color = new Color(1,1,1,1);
			// foreach(int i in idxList){
			// 	if(i == idx){
			// 		// idxList.RemoveAt(i);
			// 		idxList.Remove(i);
			// 	}
			// }
		if(idxList.Count == 0){
			return;
		}
			for(int i=0; i<idxList.Count; i++){

				if(idxList[i] == idx){

					idxList.RemoveAt(i);

				}
			}

		}
	

	public void touchImage(int i){

		check = true;
		idxList.Add(i);
		// Debug.Log("point enter ");
		imageArr[i].color = setBlackColor();

	}

	public Color32 setBlackColor(){

		int r = Random.Range(0,256);
		int g = Random.Range(0,256);
		int b = Random.Range(0,256);
		Color32 color = new Color32( (byte)(r * 255), (byte)(g * 255), (byte)(b * 255), 255 );

		return color;

	}

}
