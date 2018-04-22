using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public int hp;
	Vector3 startPos;
	int starthp;
	void Start(){
		startPos = transform.position;
		starthp = hp;
	}
	void Update(){
		if(hp <= 0){
			transform.position = new Vector3(0,-10,10);
		}
	}
	public void reset(){
		hp = starthp;
		transform.position = startPos;
		
	}
}
