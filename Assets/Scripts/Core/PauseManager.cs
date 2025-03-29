using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    public static PauseManager Instance { get; private set; }

    private bool _isPaused = false;

    

    [SerializeField] private Button continueButton;
    //private Button settingsButton;
    [SerializeField] private Button mainMenuButton;
    void Awake()
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

        if (_pauseMenu != null) _pauseMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

   
    public void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0f;
        if (_pauseMenu != null) _pauseMenu.SetActive(true);
        UnsecuredEventBus.TriggerPauseToggle();
    }

    public void ResumeGame()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        if (_pauseMenu != null) _pauseMenu.SetActive(false);
        UnsecuredEventBus.TriggerPauseToggle();
    }

    public bool IsPaused()
    {
        return _isPaused;
    }

    public void ReturnToMenu()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); 
    }
}
