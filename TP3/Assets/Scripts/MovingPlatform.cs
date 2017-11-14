using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField]
    Vector3 StartingPosition;
    [SerializeField]
    Vector3 EndPosition;
    [SerializeField]
    float MoveRate = 0.5f;

    private bool atStart;
    // Use this for initialization
    void Start () {
        atStart = true;

    }
	
	// Update is called once per frame
	void Update () {
            Vector3 curr = transform.position;
            curr = Vector3.MoveTowards(curr, (atStart) ? EndPosition : StartingPosition, MoveRate);
            transform.position = curr;

        if (curr.Equals((atStart) ? EndPosition : StartingPosition)) atStart = !atStart;
	}
}
