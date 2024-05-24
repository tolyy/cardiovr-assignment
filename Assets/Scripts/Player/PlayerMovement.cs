using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float walkSpeed = 10f;
    Vector3 velocity;
    public float gravity = -15f;

    public Transform groundCheck;
    public float checkDistance = 0.3f;
    public LayerMask ground;
    bool bGrounded;

    public float jumpForce = 3f;

    public float mouseSens = 600f;

    public Transform playerCapsule;
    float xLook = 0f;
    [SerializeField] Camera camera1;

    // Update is called once per frame
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        bGrounded = Physics.CheckSphere(groundCheck.position, checkDistance, ground);

        if(bGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && bGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * walkSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xLook -= mouseY;
        xLook = Mathf.Clamp(xLook, -90f, 90f);

        camera1.transform.localRotation = Quaternion.Euler(xLook, 0f, 0f);
        playerCapsule.Rotate(Vector3.up * mouseX);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer == LayerMask.NameToLayer("Pushable"))
        {
            Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                Vector3 pushDir = hit.gameObject.transform.position - transform.position;
                rb.AddForce(pushDir.normalized * 2f, ForceMode.Impulse);
            }
        }
    }
}
