using UnityEngine;

public class CappyMovement : MonoBehaviour
{
    [Header("Set up Cappy Movement")]
    [SerializeField] private float movementMaxSpped = 4f;
    [SerializeField] private float movementAcceleration = 3f;
    [SerializeField] private float movementDistance = 10f;
    [SerializeField] private float angularMaxSpeed = 5f;
    [SerializeField] private float angularAcceleration = 3f;

    [Header("Cappy Come Back")]
    [SerializeField] private GameObject target;
    [SerializeField] private float cappyTimeComeBack = 10f;

    private float movementSpeed = 0.0f;
    private float angularSpeed = 0.0f;

    private Vector3 finalPosition = Vector3.zero;
    private Vector3 finalMovementSpeed = Vector3.zero;
    private Vector3 finalAngularSpeed = Vector3.zero;
    private float finalMinDistance = 0.8f;

    private Vector3 newPosition = Vector3.zero;
    private Vector3 newRotation = Vector3.zero;

    private float cappyTimer = 0.0f;
    private bool isComingBack = false;

    private ThrowCappy throwCappyScript;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        throwCappyScript = target.GetComponent<ThrowCappy>();

        finalPosition = transform.position + (transform.forward * movementDistance);
    }

    private void Update()
    {

        if (isComingBack)
        {
            finalPosition = target.transform.position;
        }

        Vector3 direction = finalPosition - transform.position;

        CalculateRotation();

        if (direction.magnitude <= finalMinDistance)
        {
            if (finalPosition == target.transform.position)
            {
                throwCappyScript.SetCanThrowCappy(true);
                Destroy(this.gameObject);
            }
            else
            {
                cappyTimer += Time.deltaTime;

                if (cappyTimer >= cappyTimeComeBack)
                {
                    isComingBack = true;
                    movementSpeed = 0f;
                }
            }
            
        }
        else
        {
            CalculateMovement(direction);
        }

    }

    public Vector3 GetFinalPosition() => finalPosition;
    public void SetIsComingBack() => isComingBack = true;
    public float GetCappyTimer() => cappyTimer;

    private void CalculateRotation()
    {
        if (isComingBack)
        {
            angularSpeed -= angularAcceleration * Time.deltaTime;
        }
        else
        {
            angularSpeed += angularAcceleration * Time.deltaTime;
        }

        angularSpeed = Mathf.Clamp(angularSpeed, 0f, angularMaxSpeed);
        finalAngularSpeed.y = transform.up.y * angularSpeed;

        transform.Rotate(finalAngularSpeed);
    }

    private void CalculateMovement(Vector3 direction)
    {
        direction.Normalize();

        movementSpeed += movementAcceleration * Time.deltaTime;

        movementSpeed = Mathf.Clamp(movementSpeed, 0f, movementMaxSpped);

        finalMovementSpeed.x = direction.x * movementSpeed;
        finalMovementSpeed.z = direction.z * movementSpeed;

        transform.position = Vector3.Lerp(transform.position + finalMovementSpeed, finalPosition, movementSpeed * Time.deltaTime);
    }
}
