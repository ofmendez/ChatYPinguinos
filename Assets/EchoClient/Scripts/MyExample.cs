using System;
using UnityEngine.UI;
using UnityEngine;
using WebSocketSharp;

  public class MyExample :MonoBehaviour {


    void Start(){  

    WebSocket ws = new WebSocket("ws://192.168.0.55:8080/chat/echo");

    ws.OnOpen = (System.Object sender, EventArgs e) => {
        print ("Connected");
        print("-> ");
    };

    ws.OnMessage = (System.Object sender, MessageEventArgs e) => {
        print ("<- " + e.Data);
        print("-> ");
    };

    ws.OnError = (System.Object sender, ErrorEventArgs e) => {
        print ("ERROR: " + e.Message);
        print("-> ");
    };

    ws.OnClose = (System.Object sender, CloseEventArgs e) => {
        print ("Closed " + e.Code + e.Reason + e.WasClean);
    };
 
      ws.Connect (); 
      ws.Send ("HOLA FABIAAAAAAAN!!! ");





    } 

  }
