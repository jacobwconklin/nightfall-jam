using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawn : MonoBehaviour, IEnemySetup
{
    public GameObject Enemy;

    public int NumEnemy = 10;

    public float Damage = 10;
    public GameObject Gun = null;
    public float MaxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        //Put yourself into the event manager
        SpawnEnemies(10);
    }

    public void SpawnEnemies(int Num)
    {
        for(int i = 0; i <= Num; i++)
        {
            Vector3 RandomPos = transform.position + new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
            Enemy.GetComponent<IEnemySetup>().Setup(Damage, null, MaxHealth);
            Instantiate(Enemy, RandomPos, Quaternion.identity);
        }
    }

    public void Setup(float Dmg, GameObject Gun, float Health)
    {
        Damage = Dmg;
        this.Gun = Gun;
        MaxHealth = Health;
    }

}

