using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public interface IEnemySetup
{
    void Setup(float Damage, GameObject Gun, float Health);
}
public class EnemyAI : PlayerEnemy, IEnemySetup
{
    NavMeshAgent agent;
    public GameObject Player;
    public EnemyState State = EnemyState.Seeking;
    public float Damage = 10; //the event will set this

    //For shooting Enemies
    public bool HasGun = false;
    public GameObject Gun = null;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetHealth(100);
        if(Player == null)
        {
            State = EnemyState.Idle;
            Debug.LogError("There is no Player to track!");
        }

        if(Gun != null)
        {
            HasGun = true;
        }
    }

    private void Update()
    {
        if (Dead())
        {
            State = EnemyState.Dying;
        }

        switch (State)
        {
            case EnemyState.Seeking:
                agent.destination = Player.transform.position;

                if (Vector3.Distance(this.transform.position, Player.transform.position) < 2)
                {
                    Player.GetComponent<IDamage>().DealDamage(Damage);
                    State = EnemyState.Fight;
                }
                break;
            case EnemyState.Fight:

                //Play the fight animation Here

                if (Vector3.Distance(this.transform.position, Player.transform.position) <= 1)
                {
                    Player.GetComponent<IDamage>().DealDamage(Damage);
                   
                }
                else
                {
                    State = EnemyState.Seeking;
                }

                break;
            case EnemyState.Dying:
                //play dying Animation Here
                //couroutine the destroy game object here as well
                Destroy(gameObject);
                break;
            case EnemyState.Idle:
                //play Idle animation here
                break;
        }


        
    }

    public void Setup(float Dmg, GameObject Gun, float Health)
    {
        Damage = Dmg;
        this.Gun = Gun;
        MaxHealth = Health;
    }

    public enum EnemyState
    {
        Seeking,
        Fight,
        Shoot,
        Dying,
        Idle,
        OverHeating,
        Flee
    }
}
