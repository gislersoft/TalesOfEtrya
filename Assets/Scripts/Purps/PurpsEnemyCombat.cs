using UnityEngine;
[RequireComponent(typeof(PurpsEnemyStats))]
public class PurpsEnemyCombat : CharacterCombat
{

    public override void Attack(CharacterStats targetStats)
    {
        int damage = myStats.damage.GetValue();
        targetStats.TakeDamage(damage);
    }
}
