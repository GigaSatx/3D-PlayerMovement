using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonChase : MonoBehaviour
{
    public Transform player;
    // PlayerMovementFPS playerScript;
    static Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        // playerScript = GameObject.Find("moveAmount").GetComponent<PlayerMovementFPS>();
    }


    void Update()
    {
        Vector3 direction = player.position -  this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if(Vector3.Distance(player.position, this.transform.position) < 10f && angle <= 30)
        {
            
            direction.y = 0f;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            anim.SetBool("isIdle", false);
            if (direction.magnitude > 5.0f)
            {
                this.transform.Translate(0f, 0f, 0.05f);
                anim.SetBool("isWalking",true);
                anim.SetBool("isAttacking", false);
            }
            else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", true);
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking",false);
        }
        
    }
}
