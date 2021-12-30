using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFPS : MonoBehaviour
{
    // GameObject Components
    private CharacterController controller;
    private Animator anim;

    // Variables for Player
    [SerializeField] float moveSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] float JumpTimeout = 0.5f;
    [SerializeField] float FallTimeout = 0.15f;


    // Variables for Ground And Gravity Check
    [SerializeField] bool Grounded = true;
    [SerializeField] float GroundedOffset = 0.14f;
    [SerializeField] float GroundedRadius = 0.28f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravity = -10f;


    private Vector3 verticalVelocity;
    private Vector3 moveAmount;

    // TimeOut Deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    // Animation Handler

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        // Reset the timeouts on Start
        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }

    private void Update()
    {
        JumpAndGravity();
        GroundedCheck();
        Move();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        bool runZ = Input.GetKey("left shift");
        moveAmount = transform.forward * moveZ;

        if (moveAmount == Vector3.zero)
        {
            anim.SetBool("blend",false);
        }
        else if (moveAmount != Vector3.zero && !runZ )
        {
            moveSpeed = walkSpeed;
            anim.SetBool("blend",true);
            if(moveZ > 0.0f )
            {
                anim.SetFloat("Motion", 0.3333f, 0.15f, Time.deltaTime);
            }
            else
            {
                anim.SetFloat("Motion", 0.6666f, 0.15f, Time.deltaTime );
            }
            
        }
        else if (moveAmount != Vector3.zero && runZ)
        {
            moveSpeed = runSpeed;
            anim.SetBool("blend",true);
            if(moveZ > 0.0f)
            {
                anim.SetFloat("Motion", 0.0f, 0.15f, Time.deltaTime);
            }
            else
            {
                anim.SetFloat("Motion", 1.0f, 0.15f, Time.deltaTime);
            }
        } 
        
        controller.Move((moveAmount * moveSpeed * Time.deltaTime ) + new Vector3(0.0f, verticalVelocity.y, 0.0f) * Time.deltaTime);
        controller.transform.Rotate(Vector3.up * moveX * Time.deltaTime * 100f);

    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, groundMask, QueryTriggerInteraction.Ignore);
    }

    private void JumpAndGravity()
    {
        bool jumpKey = Input.GetKey("space");
        if (Grounded)
        {
            // Reset the timeouts
            _fallTimeoutDelta = FallTimeout;

            // Stop our dropping infinitely when Grounded
            if(verticalVelocity.y <= 0.0f)
            {
                verticalVelocity.y = -2f;
            }
            // Jump
            if(jumpKey && _jumpTimeoutDelta < 0.0f)
            {
                verticalVelocity.y = Mathf.Sqrt(gravity * -2 * jumpHeight);
                anim.SetBool("jump",true);
            }
            if(_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
                anim.SetBool("jump", false);
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            if(_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta = FallTimeout;
            }
            // if not Grounded then donot Jump
            jumpKey = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;

    }
    private void Attack()
    {
        anim.SetTrigger("Attack");
    }

}
