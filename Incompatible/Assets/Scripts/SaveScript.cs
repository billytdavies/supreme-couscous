using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour {
	public GameObject Clone;
	public Transform spawnpoint;
	Iteration CurrIt;
	bool Stop;
	Iteration PrevIt;
	void Start () {
		CurrIt = new Iteration();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.frameCount % 2 == 0){
			CurrIt.Positions.Enqueue(transform.position);
			CurrIt.Rotations.Enqueue(transform.rotation);
		}

		if(Input.GetKeyDown(KeyCode.H)){
			ResetIteration();
		}
	}
	public void ResetIteration(){
		PrevIt = CurrIt;
		CurrIt = new Iteration();
		CurrIt.IterationNum = PrevIt.IterationNum++;
		//print("New Iteration "+CurrIt.IterationNum);
		//print("Length"+PrevIt.Positions.ToArray().Length);
		var newClone = Instantiate(Clone,spawnpoint.position,Quaternion.identity);
		newClone.GetComponent<CloneMove>().It = PrevIt;
	}
}
