using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBrakeys : MonoBehaviour
{
    // Variables
    [SerializeField] float moveSpeed;

    // References
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();  // initializing controller
    }


    private void Update()
    {
        Move();
    }
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // if horizontal and vertical keys are pressed simultanously then the player should not go 
        //  double the speed so add the normalized 
        Vector3 moveAmount = new Vector3(moveX, 0f, moveZ).normalized;  

        controller.Move(moveAmount * moveSpeed * Time.deltaTime);
    }
}
