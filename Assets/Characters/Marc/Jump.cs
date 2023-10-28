using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Jump : MonoBehaviour
{
    [Header("Set up Jump")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float maxNextJumpTimer = 0.3f;

    [Header("Set up gravity")]
    [SerializeField] private float gravity = 20f;

    private float directionY = -1f;
    private Vector3 finalVelocity = Vector3.zero;
    private List<float> forceAdded = new List<float>()
                                     {1.0f, 1.2f, 1.3f};
    private int jumpCount = 0;
    private float nextJumpTimer = 0.0f;

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
            nextJumpTimer += Time.deltaTime;

            // Saltaremos si pulsamos el botón SOUTH o SPACE
            //      Revisaremos si el jugador ha pulsado 0.25s antes de tocar el suelo
            //      O si lo ha pulsado 0.25s después de tocar el suelo
            //          En esos casos, saltaremos con en el siguiente jump (si el 3, haremos el primero)
            //          Sino haremos el primer salto y volvemos a comenzar el conteo de saltos
            
            if (Input_Manager._INPUT_MANAGER.GetButtonSouthValue())
            {
                if ((Input_Manager._INPUT_MANAGER.GetButtonSouthTimer() <= maxNextJumpTimer || nextJumpTimer <= maxNextJumpTimer))
                {
                    jumpCount++;

                    if (jumpCount >= forceAdded.Count)
                    {
                        jumpCount = 0;
                    }
                }
                else
                {
                    jumpCount = 0;
                }

                finalVelocity.y = jumpForce * forceAdded[jumpCount];

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
