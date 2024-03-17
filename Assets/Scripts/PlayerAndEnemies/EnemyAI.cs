using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine.AI;
using UnityEngine;

public interface IEnemySetup
{
    void Setup(float Damage, int Gun, float Health);
}
public class EnemyAI : PlayerEnemy, IEnemySetup
{
    NavMeshAgent agent;
    public GameObject Player;
    public EnemyState State = EnemyState.Seeking;
    public float Damage = 10; //the event will set this
    public float GunDamage = 10; //the event will set this

    //For shooting Enemies
    public bool HasGun = false;
    public int Gun = 0; //Gun will be the index of the array
    public int Ammo;
    public float GDPS = 2;
    private float RGDPS = 2;

    private Animator anim;

    public float DPS = 2; //Damage Per Second
    private float RDPS; //Anything with R means to remember

    public float MeleeRange = 4;
    public float ShootingRange = 11;

    public float Speed = 3.5f;

    private bool StopAll = false; //to stop the death spam

    [Header("Drag Guns Here")]
    [SerializeField] private GameObject[] Guns; //NEEDS TO BE RIFLE SMG PISTOL
    //[SerializeField] private GameObject rifle;
    //[SerializeField] private GameObject smg;
    //[SerializeField] private GameObject pistol;

    [SerializeField] private GameObject BulletAttach;
    
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        RDPS = DPS;
        agent.speed = Speed;

        SetHealth(100);
        if(Player == null)
        {
            State = EnemyState.Idle;
            Debug.LogError("There is no Player to track!");
            
        }

        Guns[0].SetActive(false);
        Guns[1].SetActive(false);
        Guns[2].SetActive(false);
        if (Gun != 0)
        {
            Gun--; //because the setup returned a value a little higher. 
            HasGun = true;
            Guns[Gun].SetActive(true);
            anim.SetBool("HasGun", true);
            //This is why I wanted the gun script to be used by both player and npc using Unity event instead, so I dont have to do this ififfifif.
            if (Gun == 0)
            {
                Ammo = 32;
                GunDamage = 10;
                GDPS = 0.7f;
                RGDPS = 0.7f;
            }

            if (Gun == 1)
            {;
                Ammo = 60;
                GunDamage = 5;
                GDPS = 0.2f;
                RGDPS = 0.2f;
            }

            if (Gun == 2)
            {
                Ammo = 16;
                GunDamage = 2;
                GDPS = 1;
                RGDPS = 1f;
            }
        }

