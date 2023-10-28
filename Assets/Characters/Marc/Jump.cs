using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Jump : MonoBehaviour
{
    private Vector3 finalVelocity = Vector3.zero;
    private float velocityXZ = 5f;
    private float gravity = 20f;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
    }
}
