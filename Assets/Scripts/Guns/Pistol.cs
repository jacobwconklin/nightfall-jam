using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//varname.GetComponent<IWeapInfo>().Reload()
//Bullet = Instantiate(Resources.Load("Bullet", typeof(GameObject))) as GameObject;
public class Pistol : MonoBehaviour, IWeapInfo
{

    [Header("Ammo and the Max")]
    public int Mag = 16;
    public int Ammo;

    [Header("Time Between Shots")]
    public float timer = 1;
    private float Rtimer;

    [Header("Damage It Will Deal")]
    public float Damage = 2;

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
        if(Input.GetMouseButtonDown(0) && !HasShot && Ammo <= 0)
        {
            GameObject Bullet = Instantiate(Resources.Load("BulletHitBox", typeof(GameObject)), this.transform.position, this.transform.rotation) as GameObject;
            Bullet.GetComponent<IBullet>().SetDamage(Damage);

            Ammo--;
            HasShot = true;
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
        return "Pistol";
    }

    public int GetAmmo()
    {
        return Ammo;
    }

    public int GetMag()
    {
        return Mag;
    }
}
