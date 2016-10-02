using UnityEngine;
using System.Collections;

public class ControlGame : MonoBehaviour {
	public BoxA[] boxes;
	public int columns_Y ;
	public int rows_X ;
	public Vector2 posG;
	public Vector2 posM;
	int moves;

	string[,] array2D = new string[,]{
		{".",".",".",".",".",".",".","L",".",".",".",".",".",".","."},
		{".","#","#","#",".","#","#","#",".","#","#","#",".","#","#"},
		{"#","#",".","#",".","#",".","#","#","#",".","#",".","#","."},
		{".",".",".",".",".",".",".","#",".",".",".",".",".",".","."},
		{".","#","#","#","#","#",".","#",".","#","#","#","#","#","."},
		{".",".",".",".",".",".",".","#",".",".",".",".",".",".","."},
		{"#","#",".","#",".","#",".","#",".","#",".","#",".","#","#"},
		{".",".",".",".",".",".",".","#",".",".",".",".",".",".","."},
		{".","#","#","#","#","#","#","#","#","#","#","#","#","#","."},
		{".",".",".",".",".",".","G","#","M",".",".",".",".",".","."}
	};
	// Use this for initialization
	void Start () {
	    boxes = this.GetComponentsInChildren<BoxA>(true);
		for(int i=0; i<boxes.Length; i++){
			boxes[i].SetPosXY(GetIndexX(i),GetIndexY(i));
			boxes[i].LoadChildrens();
			// print("es x: "+boxes[i].posX+" , y:"+boxes[i].posY);
		}
		print ("es a "+array2D[9,6]);
		posG = new Vector2(9,6);
		posM = new Vector2(9,8);
		Reload();	
		moves =0;
	}
	void Reload(){
		for(int i=0; i<boxes.Length; i++){
			boxes[i].SetTextState(array2D[GetIndexY(i),GetIndexX(i)]);

		}
		
	}
	void MoveGTo(float nx,float ny){
		if(ny>=rows_X || ny<0 || nx>=columns_Y || nx<0  ){
			return;
		}
		if(array2D[(int)nx,(int)ny] == "#"){
			return;
		}
		// if(array2D[(int)nx,(int)ny] == "." || array2D[(int)nx,(int)ny] == "L" ){
			array2D[(int)posG[0],(int)posG[1]]=""+moves;
			moves++;
			array2D[(int)nx,(int)ny]="G"+moves;
			posG.Set(nx,ny);
			// print(nx+" , "+ny);
			Reload();
		// }



			// print("JODER, NO SE PUEDE");
	}
	void MoveMTo(float nx,float ny){
		if(ny>=rows_X || ny<0 || nx>=columns_Y || nx<0  ){
			return;
		}
		if(array2D[(int)nx,(int)ny] == "#"){
			return;
		}
		// if(array2D[(int)nx,(int)ny] == "." || array2D[(int)nx,(int)ny] == "L" ){
			array2D[(int)posM[0],(int)posM[1]]=""+moves;
			// moves++;
			array2D[(int)nx,(int)ny]="G"+moves;
			posM.Set(nx,ny);
			// print(nx+" , "+ny);
			Reload();
		// }
			// print("JODER, NO SE PUEDE");
	}
	public void Step(Vector2 dir){
	    if(dir == Vector2.left){
	    	MoveGTo(posG[0],posG[1]-1);
	    	MoveMTo(posM[0],posM[1]+1);
	    }

	    if(dir == Vector2.right){
	    	MoveGTo(posG[0],posG[1]+1);
	    	MoveMTo(posM[0],posM[1]-1);
	    }

	    if(dir == Vector2.up){
	    	MoveGTo(posG[0]-1,posG[1]);
	    	MoveMTo(posM[0]-1,posM[1]);
	    }

	    if(dir == Vector2.down){
	    	MoveGTo(posG[0]+1,posG[1]);
	    	MoveMTo(posM[0]+1,posM[1]);
	    }

	}

    void OnGUI() {
        Event e = Event.current;
        if ( e.type == EventType.KeyUp){
	        if( Input.GetKeyUp( KeyCode.LeftArrow ) ){
	            Debug.Log("Detected key code: LeftArrow");
	            Step(Vector2.left);
	        }  

	        if( Input.GetKeyUp( KeyCode.RightArrow ) ){
	            Debug.Log("Detected key code: RightArrow");
	            Step(Vector2.right);
	        }  

	        if( Input.GetKeyUp( KeyCode.UpArrow ) ){
	            Debug.Log("Detected key code: UpArrow");
	            Step(Vector2.up);
	        }  

	        if( Input.GetKeyUp( KeyCode.DownArrow ) ){
	            Debug.Log("Detected key code: DownArrow");
	            Step(Vector2.down);
	        }
        }
        
    }

	int GetIndexY(int _index){
		int result = (int)(_index /rows_X);
		return result;
	}

	int GetIndexX(int _index){
		int result = (int)(_index %rows_X) ;
			// columns_Y-(int)_pos.y)+(int)_pos.x -1

		// :   rows_X*( columns_Y-(int)_pos.x)+(rows_X-(int)_pos.y);

		return result;
	}
	
}
