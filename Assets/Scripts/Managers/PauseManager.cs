using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PauseManager : MonoBehaviour
{
    [SerializeField] private AudioClip _pauseSound;
    [SerializeField] private AudioClip _resumeSound;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField][Range(0f, 1f)] private float _soundVolume = 1f;

    public static PauseManager Instance { get; private set; }

    private bool _isPaused = false;

    private Button continueButton;
    //private Button settingsButton;
    private Button mainMenuButton;
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
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindUIElements();
    }

    private void BindUIElements()
    {
        GameObject uiContainer = GameObject.Find("===UI===");
        if (uiContainer == null)
        {
            Debug.LogError("��������� �� ������ � �����");
            return;
        }

        Transform gameUITransform = uiContainer.transform.Find("GameUI");
        if (gameUITransform == null)
        {
            Debug.LogError("GameUI �� ������ � ���������/UI");
            return;
        }

        _pauseMenu = gameUITransform.Find("PauseMenu")?.gameObject;
        if (_pauseMenu == null)
        {
            Debug.LogError("PauseMenu �� ������ � GameUI");
        }

        Transform containerTransform = _pauseMenu.transform.Find("Container");
        if (containerTransform != null)
        {
            continueButton = containerTransform.Find("ResumeButton")?.GetComponent<Button>();
            //settingsButton = containerTransform.Find("SettingsButton")?.GetComponent<Button>();
            mainMenuButton = containerTransform.Find("MainMenuButton")?.GetComponent<Button>();

            if (continueButton != null)
            {
                continueButton.onClick.RemoveAllListeners();
                continueButton.onClick.AddListener(ResumeGame);
            }
            //if (settingsButton != null)
            //{
            //    settingsButton.onClick.RemoveAllListeners();
            //    settingsButton.onClick.AddListener(ReturnToMenu);
            //}
            if (mainMenuButton != null)
            {
                mainMenuButton.onClick.RemoveAllListeners();
                mainMenuButton.onClick.AddListener(ReturnToMenu);
            }

            if (continueButton == null) Debug.LogError("ResumeButton �� ������");
            //if (settingsButton == null) Debug.LogError("SettingsButton �� ������");
            if (mainMenuButton == null) Debug.LogError("MainMenuButton �� ������");
        }
        else
        {
            Debug.LogError("Container �� ������ � PauseMenu");
        }

        if(_pauseMenu != null)
        {
            _pauseMenu.SetActive(false);
        }
    }
    public void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0f;
        if (_pauseMenu != null) _pauseMenu.SetActive(true);
        AudioManager.Instance.PlaySound(_pauseSound, _soundVolume);
    }

    public void ResumeGame()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        if (_pauseMenu != null) _pauseMenu.SetActive(false);
        AudioManager.Instance.PlaySound(_resumeSound, _soundVolume);
    }

    public bool IsPaused()
    {
        return _isPaused;
    }

    public void ReturnToMenu()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // ������ ����� MainMenu
        //Debug.Log("����� �������� ������ ����� � �������� �������");
    }
}
