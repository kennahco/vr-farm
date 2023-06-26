using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2f;
    private float jumpHeight = 1f;
    private float gravityValue = -9.81f;
    private InputMaster inputMaster;
    private Vector2 moveInput;
    private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        inputMaster = new InputMaster();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        moveInput = inputMaster.Player.Move.ReadValue<Vector2>();
        move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
