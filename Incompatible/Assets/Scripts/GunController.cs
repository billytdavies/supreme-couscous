using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public GameObject bullet;

	public void Shoot () {
		var bulletObject = Instantiate(bullet,transform.position,Quaternion.identity);
		bulletObject.GetComponent<Rigidbody>().AddForce(Vector3.right);
		Destroy(bulletObject,4f);
	}
}
