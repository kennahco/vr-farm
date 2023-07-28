using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloodValveController : MonoBehaviour
{
    public HingeJoint hinge;
    public float hingeSpinRequirement;
    public Animator animator;
    public UnityEvent ValveOpened;

    private bool hasBeenOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hinge.angle > hingeSpinRequirement && !hasBeenOpened)
        {
            OpenValve();
        }
    }

    private void OpenValve()
    {
        hasBeenOpened = true;
        animator.enabled = true;
        ValveOpened.Invoke();
    }
}
