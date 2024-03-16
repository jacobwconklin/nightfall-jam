using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ChargeDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //Total amt of solar charge
    float charge = 100f;
    public TextMeshProUGUI chargeText;
    // Update is called once per frame
    void Update()
    {
        chargeText.text = "Charge : " + charge;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spentCharge(5);
        }
        //Replace this if with when it is daylight;
        /*if ()
        {
            addCharge(1);
        }*/
    }

    public void spentCharge(float amount)
    {
        if (charge - amount <= 0)
        {
            charge = 0;
        }
        else
        {
            charge -= amount;
        }
    }

    public void addCharge(float amount)
    {
        if (charge + amount > 100)
        {
            charge = 100;
        }
        else
        {
            charge += amount;
        }
    }
}
