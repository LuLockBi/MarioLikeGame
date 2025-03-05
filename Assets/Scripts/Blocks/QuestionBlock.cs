using UnityEngine;

public class QuestionBlock : Block
{    
    public Sprite DisabledSprite;
    public BoosterConfig BoosterConfig;


    protected override void OnHit()
    {
        if (DisabledSprite != null)
        {
            spriteRenderer.sprite = DisabledSprite;
        }

        SpawnRandomBooster();
    }

    private void SpawnRandomBooster()
    {
        if (BoosterConfig != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * 1f;
            BoosterConfig.GetRandomBooster(spawnPosition);
        }
    }
}
