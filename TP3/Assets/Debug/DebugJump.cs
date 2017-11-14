using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugJump : MonoBehaviour {

    public float JumpForce = 0f;
    public Animator RobotAnimator;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpForce);
            RobotAnimator.SetTrigger("Jump");

        }

    }

    public void SetGround(bool isGrounded)
    {
        RobotAnimator.SetBool("IsGrounded", isGrounded);
    }
}
