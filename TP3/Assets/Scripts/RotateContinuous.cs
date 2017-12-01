using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateContinuous : MonoBehaviour {

    [SerializeField] Vector3 Rate;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Rate * Time.deltaTime);
	}
}
