using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    public void Die()
    {
        SceneManager.LoadScene(0);
    }
}
