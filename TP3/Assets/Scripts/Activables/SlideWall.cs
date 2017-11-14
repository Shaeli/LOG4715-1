using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideWall : Activable {

    [SerializeField]
    GameObject Wall;
    [SerializeField]
    float SlideRate = 0.5f;
    [SerializeField]
    bool StartingState;

    private bool state;

    private const float OnValue = 1.5f;
    private const float OffValue = -1.5f;

    // Use this for initialization
    void Start () {
        state = StartingState;
    }
	
	// Update is called once per frame
	void Update () {
		if (state)
        {
            Vector3 p = Wall.transform.localPosition;
            p.y = Mathf.Lerp(p.y, OffValue, SlideRate);
            Wall.transform.localPosition = p;
        }
        else
        {
            Vector3 p = Wall.transform.localPosition;
            p.y = Mathf.Lerp(p.y, OnValue, SlideRate);
            Wall.transform.localPosition = p;
        }
	}

    public override void Activate()
    {
        state = !state;
    }
}
