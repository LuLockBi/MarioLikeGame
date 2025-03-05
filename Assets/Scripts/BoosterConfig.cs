using UnityEngine;

[CreateAssetMenu(fileName = "BoosterConfig", menuName = "Configs/PowerUpConfig")]
public class BoosterConfig : ScriptableObject
{
    [System.Serializable]
    public struct BoosterChance
    {
        public GameObject BoosterPrefab;
        [Range(0f, 100f)]
        public float Chance;
    }

    public BoosterChance[] Boosters;

    public GameObject GetRandomBooster(Vector3 position)
    {
        float roll = Random.Range(0f, 100f);
        float cumulativeChance = 0f;

        foreach (BoosterChance boost in Boosters)
        {
            cumulativeChance += boost.Chance;
            if (roll <= cumulativeChance && boost.BoosterPrefab != null)
            {
                if (boost.BoosterPrefab.TryGetComponent<Coin>(out _))
                {
                    return CoinPool.Instance.GetCoin(position).gameObject;
                }
                else if (boost.BoosterPrefab.TryGetComponent<Mushroom>(out _))
                {
                    return MushroomPool.Instance.GetMushroom(position).gameObject;
                }
                //другие типы бонусов 
                else
                {
                    return Object.Instantiate(boost.BoosterPrefab, position, Quaternion.identity);
                }
            }
        }
        return null;
    }
}
