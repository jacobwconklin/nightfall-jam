using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawn : MonoBehaviour, IEnemySetup
{
    public GameObject Enemy;

    public int NumEnemy = 10;

    public float Damage = 10;
    public float MaxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        //Put yourself into the event manager
        //SpawnEnemies(10);
    }

    public void SpawnEnemies(int Num)
    {
        for(int i = 0; i < Num; i++)
        {
            Vector3 RandomPos = transform.position + new Vector3(Random.Range(0, this.transform.localScale.x), 0, Random.Range(0, this.transform.localScale.z));
            Enemy.GetComponent<IEnemySetup>().Setup(Damage, PickGun(), MaxHealth); // call a chance table right here
            Instantiate(Enemy, RandomPos, Quaternion.identity);
        }
    }

    //Always make gun null!
    public void Setup(float Dmg, int Gun, float Health)
    {
        Damage = Dmg;
        MaxHealth = Health;
    }

    public int PickGun()
    {
        float RandomValue = Random.value;

        float[] probabilities = { 0.7f, 0.5f, 0.10f, 0.15f };
        //string[] Weapon = { null, "Rifle", "SMG", "Pistol" };

        //Debug.Log(RandomValue);
        float cumulativeProbability = 0;
        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (RandomValue <= cumulativeProbability)
            {
                return i;
            }
        }

        //This should never return
        return 0;
    }

}

