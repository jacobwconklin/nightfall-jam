using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour, IWeapInfo
{
    private Animator animator;

    [Header("Ammo and the Max")]
    public int Mag = 32;
    public int Ammo;

    [Header("Time Between Shots")]
    public float timer = 0.7f;
    private float Rtimer;

    [Header("Damage It Will Deal")]
    public float Damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        Ammo = Mag;
        Rtimer = timer;
        animator = GetComponent<Animator>();
        animator.SetBool("Shooting", false);
    }

    // Update is called once per frame
    private bool HasShot = false;
    void Update()
    {
        if (Input.GetMouseButton(0) && !HasShot)
        {
            GameObject Bullet = Instantiate(Resources.Load("BulletHitBox", typeof(GameObject)), this.transform.position, this.transform.rotation) as GameObject;
            Bullet.GetComponent<IBullet>().SetDamage(Damage);
            //Instantiate(Bullet, this.transform.position, this.transform.rotation);
            Ammo--;
            HasShot = true;

            animator.SetBool("Shooting", true);
        }
        else
        {
            animator.SetBool("Shooting", false);
        }
    }

    void FixedUpdate()
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
    }

    public void Reload()
    {
        Ammo = Mag;
    }

    public string GetWeapType()
    {
        return "Rifle";
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
        animator.SetTrigger("Pickup");
    }
}
