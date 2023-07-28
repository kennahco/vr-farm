using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerActivator : MonoBehaviour
{
    public bool isRightSide;
    public InputActionReference pointerInputAction;
    public GameObject objectToEnable;
    public GameObject objectToDisable;

    private static bool runRightOnStart;

    private void Awake()
    {
        if(isRightSide && runRightOnStart)
        {
            objectToEnable.SetActive(true);
            objectToDisable.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()   
    {
        if (pointerInputAction.action.triggered && !objectToEnable.activeSelf)
        {
            objectToEnable.SetActive(true);
            objectToDisable.SetActive(false);
            runRightOnStart = isRightSide;
        }
    }
}
