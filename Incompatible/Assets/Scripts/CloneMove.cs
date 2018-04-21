using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMove : MonoBehaviour {

	public Iteration It;
	Iteration ItBackup;
	public GameObject poof;
	bool Stopped;
	void Update () {
		if(It.Positions.Count != 0){
			if(Time.frameCount % 2 == 0){
				//ItBackup.Positions.Enqueue(It.Positions.Peek());
				//ItBackup.Rotations.Enqueue(It.Rotations.Peek());
				Vector3 newPosition = It.Positions.Dequeue();
				Quaternion newRotation = It.Rotations.Dequeue();
				//print("Clone "+It.Positions.ToArray().Length);
				//print("Clone "+newPosition.y.ToString());
				transform.position = newPosition;
				transform.rotation = newRotation;
			}
		} else{
            if(!Stopped){Stop();}
        }
    }

    private void Stop()
    {
        Stopped = true;
		Instantiate(poof, transform.position, Quaternion.identity);
        transform.position = new Vector3(0, -10, 0);
    }

    void Restart(){
		while(ItBackup.Positions.Peek() !=null){
			It.Positions.Enqueue(ItBackup.Positions.Dequeue());
			It.Rotations.Enqueue(ItBackup.Rotations.Dequeue());
		}
	}
}