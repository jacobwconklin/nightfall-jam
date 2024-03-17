using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float sprintSpeed = 30f;
    [SerializeField] private float sprintChargeDrainPerSecond = 2f;
    [SerializeField] private float mouseSensitivity = 1f;
    private PlayerStatus playerStatus;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        // TODO may need to add Time.deltaTime
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0)));

       
        // EventManager.EventManagerInstance.
    }

    void FixedUpdate()
    {
        // Allow player to sprint but cost charge
        float forwardMovement = Input.GetAxis("Vertical");
        float sidewaysMovement = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift) && (new Vector3(forwardMovement, 0, sidewaysMovement)).magnitude > 0.1) 
        {
            // Sprint
            rigidbody.MovePosition(transform.position + (transform.forward * forwardMovement * sprintSpeed * Time.fixedDeltaTime) +
                (transform.right * sidewaysMovement * sprintSpeed * Time.fixedDeltaTime));
            // Spends charge per second while sprinting
            playerStatus.spendCharge(sprintChargeDrainPerSecond * Time.fixedDeltaTime);
        } else
        {
            // Walk
            rigidbody.MovePosition(transform.position + (transform.forward * forwardMovement * moveSpeed * Time.fixedDeltaTime) + 
                (transform.right * sidewaysMovement * moveSpeed * Time.fixedDeltaTime));

        }

    }

    public float getMouseSensitivity()
    {
        return mouseSensitivity;

    }
}