using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            coll.transform.parent.parent = transform;
            coll.gameObject.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(0, 0, 5), ForceMode.Impulse);
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            coll.gameObject.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(0, 0, 5), ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            coll.transform.parent.parent = null;
        }
    }

}
