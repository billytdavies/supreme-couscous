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
		if(other.GetComponent<Destructible>() != null){
			other.GetComponent<Destructible>().hp -= 5;
		}
		if(other.tag == "Enemy"){
			other.GetComponent<EnemyMove>().hp -=5;
		}
		if(other.tag == "Player"){
			other.GetComponent<PlayerMove>().hp -=8;
		}
		if(other.tag == "Quit"){
			Application.Quit();
		}
	}
}
