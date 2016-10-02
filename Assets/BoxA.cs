using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoxA : MonoBehaviour {
	public int posX;
	public int posY;
	Text id;

	public void SetPosXY(int _x,int _y){
		posX = _x;
		posY = _y;
	}
	// Use this for initialization
	void Start () {
    	
	
	}
	public void LoadChildrens(){
    	id = this.GetComponentInChildren<Text>(true);
    	// pin = this.GetComponentInChildren<Image>(true);
	}
	public void SetTextState(string _id){
		if(_id =="#"){
			id.color = Color.red;
		} else{
			id.color = Color.black;
		}
		id.text =_id+"";
	}
	

}
