using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Orientation")]
    [SerializeField] private Camera m_Camera;

    [Header("Movement variables")]
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float maxvelocityXZ = 5f;
    [SerializeField] private float maxvelocityXZCrouching = 3f;
    [SerializeField, Range(2.0f, 10.0f)] private float rotationSpeed;

    private Vector3 finalVelocity = Vector3.zero;
    private Vector3 tmpDir = Vector3.zero;
    private float velocityXZ = 0f;

    private CharacterController controller;
    private Jump jumpScript;
    private Crouch crouchScript;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        jumpScript = GetComponent<Jump>();
        crouchScript = GetComponent<Crouch>();
    }

    private void Update()
    {
        Vector3 direction = Vector3.zero;

        if (!jumpScript.GetIsWallJumping())
        {
            //Calcular direccion XZ
            direction = Quaternion.Euler(0f, m_Camera.transform.eulerAngles.y, 0f) * new Vector3(Input_Manager._INPUT_MANAGER.GetLeftAxisValue().x, 0f, Input_Manager._INPUT_MANAGER.GetLeftAxisValue().y);
            direction.Normalize();

            // Calcular la aceleración en XZ
            // Nos guardamos la direccíón en la que estábamos yendo para
            // poder continuar el movimento en caso que dejemos de pulsar
            // o mover el joystick y no se frene en seco (direction = Vector3.zero)
            if (direction != Vector3.zero)
            {
                velocityXZ += acceleration * Time.deltaTime;
                tmpDir = direction;
            }
            else if (!jumpScript.GetIsWallJumping())
            {
                velocityXZ -= decceleration * Time.deltaTime;
                direction = tmpDir;
            }

            // Limitamos la velocidad a la máxima indicada en el inspector
            // Diferenciaremos entre la velocidad agachado o normal

            if (crouchScript.GetIsCrouching())
            {
                velocityXZ = Mathf.Clamp(velocityXZ, 0f, maxvelocityXZCrouching);
            }
            else
            {
                velocityXZ = Mathf.Clamp(velocityXZ, 0f, maxvelocityXZ);
            }

            // Calculamos y aplicamos la rotación del personaje
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            this.transform.forward = direction;

            //Calcular velocidad XZ
            finalVelocity.x = direction.x * velocityXZ;
            finalVelocity.z = direction.z * velocityXZ;

            finalVelocity.y = jumpScript.GetYVelocity();

            controller.Move(finalVelocity * Time.deltaTime);
        }
        else
        {
            velocityXZ = 0f;
        }
    }

    public float GetVelocityXZ() => velocityXZ;
    public float GetDecceleration() => decceleration;
    public float GetAcceleration() => acceleration;
}
