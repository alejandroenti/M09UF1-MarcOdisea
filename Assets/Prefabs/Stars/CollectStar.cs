using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStar : MonoBehaviour
{
    private GameObject target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            // Añadimos punto al LevelManager
            Destroy(this.gameObject);
        }
    }
}
