using UnityEngine;

public class BreakableBlock : Block
{
    [SerializeField] private GameObject leftFragmentPrefab;  
    [SerializeField] private GameObject rightFragmentPrefab;
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

        GameObject left1 = Instantiate(leftFragmentPrefab, spawnPosition, Quaternion.identity);
        left1.GetComponent<BrickFragment>().Initialize(new Vector2(-_fragmentSpeed * 0.7f, _upwardForce * 0.8f));

        GameObject left2 = Instantiate(leftFragmentPrefab, spawnPosition, Quaternion.identity);
        left2.GetComponent<BrickFragment>().Initialize(new Vector2(-_fragmentSpeed, _upwardForce));

        GameObject right1 = Instantiate(rightFragmentPrefab, spawnPosition, Quaternion.identity);
        right1.GetComponent<BrickFragment>().Initialize(new Vector2(_fragmentSpeed * 0.7f, _upwardForce * 0.8f));

        GameObject right2 = Instantiate(rightFragmentPrefab, spawnPosition, Quaternion.identity);
        right2.GetComponent<BrickFragment>().Initialize(new Vector2(_fragmentSpeed, _upwardForce));
    }
}
