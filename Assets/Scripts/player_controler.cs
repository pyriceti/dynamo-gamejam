using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controler : MonoBehaviour {



    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public Transform left_check;
    public Transform right_check; 


    private bool grounded = false;
    //private Animator anim;
    private Rigidbody rb;

    bool safe;


    // Use this for initialization
    void Awake()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        bool left_grounded = Physics.Linecast(transform.position, left_check.position, 1 << LayerMask.NameToLayer("Ground"));
        bool right_grounded = Physics.Linecast(transform.position, right_check.position, 1 << LayerMask.NameToLayer("Ground"));

        Debug.Log("Safe : " + safe); 

        //anim.SetFloat("Speed", Mathf.Abs(h));

        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            if((h>0 && !right_grounded) || (h<0 && !left_grounded) )
            {
                rb.velocity = new Vector3(h * maxSpeed, rb.velocity.y, rb.velocity.z);
            }
            else{
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            }
        }

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            //anim.SetTrigger("Jump");
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag =="safe_zone")
        {
            safe = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "safe_zone")
        {
            safe = false;
        }

    }
}
