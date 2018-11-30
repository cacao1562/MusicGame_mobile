using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;


public class SocketManager : MonoBehaviour
{
	private SocketIOComponent socket;

	private string mUserid;

	private JSONObject mJsonObj;

	private bool check = false;

	public AudioClip[] mAudioClip = new AudioClip[7];
	private AudioSource mAudioSource;
	public Text scoreText;
	public Slider slider;
	public Button dragonButton;
	public GameObject particleObj;
	public ParticleSystem ps;

	public void Start() 
	{

		socket = GameObject.Find ("SocketIO").GetComponent<SocketIOComponent> ();
		socket.On ("open", OnOpen);
		socket.On ("drawing", OnDrawing);
		socket.On ("error", OnError);
		socket.On ("close", OnClose);
		mAudioSource = GetComponent<AudioSource>();
		// mAudioSource.clip = mAudioClip[0];
	}

	public void Update(){

		if(!socket.IsConnected) {

			Debug.Log("소켓 연결 안됨");
			return;
			
		}

		if(mJsonObj == null) {

			return;
		}

		if(check) {

			JsonDataStr jstr = JsonUtility.FromJson<JsonDataStr>(mJsonObj.ToString() );
			if (jstr.sendStr == "catch") {
				
				scoreText.text = jstr.score.ToString();
				slider.value = jstr.score;
				if(jstr.score == 10) {

					dragonButton.interactable = true;
					particleObj.SetActive(true);
					ps.Play();
				}
				mAudioSource.PlayOneShot(mAudioClip[jstr.spNum - 1]);
				// if(mAudioSource.isPlaying){

				// }else{
				// 	mAudioSource.PlayOneShot(mAudioClip);
				// }
				Handheld.Vibrate();
				// iPhoneUtils.Vibrate();
				
				
				
				check = false;
			}
		}

	}

	public void OnOpen(SocketIOEvent e)
	{
		
		Debug.Log("[SocketIO] Open(): " + e.data);

		socket.Emit("joinRoom", JSONObject.StringObject("music"));

	}
	
	public void OnDrawing(SocketIOEvent e)
	{
		check = true;
		mJsonObj = e.data;
		// Debug.Log("[SocketIO] OnDrawing(): " + e.data);

	}
	
	public void OnError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Error(): " + e.data);
	}
	
	public void OnClose(SocketIOEvent e)
	{	
		Debug.Log("[SocketIO] Close(): " + e.data);
	}

	public void SendJsonDrawing(JSONObject jo){

		jo.AddField("userid", "music start");
		socket.Emit("send", jo);

	}

	public void SendJson(List<int> idxlist){
		// Debug.Log("idxList = " + idxlist[0]);
		//JSONObject jo = new JSONObject();
		JsonData jd = new JsonData();
		jd.idxList = idxlist;
		JSONObject jo = new JSONObject(JsonUtility.ToJson(jd));
		// Debug.Log("id = " + id );
		// jo.AddField("idx", id );
		// Debug.Log(jo);
		socket.Emit("send", jo);
	} 

	public void SendDragon(){

		JsonDataStr js = new JsonDataStr();
		js.sendStr = "dragon";
		// js.score = 0;
		JSONObject jo = new JSONObject(JsonUtility.ToJson(js) );
		socket.Emit("send", jo);
		dragonButton.interactable = false;
		slider.value = 0;
		particleObj.SetActive(false);
		ps.Stop();

	}

}
[System.Serializable]
public class JsonData{
	public List<int> idxList;
}

[System.Serializable]
public class JsonDataStr{
	public string sendStr;
	public int score;
	public int spNum;
}

