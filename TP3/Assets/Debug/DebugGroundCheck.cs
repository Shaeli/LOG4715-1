using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGroundCheck : MonoBehaviour {

    public DebugJump jumpScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        jumpScript.SetGround(true);
    }

    void OnTriggerExit(Collider other)
    {
        jumpScript.SetGround(false);
    }
}
