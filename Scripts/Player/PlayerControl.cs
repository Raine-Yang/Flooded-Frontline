using OD.Effect.HDRP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;

// different states for the player
public enum PlayerState
{
    normal,
    sense
}


public class PlayerControl : MonoBase
{

    private Rigidbody rbody;
    private Transform view;

    private PlayerState state = PlayerState.normal;
    private int HP;
    private int maxHP;
    private float jumpTime = 0;

    private float sensitivity = 1.5f;
    private float rotationX = 0f;
    private float speed = 3f;   // walk speed 3, run spped 5

    // constants
    private static float MAX_JUMP_INTERVAL = 5f;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        view = transform.Find("Camera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // press LeftAlt to show/hide cursor
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
        if (Cursor.lockState == CursorLockMode.None)
        {
            return;
        }

        switch(state)
        {
            case PlayerState.normal:
                Move();
                Sense();
                Rotate();
                break;
        }
    }

    // rotate with mouse
    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity; // Get the horizontal mouse movement
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity; // Get the vertical mouse movement

        rotationX -= mouseY; // Adjust the rotation around the X-axis based on vertical movement
        rotationX = Mathf.Clamp(rotationX, -60f, 30f); // Clamp the rotation to avoid flipping the camera

        transform.Find("Camera").localRotation = Quaternion.Euler(rotationX, 0f, 0f); // Apply the rotation to the camera

        transform.Rotate(Vector3.up * mouseX); // Rotate the player object horizontally based on mouse movement
    }

    // movement
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        // hold left shift to run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 5f;
        } else
        {
            speed = 3f;
        }
        transform.Translate(dir * Time.deltaTime * speed);  // move
        // press space to jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpTime > MAX_JUMP_INTERVAL)
        {
            rbody.AddForce(Vector3.up * 1000, ForceMode.Impulse);
            jumpTime = 0;
        } else
        {
            jumpTime += Time.deltaTime;
        }
        
    }


    // sensor system
    // spotlight
    void Sense()
    {
        if (Input.GetKey(KeyCode.E))
        {
            MessageCenter.Instance.SendCustomMessage(new Message(MessageType.Type_UI, MessageType.UI_OpenSensor, null));
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                MessageCenter.Instance.SendCustomMessage(new Message(MessageType.Type_UI, MessageType.UI_Sensor1, null));
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // TODO: fix bug: interference between radar and stealth vision (due to involking the same object)
                MessageCenter.Instance.SendCustomMessage(new Message(MessageType.Type_UI, MessageType.UI_Sensor2, null));
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                MessageCenter.Instance.SendCustomMessage(new Message(MessageType.Type_UI, MessageType.UI_Sensor3, null));
            }
        } else
        {
            MessageCenter.Instance.SendCustomMessage(new Message(MessageType.Type_UI, MessageType.UI_CloseSensor, null));
        }


    }

    void UseSpotlight()
    {
        //view.Find("Spot Light").gameObject.SetActive(turnOn);
    }

}
