using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _GAME_MANAGER;


    private void Awake()
    {
        if (_GAME_MANAGER != null && _GAME_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _GAME_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LevelPassed()
    {
        Scene_Manager._SCENE_MANAGER.ExitGame();
    }

    public void PlayerHasDied()
    {
        Scene_Manager._SCENE_MANAGER.ReloadLevel();
    }
}
