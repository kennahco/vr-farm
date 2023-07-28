using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomVRIFGrabber : Grabber
{
    public InputActionReference spawnerInputAction;
    [HideInInspector]
    public float currentGrabTime;

    public override bool GetInputDownForGrabbable(Grabbable grabObject)
    {

        if (grabObject == null)
        {
            return false;
        }

        // Check Hold Controls
        HoldType closestHoldType = getHoldType(grabObject);
        GrabButton closestGrabButton = GetGrabButton(grabObject);

        // Hold to grab controls
        if (closestHoldType == HoldType.HoldDown)
        {
            bool grabInput = spawnerInputAction.action.ReadValue<float>() >= GripAmount;

            /*
            if (!FreshGrip)
            {
                return false;
            }
            */

            //Debug.Log(grabInput);
            return grabInput;
        }
        // Check Toggle Controls
        else if (closestHoldType == HoldType.Toggle)
        {
            return spawnerInputAction.action.ReadValue<float>() >= GripAmount;
        }

        return false;
    }

    protected override bool inputCheckRelease()
    {

        var grabbingGrabbable = RemoteGrabbingItem ? RemoteGrabbingGrabbable : HeldGrabbable;

        // Can't release anything we're not holding
        if (grabbingGrabbable == null)
        {
            return false;
        }

        // Check Hold Controls
        HoldType closestHoldType = getHoldType(grabbingGrabbable);
        GrabButton closestGrabButton = GetGrabButton(grabbingGrabbable);

        if (closestHoldType == HoldType.HoldDown)
        {
            return spawnerInputAction.action.ReadValue<float>() <= ReleaseGripAmount;
        }
        // Check for toggle controls
        else if (closestHoldType == HoldType.Toggle)
        {
            return spawnerInputAction.action.ReadValue<float>() >= GripAmount;
        }

        return false;
    }

    HoldType getHoldType(Grabbable grab)
    {
        HoldType closestHoldType = grab.Grabtype;

        // Inherit from Grabber
        if (closestHoldType == HoldType.Inherit)
        {
            closestHoldType = DefaultHoldType;
        }

        // Inherit isn't a value in itself. Use "hold down" instead and warn the user
        if (closestHoldType == HoldType.Inherit)
        {
            closestHoldType = HoldType.HoldDown;
            Debug.LogWarning("Inherit found on both Grabber and Grabbable. Consider updating the Grabber's DefaultHoldType");
        }

        return closestHoldType;
    }

    void updateFreshGrabStatus()
    {
        // Update Fresh Grab status
        if (spawnerInputAction.action.ReadValue<float>() <= ReleaseGripAmount)
        {
            // We release grab, so this is considered fresh
            FreshGrip = true;
            currentGrabTime = 0;
        }

        // Increment fresh grab time
        if (spawnerInputAction.action.ReadValue<float>() >= GripAmount)
        {
            currentGrabTime += Time.deltaTime;
        }

        // Not considered a valid grab if holding down for too long
        if (currentGrabTime > GrabCheckSeconds)
        {
            FreshGrip = false;
        }
    }

    override public void Update()
    {
        // Make sure grab is valid
        updateFreshGrabStatus();
        base.Update();
    }
}
