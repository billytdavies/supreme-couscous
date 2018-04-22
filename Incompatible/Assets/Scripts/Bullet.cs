using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public GameObject puffParticles;
	void OnTriggerEnter(Collider other){
		if(other.tag != "Gun") {
			Instantiate(puffParticles,transform.position,Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
