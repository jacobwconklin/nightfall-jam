using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float sprintSpeed = 1.5f;
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

    }

    void FixedUpdate()
    {
        // Allow player to sprint but cost charge
        float forwardMovement = Input.GetAxis("Vertical");
        float sidewaysMovement = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftShift) && (new Vector3(forwardMovement, 0, sidewaysMovement)).magnitude > 0.1) 
        {
            // Sprint
            transform.position = transform.position + (((transform.forward * Input.GetAxis("Vertical") * sprintSpeed) +
            (transform.right * Input.GetAxis("Horizontal") * moveSpeed)) * Time.fixedDeltaTime);
            // Spends charge per second while sprinting
            playerStatus.spendCharge(sprintChargeDrainPerSecond * Time.fixedDeltaTime);
        } else
        {
            // Walk
            transform.position = transform.position + (((transform.forward * Input.GetAxis("Vertical") * moveSpeed) +
                (transform.right * Input.GetAxis("Horizontal") * moveSpeed)) * Time.fixedDeltaTime);
        }
        
    }

    public float getMouseSensitivity()
    {
        return mouseSensitivity;
    }
}