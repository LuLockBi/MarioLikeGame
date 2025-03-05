using UnityEngine;

public class ShootAttack : MonoBehaviour
{
    private readonly float shootInterval = 3f;
    private float nextShootTime;
    private GameObject projectilePrefab; 

    public ShootAttack(GameObject projectilePrefab)
    {
        this.projectilePrefab = projectilePrefab;
    }

    public void Attack(Transform transform)
    {
        if (Time.time >= nextShootTime && projectilePrefab != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.right;
            GameObject.Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            nextShootTime = Time.time + shootInterval;
        }
    }
}
