using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Set up Jump")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float maxNextJumpTimer = 0.3f;

    [Header("Set up Gravity")]
    [SerializeField] private float gravity = 20f;

    private float directionY = -1f;
    private Vector3 finalVelocity = Vector3.zero;
    private List<float> forceAdded = new List<float>()
                                     {1.0f, 1.15f, 1.4f};
    private int jumpCount = 0;
    private float alternativeJumpForce = 1.65f;
    private float nextJumpTimer = 0.0f;
    // Pasando el ángulo en radianes
    private float longJumpAngle = 20f * Mathf.PI / 180;
    private float mortalJumpAngle = 60f * Mathf.PI / 180;
    private float wallJumpAngle = 45f * Mathf.PI / 180;

    private float accelerateZ;

    private CharacterController controller;
    private Crouch crouchScript;
    private Movement movementScript;
    private WallDetect wallDetectScript;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        crouchScript = GetComponent<Crouch>();
        movementScript = GetComponent<Movement>();
        wallDetectScript = GetComponent<WallDetect>();
    }

    private void Update()
    {
        Vector3 direction = Vector3.up * directionY;
        direction.Normalize();

        if (controller.isGrounded)
        {
            nextJumpTimer += Time.deltaTime;

            // Saltaremos si pulsamos el botón SOUTH o SPACE
            //
            //      Revisaremos si el jugador se encuentra en crouching
            //      (solo podrá hacerlo en el suelo) y en movimiento en XZ;
            //      haremos un salto de 20 grados de fuerza, haciendo que sea más largo que alto en
            //      el vector FORWARD del Character Controller
            //
            //      Revisamos si el jugador se encuentra agachado y su velocidad en XZ es igual a 0
            //      Sólo le empuja la gravedad
            //      Lo empujaremos con la fuerza del TERCER SALTO con un ángulo de 100 grados (X negativa Y positiva)
            //      y haciendo la animación del mortal
            //
            //      Revisaremos si el jugador ha pulsado 0.25s antes de tocar el suelo
            //      O si lo ha pulsado 0.25s después de tocar el suelo
            //          En esos casos, saltaremos con en el siguiente jump (si el 3, haremos el primero)
            //          Sino haremos el primer salto y volvemos a comenzar el conteo de saltos
            //           

            if (Input_Manager._INPUT_MANAGER.GetButtonSouthValue())
            {
                if (crouchScript.GetIsCrouching())
                {
                    if (movementScript.GetVelocityXZ() > 0)
                    {
                        LongJump();
                    }
                    else if (movementScript.GetVelocityXZ() == 0)
                    {
                        MortalJump();
                    }

                }
                else
                {
                    if (nextJumpTimer <= maxNextJumpTimer)
                    {
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
                    nextJumpTimer = 0f;
                    jumpCount++;
                }
            }
            else
            {
                Debug.Log("Ento ELSE GetSouthButton");
                finalVelocity.y = direction.y * gravity * Time.deltaTime;
                DeccelerateXZ();
            }
        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;

            // SI nos encontramos en el aire y estamos en contacto con una pared de frente (FORWARD)
            // al pusar el BUTTON SOUTH o el SPACE, haremos un salto en dirreción contraria
            // en un águlo de 45 grados

            if (Input_Manager._INPUT_MANAGER.GetButtonSouthValue())
            {

                if (wallDetectScript.GetIsOnWall())
                {
                    WallJump();
                    Debug.Log("Walljumping");
                }
                else if (Input_Manager._INPUT_MANAGER.GetButtonSouthTimer() <= maxNextJumpTimer)
                {
                    nextJumpTimer = 0f;
                }
            }
            Debug.Log("Ento ELSE isGrounded");

            if (finalVelocity.y > 0 && accelerateZ != 0f)
            {
                finalVelocity.z += movementScript.GetAcceleration() * Time.deltaTime;
                finalVelocity.z = Mathf.Clamp(finalVelocity.z, 0f, accelerateZ);
            }
        }

        //Aplicamos gravedad calculada sobre el cuerpo
        controller.Move(finalVelocity * Time.deltaTime);
    }

    public float GetYVelocity() => finalVelocity.y;

    private void DeccelerateXZ()
    {
        // Vamos decelerando al jugador
        finalVelocity.x -= movementScript.GetDecceleration() * Time.deltaTime;
        finalVelocity.z -= movementScript.GetDecceleration() * Time.deltaTime;

        finalVelocity.x = Mathf.Clamp(finalVelocity.x, 0f, finalVelocity.x);
        finalVelocity.z = Mathf.Clamp(finalVelocity.z, 0f, finalVelocity.z);

        accelerateZ = 0.0f;
    }

    private void LongJump()
    {
        // DIRECTION = (FORWARD * COS(20) * FORCE) + (UP * SIN(20) * FORCE)
        Vector3 jumpDirection = (controller.transform.forward * Mathf.Cos(longJumpAngle)) + (controller.transform.up * Mathf.Sin(longJumpAngle));
        jumpDirection.Normalize();
        
        jumpDirection *= jumpForce * alternativeJumpForce;

        finalVelocity = jumpDirection;
    }

    private void MortalJump()
    {
        // DIRECTION = (FORWARD * COS(60) * -1 * FORCE) + (UP * SIN(60) * FORCE)
        Vector3 jumpDirection = (controller.transform.forward * -1f *  Mathf.Cos(wallJumpAngle)) + (controller.transform.up * Mathf.Sin(wallJumpAngle));
        jumpDirection.Normalize();

        accelerateZ = jumpDirection.z * jumpForce * alternativeJumpForce;
        jumpDirection.y *= jumpForce * alternativeJumpForce;

        finalVelocity = jumpDirection;
    }

    private void WallJump()
    {
        // DIRECTION = (FORWARD * COS(45) * -1 * FORCE) + (UP * SIN(45) * FORCE)
        Vector3 jumpDirection = (controller.transform.forward * -1f * Mathf.Cos(mortalJumpAngle)) + (controller.transform.up * Mathf.Sin(mortalJumpAngle));
        jumpDirection.Normalize();
        
        accelerateZ = jumpDirection.z * jumpForce * alternativeJumpForce;
        jumpDirection.y *= jumpForce * alternativeJumpForce;

        finalVelocity = jumpDirection;
    }
}
