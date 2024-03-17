using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : MonoBehaviour, IWeapInfo
{
    private Animator animator;

    // disables firing the weapon (happens when picking up a gun / reloading)
    private bool shootingDisabled;

    public void disableShooting ()
    {
        shootingDisabled = true;
    }

    public void enableShooting ()
    {
        shootingDisabled = false;
    }

    [Header("Ammo and the Max")]
    public int Mag = 60;
    public int Ammo;

    [Header("Time Between Shots")]
    public float timer = 0.2f;
    private float Rtimer;

    [Header("Damage It Will Deal")]
    public float Damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        Ammo = Mag;
        Rtimer = timer;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private bool HasShot = false;
    void Update()
    {

        if (HasShot)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            timer = Rtimer;
            HasShot = false;
        }

        if (Input.GetMouseButton(0) && !HasShot && Ammo != 0 && !shootingDisabled)
        {
            GameObject Bullet = Instantiate(Resources.Load("BulletHitBox", typeof(GameObject)), this.transform.position, this.transform.rotation) as GameObject;
            Bullet.GetComponent<IBullet>().SetDamage(Damage);
            Ammo--;
            HasShot = true;

            if (animator != null) animator.SetTrigger("Shooting");
        }
        else if (Input.GetMouseButton(0) && !HasShot && Ammo == 0 && !shootingDisabled)
        {
            // reload when ammo hits 0
            Reload();
        }
    }

    public void Reload()
    {
        Ammo = Mag;
        StartCoroutine("performReload");
    }

    IEnumerator performReload()
    {
        disableShooting();
        if (animator != null) animator.SetTrigger("Reload");
        // wait one and a half seconds then allow shooting
        yield return new WaitForSeconds(1.5f);
        enableShooting();
    }

    public string GetWeapType()
    {
        return "SMG";
    }

    public int GetAmmo()
    {
        return Ammo;
    }

    public int GetMag()
    {
        return Mag;
    }
    public void runPickupAnimation()
    {
        if (animator != null) animator.SetTrigger("Pickup");
    }
}
