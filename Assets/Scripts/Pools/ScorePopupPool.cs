using Mono.Cecil.Cil;
using System.Collections.Generic;
using UnityEngine;

public class ScorePopupPool : MonoBehaviour
{
    public static ScorePopupPool Instance { get; private set; }
    [SerializeField] private ScorePopup _popupPrefab;
    [SerializeField] private int _initSize = 5;
    [SerializeField] private bool _autoExpand = true;
    private ObjectPool<ScorePopup> _pool;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _pool = new ObjectPool<ScorePopup>(_popupPrefab, _initSize, transform, _autoExpand,
                onGet: popup => popup.Activate(0, Vector3.zero),
                onRelease: popup => popup.gameObject.SetActive(false));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public ScorePopup GetPopup(int points, Vector3 position)
    {
        ScorePopup popup = _pool.GetFreeElement(position);
        popup.Activate(points, position);
        return popup;
    }

    public void ReturnPopup(ScorePopup scorePopup)
    {
        _pool.ReturnToPool(scorePopup);
    }
}
