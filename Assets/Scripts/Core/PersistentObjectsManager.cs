using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObjectsManager : MonoBehaviour
{
    public static PersistentObjectsManager Instance { get; private set; }

    [SerializeField] private List<GameObject> _persistentObjects = new List<GameObject>();

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void RegisterPersistentObject(GameObject obj)
    {
        if (!_persistentObjects.Contains(obj))
        {
            _persistentObjects.Add(obj);
            DontDestroyOnLoad(obj); 
        }
    }
    public void DestroyAllPersistentObjects()
    {
        foreach (var obj in _persistentObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        _persistentObjects.Clear();
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu_Snene")
        {
            DestroyAllPersistentObjects();
            Destroy(gameObject);
        }
    }
}
