using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

	Transform[] mapTransforms;
	void Start () {
		mapTransforms = transform.GetComponentsInChildren<Transform>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void reset(){

		foreach (Transform obj in mapTransforms) {
			if(obj.gameObject.GetComponent<Destructible>() != null){
				obj.gameObject.GetComponent<Destructible>().reset();
			}
		}
	}

}
