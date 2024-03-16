using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Total amt of health
    float charge = 100f;
    public TextMeshProUGUI chargeText;
    // Update is called once per frame
    void Update()
    {
        chargeText.text = "Charge: " + charge;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spentCharge(5);
        }

        /*if (isDay())
        {
            addCharge(1);
        }*/
    }

    public void spentCharge(float amt)
    {
        if (charge - amt <= 0)
        {
            charge = 0;
        }
        else
        {
            charge -= amt;
        }
    }

    public void addCharge(float amt)
    {
        if (charge + amt >= 100)
        {
            charge = 100;
        }
        else
        {
            charge += amt;
        }
    }
}
