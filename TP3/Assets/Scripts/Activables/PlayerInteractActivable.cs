using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractActivable : MonoBehaviour {

    private Switch currentSwitch;

    void Update()
    {
        if (Input.GetButtonDown("Interact" + GetComponent<Multiplayer>().PlayerNumber))
        {
            //Debug.Log("Interact");
            currentSwitch?.TriggerSwitch();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("enter");
        Switch script = other.gameObject.GetComponent<Switch>();
        if (script != null) currentSwitch = script;
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit");
        if (other.gameObject == currentSwitch?.gameObject)
        {
            currentSwitch = null;
        }
    }
}
