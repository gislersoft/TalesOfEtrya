using UnityEngine;

[RequireComponent (typeof(PlayerStats))]
public class PlayerCombat : CharacterCombat {
    public PlayerStats playerStats;
    public float cooldown = 0f;

    protected override void Start()
    {
        playerStats = GetComponent<PlayerStats>();   
    }

    public void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public override void Attack(CharacterStats targetStats)
    {
        int damage = playerStats.damage.GetValue();
        targetStats.TakeDamage(damage);

    }
}
