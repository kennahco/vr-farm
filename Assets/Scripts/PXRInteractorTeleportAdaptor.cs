using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PXRInteractorTeleportAdaptor : MonoBehaviour
{
    public Transform gameObjectToTeleport;
    public XRRayInteractor xRRayInteractor;
    public Vector3 offset;

    private bool pressedLastTick = false;

    // Update is called once per frame
    void Update()
    {
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out var r_Button);
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.triggerButton, out var l_Button);
        if(r_Button || l_Button)
        {
            if(!pressedLastTick)
            {
                pressedLastTick = true;
                Vector3 position = Vector3.zero;
                Vector3 rotation = Vector3.zero;
                bool isValid = false;
                int posInLine = 0;

                xRRayInteractor.TryGetHitInfo(out position, out rotation, out posInLine, out isValid);

                if (isValid)
                {
                    gameObjectToTeleport.position = position + offset;
                }
            }
        }
        else
        {
            pressedLastTick = false;
        }
    }
}
