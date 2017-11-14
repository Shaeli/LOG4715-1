using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotWall : Activable {

    [SerializeField]
    float RotateAngle;
    [SerializeField]
    float RotationRate = 1f;

    [SerializeField]
    GameObject Root;

    private float targetAngle;
    private float startAngle;

    private bool atStartAngle;


    // Use this for initialization
    void Start () {
        atStartAngle = true;
        startAngle = Root.transform.eulerAngles.x;
        targetAngle = Root.transform.eulerAngles.x;
    }
	
	// Update is called once per frame
	void Update () {

        // Rotate smoothly.
        Vector3 angles = Root.transform.eulerAngles;
        angles.x = Mathf.LerpAngle(Root.transform.eulerAngles.x, targetAngle, RotationRate);
        Root.transform.eulerAngles = angles; 
	}

    public override void Activate()
    {
        if (atStartAngle)
        {
            RotateWall(RotateAngle);
        }
        else
        {
            RotateWall(startAngle);
        }
        atStartAngle = !atStartAngle;
    }

    private void RotateWall(float angle)
    {
        targetAngle = angle;
    }
}
