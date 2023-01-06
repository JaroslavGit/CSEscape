using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JosefScript : MonoBehaviour
{
    [SerializeField]
    public float speed = 7.0f;
    public float jump = 5.0f;
    private float moveH;
    private float moveV;

    public GameObject teleporter;

    public bool isJumping;
    public Animator anim;
    public SpriteRenderer srender;
    public Rigidbody2D rig;

    private int bagsCount;
    public Text bagsText;
    [SerializeField] private AudioSource bagCollect;

    public LayerMask layer;
    [SerializeField]
    public float rayLenght = 0.9f;
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
        if (isGrounded())
        {
            Jump();
        }
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

    bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), Vector2.down,rayLenght, layer);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bag")
        {
            bagCollect.Play();
            Destroy(collision.gameObject);
            bagsCount++;
            bagsText.text = bagsCount + " / 5";
        }

        if (collision.gameObject.tag == "Cedula") {
            Instantiate(teleporter, new Vector3(-18, 3 , 0), Quaternion.identity);
            
        }

        if (collision.gameObject.tag == "Teleporter") {
            transform.position = new Vector3(12, 0, 0);
        }







        /*if (collision.gameObject.tag == "Medailon") //SKRIPTOS PRO MEDAILONOS
        {
            Instantiate(teleporter, new Vector3(-18, 3, 0), Quaternion.identity);

        }

        if (collision.gameObject.tag == "Teleporter1")
        {
            transform.position = new Vector3(12, 0, 0);
        }*/
    }

    

}
