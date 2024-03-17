using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NightDisplay : MonoBehaviour
{
    private float currentNight = 0;

    // Start is called before the first frame update
    float nights = 0f;
    public TextMeshProUGUI nightsText;
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        nightsText.text = "Night's survived: " + GameController.GameControllerInstance.getNightCount();
    }
}
