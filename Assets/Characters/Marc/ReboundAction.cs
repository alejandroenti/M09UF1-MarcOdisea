using UnityEngine;

public class ReboundAction : MonoBehaviour
{
    private Vector3 finalVelocity = Vector3.zero;

    private CharacterController controller;
    private Jump jumpScript;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        jumpScript = GetComponent<Jump>();
    }

    public void Rebound(Vector3 jumpDirection, float jumpForce)
    {
        finalVelocity = jumpDirection * jumpForce;
        jumpScript.SetFinalVelocity(finalVelocity);
    }
}
