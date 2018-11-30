using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;


public class SocketManager2 : MonoBehaviour
{
	private SocketIOComponent socket;

	private string mUserid;

	private JSONObject mJsonObj;

	private bool check = false;

	public void Start() 
	{

		socket = GameObject.Find ("SocketIO").GetComponent<SocketIOComponent> ();
		socket.On ("open", OnOpen);
		socket.On ("drawing", OnDrawing);
		socket.On ("error", OnError);
		socket.On ("close", OnClose);
		
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
				
					
				
				check = false;
			}
		}

	}

	public void OnOpen(SocketIOEvent e)
	{
		
		Debug.Log("[SocketIO] Open(): " + e.data);

		socket.Emit("joinRoom", JSONObject.StringObject("gyro"));

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


	public void SendJson(float x, float y,  float z){
		// Debug.Log("idxList = " + idxlist[0]);
		//JSONObject jo = new JSONObject();
		JsonFloat jd = new JsonFloat();
		jd.x = x;
		jd.y = y;
		jd.z = z;
		jd.status = "update";
		JSONObject jo = new JSONObject(JsonUtility.ToJson(jd));
		// Debug.Log("id = " + id );
		// jo.AddField("idx", id );
		// Debug.Log(jo);
		
		socket.Emit("send", jo);
	}

	public void Sendinit(int x, int y,  int z){
		// Debug.Log("idxList = " + idxlist[0]);
		//JSONObject jo = new JSONObject();
		JsonInt ji = new JsonInt();
		ji.init_x = x;
		ji.init_y = y;
		ji.init_z = z;
		JSONObject jo = new JSONObject(JsonUtility.ToJson(ji));
		// Debug.Log("id = " + id );
		// jo.AddField("idx", id );
		// Debug.Log(jo);
		
		socket.Emit("send", jo);
	} 

	public void SendPause(){
		// Debug.Log("idxList = " + idxlist[0]);
		//JSONObject jo = new JSONObject();
		JsonFloat jd = new JsonFloat();
		jd.status = "pause";
		JSONObject jo = new JSONObject(JsonUtility.ToJson(jd));
		// Debug.Log("id = " + id );
		// jo.AddField("idx", id );
		// Debug.Log(jo);
		
		socket.Emit("send", jo);
	}

	public void SendStop(){
		// Debug.Log("idxList = " + idxlist[0]);
		//JSONObject jo = new JSONObject();
		JsonFloat jd = new JsonFloat();
		jd.status = "stop";
		JSONObject jo = new JSONObject(JsonUtility.ToJson(jd));
		// Debug.Log("id = " + id );
		// jo.AddField("idx", id );
		// Debug.Log(jo);
		
		socket.Emit("send", jo);
	}


	public void clickButton(){

		JsonFloat jd = new JsonFloat();
		jd.status = "shoot";
		JSONObject jo = new JSONObject(JsonUtility.ToJson(jd));
		// Debug.Log("id = " + id );
		// jo.AddField("idx", id );
		// Debug.Log(jo);
		
		socket.Emit("send", jo);
	}

	

}

[System.Serializable]
public class JsonInt {
	
	public int init_x;
	public int init_y;
	public int init_z;
}

[System.Serializable]
public class JsonFloat {
	
	public string status;
	public float x;
	public float y;
	public float z;
}



