using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMove : MonoBehaviour {

	public Iteration It;
	int ItLength;
	int ItCount=0;
	public GameObject poof;
	bool Stopped;
	Transform head;
	GameObject headgo;
	void Start(){
		head = transform.GetChild(0);
		headgo = head.gameObject;
		ItLength = It.Positions.Count;
	}
	void Update () {
		if(!Stopped){
			if(Time.frameCount % 2 == 0){
				ItCount++;

				Vector3 newPosition = It.Positions.Dequeue();
				Quaternion newRotation = It.Rotations.Dequeue();
				
				//print("Clone "+It.Positions.ToArray().Length);
				//print("Clone "+newPosition.y.ToString());
				
				transform.position = newPosition;
				transform.rotation = Quaternion.Euler(0,newRotation.y,0);
				print(transform.rotation);
				headgo.transform.rotation = newRotation;
				
				It.Positions.Enqueue(newPosition);
				It.Rotations.Enqueue(newRotation);

				if(ItCount==ItLength){
					Stop();
				}
			}
		}
    }

    private void Stop()
    {
		print("Stopped");
        Stopped = true;
		var poofe = Instantiate(poof, transform.position, Quaternion.identity);
		Destroy(poofe,2f);
        transform.position = new Vector3(0, -10, 0);
    }

    public void Restart(){
		while(ItCount!=ItLength){
			ItCount++;
			It.Positions.Enqueue(It.Positions.Dequeue());
			It.Rotations.Enqueue(It.Rotations.Dequeue());	
		}
		
		print("Restarted");
		Stopped = false;
		ItCount=0;
	}
}