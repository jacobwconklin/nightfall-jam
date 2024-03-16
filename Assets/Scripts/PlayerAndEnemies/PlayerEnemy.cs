using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerEnemy: MonoBehaviour, IDamage
{

    
    public float MaxHealth = 10;
    public float Health = 0;

    public void DealDamage(float Damage)
    {
        Health -= Damage;
    }

    public float GetHealth()
    {
        return Health;
    }

    public float GetMaxHealth()
    {
        return MaxHealth;
    }

    public void SetHealth(float hp)
    {
        this.gameObject.tag = "Target";
        MaxHealth = hp;
        Health = hp;
    }

    public bool Dead()
    {
        return Health <= 0;
    }
}
