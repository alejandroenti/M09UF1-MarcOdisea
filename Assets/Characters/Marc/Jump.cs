using System.Collections.Generic;
using UnityEngine;

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
    private float alternativeJumpForce = 2.5f;
    private float nextJumpTimer = 0.0f;
    // Pasando el ángulo en radianes
    private float longJumpAngle = 20f * Mathf.PI / 180;

    private CharacterController controller;
    private Crouch crouchScript;
    private Movement movementScript;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        crouchScript = GetComponent<Crouch>();
        movementScript = GetComponent<Movement>();
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
            //      Sólo le empuja la gravidad
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
                        // DIRECTION = (FORWARD * COS(30) * FORCE) + (UP * SIN(30) * FORCE)
                        Vector3 jumpDirection = (controller.transform.forward * Mathf.Cos(longJumpAngle) * jumpForce * alternativeJumpForce) + (controller.transform.up * Mathf.Sin(longJumpAngle) * jumpForce * alternativeJumpForce);

                        Debug.Log(jumpDirection);

                        finalVelocity = jumpDirection;
                    }

                }
                else
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
            }
            else
            {
                finalVelocity.y = direction.y * gravity * Time.deltaTime;
                DeccelerateXZ();
            }
        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
            DeccelerateXZ();
        }

        //Aplicamos gravedad calculada sobre el cuerpo
        controller.Move(finalVelocity * Time.deltaTime);
    }

    public float GetYVelocity() => finalVelocity.y;

    private void DeccelerateXZ()
    {
        // Vamos decelerando al jugador
        // Multiplicamos por 2 la deceleración ya que el número que tenemos en el movimiento lo tenemos bien controlado
        // y en el salto la velocidad en XZ es el doble casi
        finalVelocity.x -= movementScript.GetDecceleration() * 2 * Time.deltaTime;
        finalVelocity.z -= movementScript.GetDecceleration() * 2 * Time.deltaTime;
        finalVelocity.x = Mathf.Clamp(finalVelocity.x, 0f, finalVelocity.x);
        finalVelocity.z = Mathf.Clamp(finalVelocity.z, 0f, finalVelocity.z);
    }
}
