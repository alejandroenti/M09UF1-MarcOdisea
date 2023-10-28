using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Jump : MonoBehaviour
{
    [Header("Set up Jump")]
    [SerializeField] private float jumpForce = 8f;

    [Header("Set up gravity")]
    [SerializeField] private float gravity = 20f;

    private float directionY = -1f;
    private Vector3 finalVelocity = Vector3.zero;
    private List<float> forceAdded = new List<float>()
                                     {1.0f, 1.2f, 1.3f};
    private int jumpCount = 0;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

    }

    private void Update()
    {
        Vector3 direction = Vector3.up * directionY;
        direction.Normalize();

        if (controller.isGrounded)
        {
            if (Input_Manager._INPUT_MANAGER.GetButtonSouthValue())
            {
                finalVelocity.y = jumpForce * forceAdded[jumpCount];
                jumpCount++;

                if (jumpCount >=  forceAdded.Count)
                {
                    jumpCount = 0;
                }
            }
            else
            {
                finalVelocity.y = direction.y * gravity * Time.deltaTime;
            }
        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
        }

        //Aplicamos gravedad calculada sobre el cuerpo
        controller.Move(finalVelocity * Time.deltaTime);
    }

    public float GetYVelocity() => finalVelocity.y;
}
