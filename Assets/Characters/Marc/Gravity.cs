using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Gravity : MonoBehaviour
{
    [Header("Set up gravity")]
    [SerializeField] private float gravity = 20f;

    private float directionY = -1f;
    private Vector3 finalVelocity = Vector3.zero;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Calcular dirección XZ
        Vector3 direction = Vector3.up * directionY;
        direction.Normalize();

        // Calculamos el efecto de la gravedad sobre el cuerpo
        if (controller.isGrounded)
        {
            finalVelocity.y = direction.y * gravity * Time.deltaTime;
        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
        }

        //Calcular gravedad calculada sobre el cuerpo
        controller.Move(finalVelocity * Time.deltaTime);
    }
}
