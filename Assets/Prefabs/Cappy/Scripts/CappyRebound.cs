using UnityEngine;

public class CappyRebound : MonoBehaviour
{
    [Header("Cappy Rebound")]
    [SerializeField, Range(8f, 12f)] private float reboundForce;

    private GameObject target;
    private CappyMovement cappyMovementScript;
    ReboundAction reboundScript;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        cappyMovementScript = GetComponent<CappyMovement>();
        reboundScript = target.GetComponent<ReboundAction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cappyMovementScript.GetFinalPosition() != target.transform.position && cappyMovementScript.GetCappyTimer() != 0.0f)
        {
            if (other.gameObject == target)
            {
                reboundScript.Rebound(transform.up, reboundForce);
                cappyMovementScript.SetIsComingBack();
            }
        }
    }
}
