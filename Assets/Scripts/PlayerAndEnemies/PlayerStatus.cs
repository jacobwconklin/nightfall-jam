using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : PlayerEnemy
{
    [SerializeField] private ChargeDisplay chargeDisplay;
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private float chargePerSecond = 1;
    [SerializeField] private float maxCharge = 100;
    private float charge = maxCharge;
    private EventManager eventManager;
    private bool inShadow = false;

    private void Start()
    {
        eventManager = EventManager.EventManagerInstance;
    }

    // Inherits from PlayerEnemy abstract class, handles tracking player's charge and health.
    // communicates to UI any changes
    public override void DealDamage(float Damage)
    {
        Health -= Damage;
        // healthDisplay.setHealth(Health);
    }

    public override void SetHealth(float hp)
    {
        Health = hp;
        // healthDisplay.setHealth(Health);
    }

    private void spendCharge(float amount)
    {
        charge = charge - amount < 0 ? 0 : charge - amount;
        // chargeDisplay.setCharge(charge);
    }

    // Gain charge overtime
    public void Update()
    {
        if (eventManager.IsDay() && !inShadow)
        {
            // Regain charge
            charge = charge + 1 * Time.deltaTime > maxCharge ? maxCharge : charge + 1 * Time.deltaTime;
            // chargeDisplay.setCharge(charge);
        }
    }

}
