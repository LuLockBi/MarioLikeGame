using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerState health = collision.GetComponent<PlayerState>();

            if (health != null)
                health.DieInstantly();
        }
    }
}
