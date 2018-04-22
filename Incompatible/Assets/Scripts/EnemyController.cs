using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	Transform[] EnemyTransforms;
	void Start () {
		EnemyTransforms = transform.GetComponentsInChildren<Transform>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void reset(){
		foreach (Transform obj in EnemyTransforms) {
			if(obj.gameObject.GetComponent<EnemyMove>() != null){
				obj.gameObject.GetComponent<EnemyMove>().reset();
				obj.gameObject.GetComponent<EnemyMove>().LookForPlayers();
			}
		}
	}

}
