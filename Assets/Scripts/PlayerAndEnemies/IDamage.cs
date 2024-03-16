using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void DealDamage(float Damage);
    float GetHealth();
    void SetHealth(float hp);

    float GetMaxHealth();
}
