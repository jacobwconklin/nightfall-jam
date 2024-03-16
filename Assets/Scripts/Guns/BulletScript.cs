using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour, IBullet
{
    [Header("The Start Velocity")]
    public float Speed = 70;

    [Header("Time Before it Vanishes")]
    public float timer = 10;
    //private float Rtimer;

    [Header("Damage")]
    public float Damage;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * Speed;

        //Rtimer = timer;
    }


    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            collision.gameObject.GetComponent<IDamage>().DealDamage(Damage);
        }
        Destroy(gameObject);
    }

    public void SetDamage(float dmg)
    {
        Damage = dmg;
    }

}
