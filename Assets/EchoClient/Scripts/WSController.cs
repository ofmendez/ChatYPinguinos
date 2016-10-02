using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WebSocketSharp;
using System;

public class WSController : MonoBehaviour {
	WebSocket ws;
	Action<string> mydelegate;
	string pathGetRoom = "/chat/resources/room";
	string pathWSgameRoom = "/chat/gameroom?roomId=";
	string protocolHTTP = "http://";
	string protocolWS = "ws://";
	int actualRoomId =0;
	string history = "";
	public Text mHistory;


	void Start(){
		// AppendMessage("listo");
		mydelegate = new Action<string>(delegate(string param) {
			AppendMessage(param); 
		});
	}
        // UnityEngine.WSA.Application.InvokeOnUIThread( ()=> {
    		// mHistory.text += "\n Sala creada!";
    		// 

	IEnumerator ConnectRoutine(string _url){
		WWW wwwRoom = new WWW (protocolHTTP + _url + pathGetRoom);
		yield return wwwRoom;
		if (string.IsNullOrEmpty(wwwRoom.error))  {
			string postColon = wwwRoom.text.Split(':')[1];
			actualRoomId = int.Parse( postColon.Remove(postColon.Length - 1) );
			print("obtenemos: "+ actualRoomId);
			AppendMessage("obtenemos: "+ actualRoomId);
		}else{
			print("ERROR no se ha conectado a la informacion de la sala.");
		}
		ws = new WebSocket(protocolWS + _url + pathWSgameRoom + actualRoomId);
	 		
		// ){

	    ws.OnOpen += ( sender, e) => {
	        print ("Connected");
			AppendMessage("Connected"); 
	    };

		ws.OnMessage = (System.Object sender, MessageEventArgs e) => {
    		print ("<-# " + e.Data);
			AppendMessage(e.Data+" : "+sender); 
		};
		// ws.Send ("perro");

	    ws.OnClose = ( sender, e) => {
	        print ("Closed " + e.Code +" "+ e.Reason +" "+ e.WasClean);
			AppendMessage("Closed " +  e.Code + e.Reason + e.WasClean); 
	    };

	    ws.OnError += ( sender,  e) => {
	        print ("ERROR: " + e.Message);
			AppendMessage("ERROR: " + e.Message); 
	    };
		// AppendMessage("Connected"); 
		// AppendMessage(""); 

		ws.Connect ();
	}

	public void Connect(InputField fldIP){  
		StartCoroutine( ConnectRoutine(fldIP.text) );
 	}
	    		

 	void Update(){
		// mHistory.text = history;
		if(! string.IsNullOrEmpty(history) ){
	   		Text mNewtext = Instantiate (mHistory.gameObject).GetComponent<Text>();
	        mNewtext.transform.SetParent(mHistory.transform.parent, false);
			mNewtext.text = history;
			history ="";
		}
 	}

   	void AppendMessage(string msg){
		// mHistory.text += "\n"+msg;
		history = msg;
    }

	

	public void SendMessage (InputField fld ) {
    	print("enviando "+fld.text);
    	if(ws == null){
    		AppendMessage("Please connect to server.");
    		return;
    	}
		ws.Send (fld.text);
		// AppendMessage("yo: "+fld.text);
		fld.text = "";
	}
}
