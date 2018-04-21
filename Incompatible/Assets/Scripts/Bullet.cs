using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public GameObject puffParticles;
	void onCollisionEnter(Collider other){
		var particles = Instantiate(puffParticles,transform.position,Quaternion.identity);
		Destroy(particles,2f);
		Destroy(gameObject);
	}
}
