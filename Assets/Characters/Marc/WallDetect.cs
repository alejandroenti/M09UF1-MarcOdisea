using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetect : MonoBehaviour
{
    [Header("Set up RayCast")]
    [SerializeField] private float minWallDistanceTo = 1f;
    [SerializeField] private LayerMask maskActuation;

    private bool isOnWall;
    private Vector3 offsetY;

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, transform.forward * minWallDistanceTo, Color.red);
        RaycastHit hit;

        offsetY = transform.position;
        offsetY.y += 0.45f;
        
        if (Physics.Raycast(offsetY, transform.forward, out hit, minWallDistanceTo)) {
            if (hit.transform.tag == "Wall")
            {
                isOnWall = true;
            }
            else
            {
                isOnWall= false;
            }
        }
        else
        {
            isOnWall = false;
        }
        
    }

    public bool GetIsOnWall() => isOnWall;
}
