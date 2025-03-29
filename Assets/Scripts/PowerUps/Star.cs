using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private int _points = 2000;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        UnsecuredEventBus.TriggerStarCollected(transform.position, _points);
        StarPool.Instance.ReturnStar(this);
    }
}
