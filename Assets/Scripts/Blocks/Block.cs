using UnityEngine;

public abstract class Block : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer; 
    protected bool _isHit = false;           

    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Hit()
    {
        if (_isHit) return; 
        _isHit = true;      
        OnHit();           
    }

    protected abstract void OnHit();
}
