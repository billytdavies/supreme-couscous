using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour {

	private Iteration CurrIt;
	private Iteration PrevIt;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.frameCount % 10 ==0){
			CurrIt.Positions.Enqueue(transform);
		}
	}
	public void ResetIteration(){
		PrevIt = CurrIt;
		CurrIt = new Iteration();
		CurrIt.IterationNum = PrevIt.IterationNum++;
		
	}
}
