using UnityEngine;

[CreateAssetMenu(fileName = "NoAttackSO", menuName = "Strategies/NoAttack")]
public class NoAttackSO : AttackStrategySO
{
    public override void Attack(Transform transform) { }
}
