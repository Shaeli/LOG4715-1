using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayer : MonoBehaviour {

    private float offSet;

	// Use this for initialization
	void Start () {
        offSet = transform.GetComponentInParent<SlideWall>().SlideRate;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            coll.transform.parent.parent = transform;
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector3 dest = new Vector3(coll.transform.position.x, coll.transform.position.y, transform.position.z + 1f);
            coll.GetComponentInChildren<Rigidbody>().MovePosition(Vector3.Slerp(coll.transform.position, dest, Time.deltaTime));
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
