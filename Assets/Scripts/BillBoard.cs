using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public GameObject Cam;

    void Start()
    {
        Cam = GameObject.FindObjectsOfType<Camera>()[0].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.forward = -Cam.transform.forward;
    }
}
