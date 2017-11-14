using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMovement : MonoBehaviour {

    public float Speed = 1f;

    public Animator RobotAnimator;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        float h = Input.GetAxis("Horizontal");

        if (h != 0.0)
        {
            this.gameObject.transform.Translate(Vector3.forward * Mathf.Abs(h) * Speed * Time.deltaTime);
            this.gameObject.transform.LookAt(this.gameObject.transform.position + Vector3.forward * h);
            RobotAnimator.SetBool("IsRunning", true);
        }
        else
        {
            RobotAnimator.SetBool("IsRunning", false);

        }

    }
}
