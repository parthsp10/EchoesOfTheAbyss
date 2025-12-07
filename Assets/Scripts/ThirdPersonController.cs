using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Underwater Vertical Movement")]
    public Transform waterSurface;      // drag WaterSurface here
    public float swimUpSpeed = 3f;      // how fast you go up with Space
    public float sinkSpeed = -2f;       // how fast you sink when not pressing Space

    [Header("Gravity (above water)")]
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;           // used mainly for vertical movement

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // --- 1. Horizontal input (WASD) ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(h, 0f, v);

        // camera-relative movement
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 moveDir = camForward * v + camRight * h;
        bool hasInput = moveDir.sqrMagnitude > 0.01f;
        if (hasInput) moveDir.Normalize();

        // --- 2. Rotate toward movement direction ---
        if (hasInput)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }

        // --- 3. Underwater or not? ---
        bool isUnderwater = transform.position.y < waterSurface.position.y;

        if (isUnderwater)
        {
            // Underwater: Space = go up, otherwise sink
            if (Input.GetKey(KeyCode.Space))
            {
                velocity.y = swimUpSpeed;      // go up
            }
            else
            {
                velocity.y = sinkSpeed;        // always drifting down
            }
        }
        else
        {
            // Above water: normal gravity, no swim controls
            if (controller.isGrounded && velocity.y < 0f)
                velocity.y = -2f;

            velocity.y += gravity * Time.deltaTime;
        }

        // --- 4. Combine movement and vertical velocity ---
        Vector3 finalMove = Vector3.zero;

        if (hasInput)
            finalMove += moveDir * moveSpeed;

        finalMove += Vector3.up * velocity.y;

        controller.Move(finalMove * Time.deltaTime);
    }
}
