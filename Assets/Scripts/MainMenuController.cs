using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1); // Индекс игровой сцены в Build Settings
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // временно (для редактора)
#endif
    }

    public void OpenSettings()
    {
        Debug.Log("Настройки пока не реализованы!");
    }
}
