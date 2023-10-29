using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStar : MonoBehaviour
{
    private GameObject target;
    private AudioSource audioSource;

    private float timeToDestroy = 0.25f;
    private float timerToDestroy = 0f;
    private bool hasToBeDestroyed = false;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (hasToBeDestroyed)
        {
            timerToDestroy += Time.deltaTime;

            if (timerToDestroy >= timeToDestroy)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            // Añadimos punto al LevelManage
            Level_Manager._LEVEL_MANAGER.AppendStar();
            audioSource.Play();
            hasToBeDestroyed = true;
        }
    }
}
