using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMove : MonoBehaviour {

	public Iteration It;
	
	
	int ItLength;
	int ItCount=0;
	int ShootItLength;
	int ShootItCount=0;

	public GameObject poof;
	bool Stopped;
	GameObject head;
	GameObject gun;
	void Start(){
		head = transform.GetChild(0).gameObject;
		ItLength = It.Positions.Count;
		ShootItLength = It.Shoot.Count;
		gun = head.transform.GetChild(0).gameObject;
		if(It.Shoot.Count != 0 ){StartCoroutine(ShootCycle());}
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
				head.transform.rotation = newRotation;
				
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
        StopAllCoroutines ();


        Stopped = true;
		var poofe = Instantiate(poof, transform.position, Quaternion.identity);
		Destroy(poofe,2f);
        transform.position = new Vector3(0, -10, 0);
    }

    public void Restart(){
        StopAllCoroutines ();
		while(ItCount!=ItLength){
			ItCount++;
			It.Positions.Enqueue(It.Positions.Dequeue());
			It.Rotations.Enqueue(It.Rotations.Dequeue());	
		}
		
		while(ShootItCount!=ShootItLength){
			ShootItCount++;
			It.Shoot.Enqueue(It.Shoot.Dequeue());
		}

		Stopped = false;
		ItCount=0;
		ShootItCount=0;
		if(It.Shoot.Count != 0 ){StartCoroutine(ShootCycle());}

	}
	IEnumerator ShootCycle(){
		
		for (int i=0; i<It.Shoot.Peek(); i++)
 		{
    		yield return null;
 		}
		gun.GetComponent<GunController>().Shoot();
		ShootItCount++;
		It.Shoot.Enqueue(It.Shoot.Dequeue());
		if(ShootItCount!=ShootItLength){
			RestartCoroutine(ShootCycle());
		}
	}
    public void RestartCoroutine (IEnumerator co)
    {
		StopAllCoroutines();
		StartCoroutine(co);
    }
}