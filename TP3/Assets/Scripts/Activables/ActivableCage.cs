using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableCage : Activable {


    [SerializeField]
    GameObject cage;

    [SerializeField]
    bool StartingState;

    private bool state;

    // Use this for initialization
    void Start()
    {
        state = StartingState;
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            cage.SetActive(false);
        }
        else
        {
            cage.SetActive(true);
        }
    }

    public override void Activate()
    {
        state = !state;
    }
}
