using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //  Variables
    [SerializeField] float moveSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    private Vector3 moveAmount;

    // References
    private CharacterController controller;

    private void Awake() 
    {
        controller = GetComponent<CharacterController>();   // referencing the component
    }


    private void Update()
    {
        Move();
    }
    private void Move()
    {
        float moveZ =Input.GetAxis("Vertical");
        float turnX = Input.GetAxis("Horizontal");

        moveAmount = new Vector3(0f,0f,moveZ).normalized;
        // moveAmount = transform.TransformDirection(moveAmount);

        controller.Move(moveAmount * moveSpeed * Time.deltaTime); 
        controller.transform.Rotate(Vector3.up * turnX * Time.deltaTime * 100f);

        if (moveAmount != Vector3.zero)
        {
            // do something
        }
    }
    private void Idle()
    {
        // idle
    }
    private void Walk()
    {
        // Walk
    }
    private void Run()
    {
        // Run
    }
}
