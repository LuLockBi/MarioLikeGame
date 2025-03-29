using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private int _currentWorld = 1; 
    [SerializeField] private int _currentLevel = 1; 
    [SerializeField] private int _maxLevelsPerWorld = 3; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        UnsecuredEventBus.OnLevelCompleted += HandleLevelCompleted;
    }

    private void OnDisable()
    {
        UnsecuredEventBus.OnLevelCompleted -= HandleLevelCompleted;
    }

    private void HandleLevelCompleted()
    {
        StartCoroutine(CompleteLevelWithDelay());
    }
    private System.Collections.IEnumerator CompleteLevelWithDelay()
    {
        yield return new WaitForSeconds(3f);
        LoadNextLevel();
    }
    public void LoadNextLevel()
    {
        _currentLevel++;
        if (_currentLevel > _maxLevelsPerWorld)
        {
            _currentLevel = 1;
            _currentWorld++;
        }

        string nextSceneName = $"Level_{_currentWorld}-{_currentLevel}";
        if (SceneExists(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning($"—цена {nextSceneName} не найдена! ѕереход на начальный уровень.");
            _currentWorld = 1;
            _currentLevel = 1;
            SceneManager.LoadScene("Level_1-1");
        }
    }

    public string GetCurrentLevelName()
    {
        return $"Level_{_currentWorld}-{_currentLevel}";
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameInBuild = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameInBuild == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    public void LoadLevel(int world, int level)
    {
        _currentWorld = world;
        _currentLevel = level;
        string sceneName = $"Level_{world}-{level}";
        if (SceneExists(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"—цена {sceneName} не найдена");
        }
    }
}
