using UnityEngine;

public class Level_Manager : MonoBehaviour
{
    public static Level_Manager _LEVEL_MANAGER;

    [Header("Stars count")]
    [SerializeField] private int numStars = 0;

    private int currentStars = 0;

    private void Awake()
    {
        _LEVEL_MANAGER = this;
    }

    private void Update()
    {
        if (currentStars >= numStars)
        {
            GameManager._GAME_MANAGER.LevelPassed();
        }
    }

    public void AppendStar() => currentStars++;
}
