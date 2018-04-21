using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
	string state;
	public int hp;
	public GameObject gun;
	Transform[] players;
	void Start(){
		state = "idle";
		GameObject[] goArray = GameObject.FindGameObjectsWithTag("Player");
		players = new Transform[goArray.Length];
 
 		for(int i = 0; i < goArray.Length; i++)
 		{
     		players [i] = goArray[i].transform;
 		}
	}

	void Update () {
		if(state == "idle"){
			if(PlayerCheck()){
				state = "attack";
			}
		} else if(state == "attack"){
			if(hp<50){
				state = "retreat";
			} else if(!PlayerCheck()){
				state = "idle";
			}
        Vector3 direction = (NearestPlayer(players).position - gun.transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		RaycastHit hit;
		if(Physics.Raycast(gun.transform.position,gun.transform.forward,out hit,10)){
			if(hit.transform.tag=="Player"){
			gun.GetComponent<GunController>().Shoot();
			}
		} else{
			transform.rotation = Quaternion.Euler(0,Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20).eulerAngles.y,0);	
			gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, lookRotation, Time.deltaTime * 20);
		
		}

		} else if(state == "retreat"){
			if(!PlayerCheck()){
				hp = 100;
				state = "idle";
			}
		Vector3 direction = (NearestPlayer(players).position - transform.position).normalized*-1;
		transform.Translate(direction*Time.deltaTime*3);
		}

	}
	
	bool PlayerCheck(){
		bool IsPlayer = false;
		Collider[] Colliders = Physics.OverlapSphere(transform.position,10);
		foreach (Collider col in Colliders)
		{
			if(col.tag == "Player"){
				IsPlayer = true;
			}
		}
		return IsPlayer;
	}
	Transform NearestPlayer(Transform[] players){
		Transform tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (Transform t in players)
		{
			float dist = Vector3.Distance(t.position, currentPos);
			if (dist < minDist)
			{
				tMin = t;
				minDist = dist;
			}
		}
		return tMin;
	}
}
