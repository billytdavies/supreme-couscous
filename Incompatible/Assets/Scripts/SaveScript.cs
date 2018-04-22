using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour {
	public GameObject Clone;
	//public GameObject CloneHead;
	//public GameObject CloneGun;
	public Transform spawnpoint;
	Iteration CurrIt;
	bool Stop;
	Iteration PrevIt;
	List<GameObject> clones = new List<GameObject>();
	int ShootFrameDifference;

	int ShootTime;
	int PrevShootTime;
	void Start () {
		CurrIt = new Iteration();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.frameCount % 2 == 0){
			CurrIt.Positions.Enqueue(transform.position);
			CurrIt.Rotations.Enqueue(Camera.main.transform.rotation);
		}

		if(Input.GetKeyDown(KeyCode.H)){
			ResetIteration();
		}
		if (Input.GetButtonDown("Fire1"))
		{
			ShootTime = Time.frameCount;

			ShootFrameDifference = ShootTime-PrevShootTime;
			PrevShootTime = ShootTime;

			CurrIt.Shoot.Enqueue(ShootFrameDifference);
		}
	}
	public void ResetIteration(){
		transform.position = spawnpoint.position;

		//make a new clone from the previous iteration
		PrevIt = CurrIt;
		CurrIt = new Iteration();
		CurrIt.IterationNum = PrevIt.IterationNum++;
		//print("New Iteration "+CurrIt.IterationNum);
		//print("Length"+PrevIt.Positions.ToArray().Length);
		GameObject newClone = Instantiate(Clone,spawnpoint.position,Quaternion.identity);

		clones.Add(newClone);
		
		//var newCloneHead = Instantiate(CloneHead,spawnpoint.position,Quaternion.identity);
		//var newCloneGun = Instantiate(CloneGun,spawnpoint.position,Quaternion.identity);

		newClone.GetComponent<CloneMove>().It = PrevIt;

		foreach (GameObject cl in clones){
			cl.GetComponent<CloneMove>().Restart();
		}

	}
}
