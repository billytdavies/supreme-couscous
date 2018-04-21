using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public GameObject bullet;
	public float cooldown;
	bool canShoot = true;
	public void Shoot () {
		if(canShoot){
			var bulletObject = Instantiate(bullet,transform.position,Quaternion.identity);
			bulletObject.GetComponent<Rigidbody>().AddForce(transform.forward*1000);
			Destroy(bulletObject,4f);
			canShoot = false;
			StartCoroutine(shootDelay());
		}
	}
	IEnumerator shootDelay(){
		yield return new WaitForSeconds(cooldown);
		canShoot = true;
	}
}
