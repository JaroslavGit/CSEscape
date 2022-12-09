using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JosefScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 7.0f;
    [SerializeField]
    private float jump = 8.0f;
    private float moveH;
    private float moveV;

    public bool isJumping;
    public Animator anim;
    public SpriteRenderer srender;
    public Rigidbody2D rig;

    public LayerMask groundLay;  //vrstva, která detekuje skok

    public int bagsCounter;
    //UI
    public Text uiText; 
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
     if(IsGrounded())
        {
            Jump();
        }
    }

    bool IsGrounded()
    {   //vyšlu paprsek na zem
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(this.transform.position.x,this.transform.position.y), Vector2.down, 0.9f, groundLay);
        return hit.collider != null;
    }

    void Jump()
    {
     moveV = Input.GetAxisRaw("Vertical");
     rig.AddForce(new Vector2(0f, moveV)*jump, ForceMode2D.Impulse);
    }
    

    private void Move()
    {
        moveH = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(moveH, 0f, 0f) * speed * Time.deltaTime;

       
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
     
        //pick Bags
        if (collision.gameObject.tag == "Bag")
        {
            bagsCounter++;
            Destroy(collision.gameObject);
            uiText.text = "Found Bags: " + bagsCounter + "/ 5";

        }
    }

}
