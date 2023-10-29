using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    public void Die()
    {
        GameManager._GAME_MANAGER.PlayerHasDied();
    }
}
