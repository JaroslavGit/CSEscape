using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JosefScript : MonoBehaviour
{
    [SerializeField]
    public float speed = 7.0f;
    public float jump = 5.0f;
    private float moveH;
    private float moveV;

    public bool isJumping;
    public Animator anim;
    public SpriteRenderer srender;
    public Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();  
        srender = gameObject.GetComponent<SpriteRenderer>();
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
     
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
          {
            anim.SetBool("isAttacking", true);
        }
        else {
            anim.SetBool("isAttacking", false);
        }
    }
    private void FixedUpdate()
    {
        if (!isJumping)
        {
            //Jump();
        }
    }

    /*
     * void Jump()
    {
     moveV = Input.GetAxisRaw("Vertical");
     //rig.AddForce(new Vector2(0f, moveV)*jump, ForceMode2D.Impulse);
     rig.velocity = new Vector2(rig.velocity.x, jump);
    }
    */

    private void Move()
    {
        moveH = Input.GetAxisRaw("Horizontal");

        //transform.position += new Vector3(moveH, 0f, 0f) * speed * Time.deltaTime;

        rig.velocity = new Vector2(moveH * speed, rig.velocity.y);
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, jump);
        }
        if (moveH > 0f)
        {
            anim.SetBool("isRunning", true);
            srender.flipX = false;
        } else
        
        if (moveH < 0f)
            {
             anim.SetBool("isRunning", true);
             srender.flipX = true;
            }
        
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }    
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true; ;
        }

    }
}
