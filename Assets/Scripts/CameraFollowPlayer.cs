using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private float cameraRotationLimit = 50f;
    private PlayerMovement player;

    private void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {

        //transform.rotation = Quaternion.Euler(transform.rotation.x + (Input.GetAxis("Mouse Y") * player.getMouseSensitivity()),
        //    player.transform.rotation.y, 0);
        float verticalMovement = Input.GetAxis("Mouse Y");

        float currentRotationX = transform.localRotation.eulerAngles.x;
        if (currentRotationX > cameraRotationLimit)
        {
            currentRotationX = 360 - currentRotationX;
        }
        Debug.Log(currentRotationX);
        transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -1 * player.getMouseSensitivity(), 0f, 0f));
        //if (verticalMovement > 0.1 && currentRotationX > -1 * cameraRotationLimit )
        //{
        //    transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -1 * player.getMouseSensitivity(), 0f, 0f));
        //} else if (verticalMovement < -0.1 && currentRotationX < cameraRotationLimit)
        //{
        //    transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -1 * player.getMouseSensitivity(), 0f, 0f));
        //}
        // have position follow player
        // transform.position = new Vector3(player.transform.position.x + xAxis, player.transform.position.y + yAxis, player.transform.position.z + zAxis);
    }
}
