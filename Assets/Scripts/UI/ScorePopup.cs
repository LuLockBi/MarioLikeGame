using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _lifetime = 1f;
    private TextMeshPro _textMesh;
    private float _timer;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    public void Activate(int points, Vector3 position)
    {
        transform.position = position;
        _textMesh.text = $"+{points}";
        _timer = _lifetime;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.Translate(_moveSpeed * Time.deltaTime * Vector3.up);
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            ScorePopupPool.Instance.ReturnPopup(this);
        }

    }
}
