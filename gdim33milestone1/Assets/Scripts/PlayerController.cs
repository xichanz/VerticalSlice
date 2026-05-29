using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    [Header("Sprint Settings")]
    public float sprintSpeed = 9f;
    public float sprintDuration = 1.5f;
    public float sprintCooldown = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private bool isSprinting = false;
    private bool canSprint = true;
    private float sprintTimer = 0f;
    private float cooldownTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        HandleSprint();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float currentSpeed = isSprinting ? sprintSpeed : speed;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void HandleSprint()
    {
        if (canSprint && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            canSprint = false;
            sprintTimer = sprintDuration;
        }

        if (isSprinting)
        {
            sprintTimer -= Time.deltaTime;

            if (sprintTimer <= 0f)
            {
                isSprinting = false;
                cooldownTimer = sprintCooldown;
            }
        }

        if (!canSprint && !isSprinting)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                canSprint = true;
            }
        }
    }
}