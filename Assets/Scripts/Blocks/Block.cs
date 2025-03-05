using UnityEngine;

public abstract class Block : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer; 
    protected bool isHit = false;           

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Hit()
    {
        if (isHit) return; 
        isHit = true;      
        OnHit();           
    }

    protected abstract void OnHit();
}
