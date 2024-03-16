using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private Light sun;
    private float fullDayDurationInSeconds = 120;
    private GameObject[] lights;
    private bool isDay = true;

    // Start is called before the first frame update
    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("LightSource");
    }

    // Update is called once per frame
    void Update()
    {


        // Rotate Sun
        // value = degrees per second
        float rotationDegreesaPerSecond = 360 / fullDayDurationInSeconds;
        sun.transform.Rotate(rotationDegreesaPerSecond * Time.deltaTime, 0, 0, Space.Self);

        // TODO turn lights on and off on timer ... 
    }

    private void FixedUpdate()
    {

    }

    public bool IsDay()
    {
        return isDay;
    }


}
