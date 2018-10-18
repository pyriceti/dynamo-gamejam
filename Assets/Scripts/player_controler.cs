using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI; public class player_controler : MonoBehaviour {    [HideInInspector] public bool facingRight = true;    [HideInInspector] public bool jump = false;    public float maxSpeed = 5f;    public float jumpForce = 1000f;    public float MaxLife = 100f;     public float deathspeed = 2f;
    float currentLife;    public Slider health_bar;     public Animator anim;    private Rigidbody rb;    public bool safe;


    //Collision bool
    bool left_fixed;    bool right_fixed;    bool rightorleft_pushable;     bool down_any;
    public Transform down_left;    public Transform down_right;    public Transform right_up;
    public Transform right_down;
    public Transform left_up;
    public Transform left_down;
    private LayerMask fixedMask;    private LayerMask jumpableMask;
    private LayerMask pushableMask;    // Use this for initialization
    void Awake()    {        currentLife = MaxLife;         rb = GetComponent<Rigidbody>();        fixedMask = LayerMask.GetMask("Fixed");        jumpableMask = LayerMask.GetMask("Jumpable");
        pushableMask = LayerMask.GetMask("Pushable");        health_bar.maxValue = MaxLife;
        safe = true;     }    void Update()    {        check_line_collision();        if (Input.GetButtonDown("Jump") && down_any) {            jump = true;        }        if(safe)        {            currentLife = MaxLife;         }        else        {            currentLife -= Time.deltaTime * deathspeed;         }        Debug.Log("Safe : " + safe);         health_bar.value = currentLife;     }    public float flip_speed;     void FixedUpdate()    {        //Flip         if(rotating)        {            current_rotation += Time.deltaTime * flip_speed;            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Time.deltaTime * flip_speed,  transform.eulerAngles.z);
            if (current_rotation > 180f)            {                rotating = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, start_rotation + 180, transform.eulerAngles.z);            }        }        if (object_pushing != null && !down_any)        {            object_pushing.GetComponentInParent<Rigidbody>().mass = 200;        }        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));


        if ((h>0 && !right_fixed) || (h<0 && !left_fixed) )        {            rb.velocity = new Vector3(h * maxSpeed, rb.velocity.y, rb.velocity.z);        }        if((h > 0 && right_fixed) || (h < 0 && left_fixed))
        {            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);        }        if (h > 0 && !facingRight)            Flip();        else if (h < 0 && facingRight)            Flip();        if (jump)        {            //rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);            jump = false;        }        if(down_any)        {
            anim.SetBool("IsJumping", false);
        }        else        {            anim.SetBool("IsJumping", true);
        }

        if (Mathf.Abs(rb.velocity.x) > 0.02f)        {            anim.SetBool("IsWalking", true);        }        else        {            anim.SetBool("IsWalking", false);
        }            }    public bool rotating;     public float current_rotation;    public float start_rotation;      public void Flip()    {        rotating = true;        current_rotation = 0; 
        facingRight = !facingRight;        start_rotation = transform.eulerAngles.y;     }    private void OnTriggerStay(Collider other)    {        if(other.tag =="safe_zone")        {            safe = true;         }    }    private void OnTriggerExit(Collider other)    {        if (other.tag == "safe_zone")        {            safe = false;        }
        if (other.gameObject == object_pushing)        {            object_pushing.GetComponentInParent<Rigidbody>().mass = 200;        }    }
    GameObject object_pushing;

    private void OnTriggerEnter(Collider other)
    {
        check_line_collision();        if (other.gameObject.tag == "Pushable" && rightorleft_pushable && down_any && !jump)        {
            object_pushing = other.gameObject;            object_pushing.GetComponentInParent<Rigidbody>().mass = 1;        }
    }
    void check_line_collision()    {        float x_size = GetComponent<BoxCollider>().size.x;
        float y_size = GetComponent<BoxCollider>().size.y; 
        left_fixed = Physics.Linecast(transform.position + new Vector3(0f, 0.30f * y_size, 0f), left_up.position, fixedMask)
            || Physics.Linecast(transform.position + new Vector3(0f, -0.30f * y_size, 0f), left_down.position, fixedMask);        right_fixed = Physics.Linecast(transform.position + new Vector3(0f, 0.30f * y_size, 0f), right_up.position, fixedMask)                            || Physics.Linecast(transform.position + new Vector3(0f, -0.30f * y_size, 0f), right_down.position, fixedMask);


        rightorleft_pushable = Physics.Linecast(transform.position + new Vector3(0f, 0.40f * y_size, 0f), left_up.position, pushableMask)                                || Physics.Linecast(transform.position + new Vector3(0f, -0.40f * y_size, 0f), left_down.position, pushableMask)                                || Physics.Linecast(transform.position + new Vector3(0f, 0.40f * y_size, 0f), right_up.position, pushableMask)
                                      || Physics.Linecast(transform.position + new Vector3(0f, -0.40f * y_size, 0f), right_down.position, pushableMask);        down_any = Physics.Linecast(transform.position + new Vector3(0.3f * x_size, 0f, 0f), down_right.position)                          || Physics.Linecast(transform.position + new Vector3(-0.3f * x_size, 0f, 0f), down_left.position);    }
}