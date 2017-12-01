using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatform : MonoBehaviour {

    public bool playerArrived { get; set; }

	// Use this for initialization
	void Start () {

        playerArrived = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player")) { 
            playerArrived = true;
            transform.GetComponentInChildren<WalkingDrone>().player = coll.gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
            playerArrived = false;
    }
}
