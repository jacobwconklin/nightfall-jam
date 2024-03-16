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
            setCharge(5);
        }
    }

    public void setCharge(float amount)
    {
        charge = amount;
    }
}
