using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
	string state;
	public int hp;
	public GameObject gun;
	Transform[] players;
	Vector3 startpos;

	public int range;
	void Start()
    {
        startpos = transform.position;
        state = "idle";
        LookForPlayers();
    }

    public void LookForPlayers()
    {
        GameObject[] goArray = GameObject.FindGameObjectsWithTag("Player");
        players = new Transform[goArray.Length];

        for (int i = 0; i < goArray.Length; i++)
        {
            players[i] = goArray[i].transform;
        }
	}

    void Update ()
    {
        StateCycle();
		if(hp<=0){
			transform.position = new Vector3(0,-50,0);
		}
    }

    private void StateCycle()
    {
        if (state == "idle")
        {
            if (PlayerCheck())
            {
                state = "attack";
            }
        }
        else if (state == "attack")
        {
            if (hp < 50)
            {
                state = "retreat";
            }
            else if (!PlayerCheck())
            {
                state = "idle";
            }
            Vector3 direction = (NearestPlayer(players).position - gun.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            gun.GetComponent<GunController>().Shoot();


            transform.rotation = Quaternion.Euler(0, Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20).eulerAngles.y, 0);
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, lookRotation, Time.deltaTime * 20);



        }
        else if (state == "retreat")
        {
            if (!PlayerCheck())
            {
                hp = 100;
                state = "idle";
            }
            Vector3 direction = (transform.position - NearestPlayer(players).position).normalized;
			transform.Translate(direction * Time.deltaTime * 5);

        }
    }

    bool PlayerCheck(){
		bool IsPlayer = false;
		Collider[] Colliders = Physics.OverlapSphere(transform.position,range);
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
		float minDist = (float)range;
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
	public void reset(){
		transform.position=startpos;
		hp = 100;
		state = "idle";
	}	
}
