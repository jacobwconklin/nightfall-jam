using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : PlayerEnemy
{
    [SerializeField] private ChargeDisplay chargeDisplay;
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private float chargePerSecond = 1;
    [SerializeField] private float maxCharge = 100;
    private float charge;
    private EventManager eventManager;
    private bool inShadow = false;

    private void Start()
    {
        eventManager = EventManager.EventManagerInstance;
        charge = 50;
        chargeDisplay.setCharge(charge);
    }

    // Inherits from PlayerEnemy abstract class, handles tracking player's charge and health.
    // communicates to UI any changes
    public override void DealDamage(float Damage)
    {
        Health -= Damage;
        healthDisplay.spenthealth(Health);
    }

    public override void SetHealth(float hp)
    {
        Health = hp;
        healthDisplay.spenthealth(Health);
    }

    private void spendCharge(float amount)
    {
        charge = charge - amount < 0 ? 0 : charge - amount;
        chargeDisplay.setCharge( Mathf.Floor(charge));
    }

    // Gain charge overtime
    public void Update()
    {
        if (eventManager.IsDay() && !inShadow)
        {
            // Regain charge
            charge = charge + chargePerSecond * Time.deltaTime > maxCharge ? maxCharge : charge + chargePerSecond * Time.deltaTime;
            chargeDisplay.setCharge(Mathf.Floor(charge));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Shadow"))
        {
            inShadow = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Shadow"))
        {
            inShadow = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shadow"))
        {
            inShadow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shadow"))
        {
            inShadow = false;
        }
    }

}
