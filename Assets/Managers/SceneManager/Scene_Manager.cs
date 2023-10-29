using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager _SCENE_MANAGER;

    private void Awake()
    {
        if (_SCENE_MANAGER != null && _SCENE_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _SCENE_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
