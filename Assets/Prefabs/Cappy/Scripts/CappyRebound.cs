using UnityEngine;

public class CappyRebound : MonoBehaviour
{
    [Header("Cappy Rebound")]
    [SerializeField, Range(8f, 12f)] private float reboundForce;

    private GameObject target;
    private CappyMovement cappyMovementScript;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        cappyMovementScript = GetComponent<CappyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cappyMovementScript.GetFinalPosition() != target.transform.position && cappyMovementScript.GetCappyTimer() != 0.0f)
        {
            if (other.gameObject == target)
            {
                ReboundAction reboundScript = target.GetComponent<ReboundAction>();
                reboundScript.Rebound(transform.up, reboundForce);
                cappyMovementScript.SetIsComingBack();
            }
        }
    }
}
