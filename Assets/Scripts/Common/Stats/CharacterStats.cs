using UnityEngine;
using UnityEngine.Events;
public class CharacterStats : MonoBehaviour {

    //Only add a stat using Class Stats if the stat could be modified. Otherwise, add as integer or float
    public int maxHealth = 100;
    public int CurrentHealth { get; set; } // <- This means the currentHealth value can be obtained by any script outside this one, but
                                                   //   can ONLY be modified inside this script
    public Stat damage;
    public Stat armor; //Armor acts as health boost, however, no armor equipment (visually) is taken into account.

    public UnityEvent OnDie; //This is a unity event, similar to delegates but with nicer ui
    void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
         
        CurrentHealth -= damage;

        CurrentHealth = (CurrentHealth < 0) ? 0 : CurrentHealth;

        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Character died");
        if(OnDie != null){
            OnDie.Invoke();
        }
    }

}
