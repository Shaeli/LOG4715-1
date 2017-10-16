using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondsRotation : MonoBehaviour {


	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 45, 5) * Time.deltaTime * 2);
    }
}
