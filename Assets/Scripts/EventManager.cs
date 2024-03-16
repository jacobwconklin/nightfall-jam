using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private float fullDayDurationInSeconds = 120;
    private float timeInDayRemaining;
    private GameObject[] lights;
    private bool isDay = true;

    // Start is called before the first frame update
    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("LightSource");
        timeInDayRemaining = fullDayDurationInSeconds;
        // Set sun to sunrise exactly
        sun.transform.rotation = Quaternion.Euler(180, 0, -180);
        // Turn lights off
        foreach (GameObject light in lights)
        {
            light.GetComponent<Light>().intensity = 0;
            // .SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeInDayRemaining -= Time.deltaTime;

        if (timeInDayRemaining <= 0.0f)
        {
            // One full day is over, sun has risen again reset time in Day Remaining
            timeInDayRemaining = fullDayDurationInSeconds;
            isDay = true;
            // Can reset sun to sunrise exactly to avoid sun moving rotating at different speed and drifting
            sun.transform.rotation = Quaternion.Euler(180, 0, -180);
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
        }


        // Rotate Sun
        // value = degrees per second
        float rotationDegreesaPerSecond = 360 / fullDayDurationInSeconds;
        sun.transform.Rotate(rotationDegreesaPerSecond * Time.deltaTime, 0, 0, Space.Self);
    }

    private void FixedUpdate()
    {

    }

    public bool IsDay()
    {
        return isDay;
    }


}
