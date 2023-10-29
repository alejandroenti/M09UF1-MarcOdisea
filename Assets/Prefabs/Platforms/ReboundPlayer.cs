using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundPlayer : MonoBehaviour
{
    [Header("Rebound Force Platforms")]
    [SerializeField, Range(8f, 12f)] private float reboundForce;

    private GameObject target;
    ReboundAction reboundScript;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        reboundScript = target.GetComponent<ReboundAction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            reboundScript.Rebound(transform.up, reboundForce);
        }
    }
}
