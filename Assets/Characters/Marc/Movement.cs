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
        //Calcular direccion XZ
        Vector3 direction = Quaternion.Euler(0f, m_Camera.transform.eulerAngles.y, 0f) * new Vector3(Input_Manager._INPUT_MANAGER.GetLeftAxisValue().x, 0f, Input_Manager._INPUT_MANAGER.GetLeftAxisValue().y);
        direction.Normalize();

        // Calcular la aceleraci�n en XZ
        // Nos guardamos la direcc��n en la que est�bamos yendo para
        // poder continuar el movimento en caso que dejemos de pulsar
        // o mover el joystick y no se frene en seco (direction = Vector3.zero)
        if (direction !=  Vector3.zero)
        {
            velocityXZ += acceleration * Time.deltaTime;
            tmpDir = direction;
        }
        else
        {
            velocityXZ -= decceleration * Time.deltaTime;
            direction = tmpDir;
        }

        // Limitamos la velocidad a la m�xima indicada en el inspector
        // Diferenciaremos entre la velocidad agachado o normal

        if (crouchScript.GetIsCrouching())
        {
            velocityXZ = Mathf.Clamp(velocityXZ, 0f, maxvelocityXZCrouching);
        }
        else
        {
            velocityXZ = Mathf.Clamp(velocityXZ, 0f, maxvelocityXZ);
        }

        //Calcular velocidad XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;

        finalVelocity.y = jumpScript.GetYVelocity();

        controller.Move(finalVelocity * Time.deltaTime);
    }

    public float GetVelocityXZ() => velocityXZ;
    public float GetDecceleration() => decceleration;
    public float GetAcceleration() => acceleration;
}
