using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float mouseSensitivity = 1f;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // TODO add Time.deltaTime
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0)));

    }

    void FixedUpdate()
    {
        transform.position = transform.position + (((transform.forward * Input.GetAxis("Vertical") * moveSpeed) +
            (transform.right * Input.GetAxis("Horizontal") * moveSpeed)) * Time.fixedDeltaTime);
    }
}