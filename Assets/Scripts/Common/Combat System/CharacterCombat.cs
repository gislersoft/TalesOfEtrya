using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    protected CharacterStats myStats;

    protected virtual void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }


	public virtual void Attack(CharacterStats targetStats)
    {
        int damage = myStats.damage.GetValue();
        targetStats.TakeDamage(damage);
    }
}