        anim.SetTrigger("Moving");

        

    }

    private void Update()
    {
        if (Dead()) //Add to also check if day
        {
            
            State = EnemyState.Dying;
        }

        if(Player.GetComponent<IDamage>() != null && Player.GetComponent<IDamage>().GetHealth() == 0)
        {
            anim.SetBool("PlayerIsDead", true);
            State = EnemyState.Idle;
        }

        switch (State)
        {
            case EnemyState.Seeking:

                agent.destination = Player.transform.position;

                if(Vector2DDist(this.transform.position, Player.transform.position) <= ShootingRange && HasGun && CheckForObstruction())
                {
                    State = EnemyState.Shoot;
                    agent.isStopped = true;
                    anim.ResetTrigger("Moving");
                    anim.SetTrigger("Shoot");
                    break;
                }

                if (Vector2DDist(this.transform.position, Player.transform.position) <= MeleeRange)
                {
                    anim.SetBool("IsAttacking", true);
                    State = EnemyState.Fight;
                    agent.isStopped = true;
                }
                break;
            case EnemyState.Fight:
                
                //Play the fight animation Here
                if(DPS == RDPS)
                {
                    anim.SetTrigger("Attack");

                    if (Vector2DDist(this.transform.position, Player.transform.position) <= MeleeRange && Player.GetComponent<IDamage>() != null)
                    {
                        Player.GetComponent<IDamage>().DealDamage(Damage);
                    }
                    else if (Vector2DDist(this.transform.position, Player.transform.position) > MeleeRange)
                    {
                        State = EnemyState.Seeking;
                        agent.isStopped = false;
                        DPS = RDPS;
                        anim.ResetTrigger("Attack");
                        anim.SetTrigger("Moving");
                        anim.SetBool("IsAttacking", false);

                        break;
                    }
                }

                DamagePerSec();
              
                break;
            case EnemyState.Dying:

                if(!StopAll)
                {
                    anim.ResetTrigger("Attack");
                    anim.ResetTrigger("Moving");
                    anim.SetTrigger("OverHeat");
                    agent.isStopped = true;
                    StopAll = true;
                    StartCoroutine(TrueDeath(5));
                }
                
                //if it had the gun then make it drop it here
                break;
            case EnemyState.Shoot:

                transform.rotation = Quaternion.LookRotation((Player.transform.position - transform.position).normalized);

                
                if (GDPS == RGDPS && Ammo != 0)
                {

                    if (Vector2DDist(this.transform.position, Player.transform.position) <= ShootingRange && CheckForObstruction())
                    {
                        Shoot();
                    }
                    else if (Vector2DDist(this.transform.position, Player.transform.position) > ShootingRange || !CheckForObstruction())
                    {
                        State = EnemyState.Seeking;
                        agent.isStopped = false;
                        GDPS = RGDPS;
                        anim.ResetTrigger("Shoot");
                        anim.SetTrigger("Moving");
                        break;
                    }
                }

                if(Ammo == 0)
                {
                    HasGun = false;
                    State = EnemyState.Seeking;
                    agent.isStopped = false;
                    Guns[Gun].SetActive(false);
                    anim.ResetTrigger("Shoot");
                    anim.SetBool("HasGun", false);

                    //AND MAKE IT SPAWN THE GUN HERE
                    break;
                }

                GDamagePerSec();

                break;
            case EnemyState.Idle:
                anim.SetTrigger("Idle");
                agent.isStopped = true;
                break;
        }


        
    }

    //will return true if player is unobstructed
    private bool CheckForObstruction()
    {
        RaycastHit hit;
        Vector3 line = Player.transform.position - transform.position;
        Physics.Raycast(this.transform.position, Player.transform.position - transform.position, out hit, line.magnitude);

        
        if(Physics.Raycast(this.transform.position, Player.transform.position - transform.position, out hit, line.magnitude))
        {
            if(hit.collider.gameObject == Player.gameObject)
            {
                return true;
            }
        }

        return false;
    }

    private void Shoot()
    {
        GameObject Bullet = Instantiate(Resources.Load("BulletHitBox", typeof(GameObject)), BulletAttach.transform.position, BulletAttach.transform.rotation) as GameObject;
        Bullet.GetComponent<IBullet>().SetDamage(Damage);

        Ammo--;

        Guns[Gun].GetComponent<Animator>().SetTrigger("Shooting");
    }
    //unused
    private IEnumerator ChangeState(EnemyState state, float time)
    {
        yield return new WaitForSeconds(time);
        State = state;
    }

    private IEnumerator TrueDeath(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void Setup(float Dmg, int Gun, float Health)
    {
        Damage = Dmg;
        this.Gun = Gun;
        MaxHealth = Health;
    }

    private float Vector2DDist(Vector3 Me, Vector3 player)
    {
        float x = Me.x - player.x;
        float z = Me.z - player.z;
        return Mathf.Sqrt((x * x) + (z * z));
    }

    private void DamagePerSec()
    {
        DPS -= Time.deltaTime;
        if (DPS <= 0)
        {
            DPS = RDPS;
        }
    }

    private void GDamagePerSec()
    {
        GDPS -= Time.deltaTime;
        if (GDPS <= 0)
        {
            GDPS = RGDPS;
        }
    }

    public enum EnemyState
    {
        Seeking,
        Fight,
        Shoot,
        Dying,
        Idle,
        Flee //got too complicated
    }
}
