using UnityEngine;

public class UILoader : MonoBehaviour
{
    private void Start()
    {
        GameObject uiRoot = GameObject.Find("GameUI");
        if (uiRoot != null)
        {
            UnsecuredEventBus.TriggerUILoaded(uiRoot);
        }
        else
        {
            Debug.LogError("GameUI не найден в сцене!");
        }
    }
}
