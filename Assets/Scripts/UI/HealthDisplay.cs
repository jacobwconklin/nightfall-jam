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
    float health = 5f;
    public TextMeshProUGUI healthText;
    // Update is called once per frame
    void Update()
    {
        healthText.text = "health: " + health;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spenthealth(1);
        }
    }

    public void spenthealth(float amt)
    {
        health = amt;
    }
}
