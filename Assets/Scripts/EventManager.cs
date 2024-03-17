using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private GameObject sun;
    [SerializeField] private float fullDayDurationInSeconds = 120;
    private float timeInDayRemaining;
    private GameObject[] lights;
    private bool isDay = true;
    private int nightCount = 0;
    private bool gameEnded = false;
    private EnemySpawn[] enemySpawners;

    [Header("Spawn node settings")]
    public int baseSpawnPerNodeNumber = 4;
    public int spawnIncreaseRatePerNode = 1;
    public float baseDamage = 10;
    public float damageIncrease = 0.5f;
    public GameObject Gun = null;
    public float baseHealth = 100;
    public float healthIncrease = 5f;
    private bool performedSpawn = false;

    public static EventManager EventManagerInstance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (EventManagerInstance != null && EventManagerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            EventManagerInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Should get called when a new scene starts
        startGame();
    }

    public void startGame()
    {
        nightCount = 0;
        lights = GameObject.FindGameObjectsWithTag("LightSource");
        timeInDayRemaining = fullDayDurationInSeconds;
        // Set sun to sunrise exactly
        sun = GameObject.FindGameObjectWithTag("Sun");
        if (sun != null) sun.transform.rotation = Quaternion.Euler(180, 0, -180);
        // Turn lights off
        foreach (GameObject light in lights)
        {
            light.GetComponent<Light>().intensity = 0;
            // .SetActive(false);
        }
        enemySpawners = GameObject.FindObjectsOfType<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        timeInDayRemaining -= Time.deltaTime;

        if (timeInDayRemaining <= 0.0f && !gameEnded)
        {
            performedSpawn = false;
            // One full day is over, sun has risen again reset time in Day Remaining
            nightCount++;
            timeInDayRemaining = fullDayDurationInSeconds;
            isDay = true;
            // Can reset sun to sunrise exactly to avoid sun moving rotating at different speed and drifting
            if (sun != null) sun.transform.rotation = Quaternion.Euler(180, 0, -180);
            // Turn lights off
            foreach (GameObject light in lights)
            {
                light.GetComponent<Light>().intensity = 0; 
                // .SetActive(false);
            }

        } else if (timeInDayRemaining < fullDayDurationInSeconds / 2)
        {

            // Sunset is happening, it is no longer daytime
            isDay = false;
            // Turn lights on
            foreach (GameObject light in lights)
            {
                light.SetActive(true);
                light.GetComponent<Light>().intensity = 1;
            }

            // spawn enemies on enemy spawn timer + based on nights survived
            if (!performedSpawn)
            {
                foreach (EnemySpawn enempySpawn in enemySpawners)
                {
                    enempySpawn.Setup(Mathf.Floor(baseDamage + nightCount * damageIncrease), 0, baseHealth + nightCount * healthIncrease);
                    enempySpawn.SpawnEnemies( (int) (baseSpawnPerNodeNumber + nightCount * spawnIncreaseRatePerNode));
                }

                performedSpawn = true;

            }

        }


        // Rotate Sun
        // value = degrees per second
        float rotationDegreesaPerSecond = 360 / fullDayDurationInSeconds;
        if (sun != null) sun.transform.Rotate(rotationDegreesaPerSecond * Time.deltaTime, 0, 0, Space.Self);
    }

    private void FixedUpdate()
    {

    }

    public void endGame()
    {
        StartCoroutine("goToGameOver");
    }

    IEnumerator goToGameOver()
    {
        // takes to game over screen after x seconds
        yield return new WaitForSeconds(3f);
        // TODO move to game over screen
    }

    public bool IsDay()
    {
        return isDay;
    }

    public int getNightCount()
    {
        return nightCount;
    }
}
