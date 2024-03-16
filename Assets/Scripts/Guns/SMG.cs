using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : MonoBehaviour, IWeapInfo
{
    [Header("Ammo and the Max")]
    public int Mag = 60;
    public int Ammo;

    [Header("Time Between Shots")]
    public float timer = 0.2f;
    private float Rtimer;

    // Start is called before the first frame update
    void Start()
    {
        Ammo = Mag;
        Rtimer = timer;
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

        if (Input.GetMouseButton(0) && !HasShot && Ammo != 0)
        {
            Instantiate(Resources.Load("BulletHitBox", typeof(GameObject)), this.transform.position, this.transform.rotation);
            //Instantiate(Bullet, this.transform.position, this.transform.rotation);
            Ammo--;
            HasShot = true;
        }
    }

    public void Reload()
    {
        Ammo = Mag;
    }
}
