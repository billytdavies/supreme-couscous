using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public Transform spawnpoint;
	public Rigidbody rb;
	public float speed = 10.0f;
	private bool onFloor = true;
	private bool canMove = true;
	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if(canMove)
		{
			float translation = Input.GetAxisRaw("Vertical") * speed;
			float strafe = Input.GetAxisRaw("Horizontal") * speed;
			translation *= Time.deltaTime;
			strafe *= Time.deltaTime;

			transform.Translate(strafe, 0, translation);
		
			if (Input.GetKeyDown("space"))
			{
				if(onFloor == true) {
					rb.velocity = new Vector3(0, 7, 0);
				}	
			}
		}
		if (Input.GetKeyDown("escape"))
		{
			Cursor.lockState = CursorLockMode.None;
		}

	}
	void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "Floor")
		{
			onFloor = true;
		}
		
		if (other.transform.tag == "Enemy")
		{
			transform.position = spawnpoint.transform.position;
		}
	}
	void OnCollisionExit(Collision other)
	{
		if(other.transform.tag == "Floor")
		{
			onFloor = false;
		}
	} 
}	
