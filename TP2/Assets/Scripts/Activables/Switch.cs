using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    [SerializeField]
    bool IsReusable = true;

    [SerializeField]
    bool StartingState = false;

    [SerializeField]
    Animator SwitchAnimator;

    public Activable ActivableObject;

    private bool state;
    private bool isUsed;

    void Start()
    {
        state = StartingState;
        isUsed = false;
    }

    public void TriggerSwitch()
    {
        // Already used an unreusable.
        if (!IsReusable && isUsed) { return; }

        isUsed = true;
        state = !state;
        SwitchAnimator.SetBool("State", state);
        ActivableObject?.Activate();
    }
}
