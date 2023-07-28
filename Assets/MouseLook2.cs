using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook2 : MonoBehaviour
{
    public float mouseSensX = 0.5f;
    public float mouseSensY = 0.5f;

    public Transform playerHead;

    private Vector2 mouseDelta;
    private Vector3 headRotation;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        headRotation = playerHead.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        mouseDelta = Mouse.current.delta.ReadValue();

        headRotation.y += mouseDelta.x * mouseSensX;
        headRotation.x -= mouseDelta.y * mouseSensY;
        headRotation.x = Mathf.Clamp(headRotation.x, -45f, 45f);

        playerHead.rotation = Quaternion.Euler(headRotation);

    }
}
