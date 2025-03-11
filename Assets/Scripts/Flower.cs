using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private int _points = 1000;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        UnsecuredEventBus.TriggerFlowerCollected(transform.position, _points);
        FlowerPool.Instance.ReturnFlower(this);
    }
}
