using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is just a debug script not for final build
public class Evoker : MonoBehaviour
{
    public EnemySpawn[] Spawners;
    public bool spawn = false;

    public int NumSpawn = 1;
    public float Damage = 10;
    public GameObject Gun = null;
    public float MaxHealth = 100;


    void Update()
    {
        if(spawn)
        {
            for(int i = 0; i < Spawners.Length; i++)
            {
                Spawners[i].Setup(Damage, 0, MaxHealth);
                Spawners[i].SpawnEnemies(NumSpawn);
            }
            spawn = false;
        }
    }
}
