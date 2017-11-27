using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableMovingPlateform : Activable {

    [SerializeField]
    Vector3 StartingPosition;
    [SerializeField]
    Vector3 EndPosition;
    [SerializeField]
    float MoveRate = 0.5f;
    [SerializeField]
    bool StartingState;

    private bool state;
    private bool atStart;

    // Use this for initialization
    void Start()
    {
        state = StartingState;
        atStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            Vector3 curr = transform.position;
            curr = Vector3.MoveTowards(curr, (atStart) ? EndPosition : StartingPosition, MoveRate);
            transform.position = curr;

            if (curr.Equals((atStart) ? EndPosition : StartingPosition)) atStart = !atStart;
        }
    }

    public override void Activate()
    {
        state = !state;
    }
}
