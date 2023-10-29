using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private GameObject target;
    private PlayerDie playerDieScript;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerDieScript = target.GetComponent<PlayerDie>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            playerDieScript.Die();
        }
    }
}
