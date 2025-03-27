using UnityEngine;

public class BreakableBlock : Block
{
    [SerializeField] private float _fragmentSpeed = 3f;       
    [SerializeField] private float _upwardForce = 4f;
    
    protected override void OnHit()
    {
        UnsecuredEventBus.TriggerBlockDestroyed(transform.position, 50);
        SpawnFragments();
        Destroy(gameObject);
    }

    private void SpawnFragments()
    {
        Vector3 spawnPosition = transform.position;

        BrickFragmentPool.Instance.GetLeftFragment(spawnPosition, new Vector2(-_fragmentSpeed * 0.7f, _upwardForce * 0.8f));

        BrickFragmentPool.Instance.GetLeftFragment(spawnPosition, new Vector2(-_fragmentSpeed, _upwardForce));

        BrickFragmentPool.Instance.GetRightFragment(spawnPosition, new Vector2(_fragmentSpeed * 0.7f, _upwardForce * 0.8f));

        BrickFragmentPool.Instance.GetRightFragment(spawnPosition, new Vector2(_fragmentSpeed, _upwardForce));

        
    }
}
