using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Pistol pistol;
    [SerializeField] private Rifle rifle;
    [SerializeField] private SMG smg;
    private IWeapInfo selectedGun;


    // Start is called before the first frame update
    void Start()
    {
        // Start player with just a pistol
        pistol.gameObject.SetActive(true);
        rifle.gameObject.SetActive(false);
        smg.gameObject.SetActive(false);
        selectedGun = pistol;
    }

    // hide other guns besides the pistol. Simple as.
    public void pickUpPistol()
    {
        pistol.gameObject.SetActive(true);
        rifle.gameObject.SetActive(false);
        smg.gameObject.SetActive(false);
        selectedGun = pistol;
        pistol.runPickupAnimation();
    }

    // show just rifle
    public void pickUpRifle()
    {
        pistol.gameObject.SetActive(false);
        rifle.gameObject.SetActive(true);
        smg.gameObject.SetActive(false);
        selectedGun = rifle;
        rifle.runPickupAnimation();
    }

    // show just smg
    public void pickUpSmg()
    {
        pistol.gameObject.SetActive(false);
        rifle.gameObject.SetActive(false) ;
        smg.gameObject.SetActive(true);
        selectedGun = smg;
        smg.runPickupAnimation();
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            pickUpPistol();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X detected picking up rifle");
            pickUpRifle();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            pickUpSmg();
        }
    }

}
