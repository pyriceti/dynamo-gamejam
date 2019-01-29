using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_shoot : MonoBehaviour
{
    player_audio pa;
    public Animator anim;

    public int floorMask;

    public GameObject ball;
    public GameObject ball_prefab;

    public float Force;


    bool ball_ready;

    [HideInInspector] public bool holding;
    [HideInInspector] public float holding_time;


    public AudioClip audioClip_throw;
    public AudioClip audioClip_magnet;

    LineRenderer gunLine; // Reference to the line renderer.

    bool draw_line = false;

    private player_controller playerController;

    void Start()
    {
        holding = false;
        holding_time = 0.0f;
        ball_ready = true;

        pa = GetComponent<player_audio>();

        gunLine = GetComponent<LineRenderer>();

        playerController = GetComponent<player_controller>();
    }


    private float air_time;

    Vector3 playerToMouse;

    void Update()
    {
        if (ball_ready)
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit target_hit;

            //Debug.Log(holding_time);

            if (Physics.Raycast(camRay, out target_hit, 20, 1 << floorMask))
            {
                gunLine.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
                gunLine.SetPosition(1, new Vector3(target_hit.point.x, target_hit.point.y, 0));

                // Debug.Log(target_hit.point);

                playerToMouse = target_hit.point - transform.position;

                Debug.DrawLine(this.transform.position, target_hit.point, new Color(255, 0, 0));

                if (holding)
                    holding_time += Time.deltaTime;

                holding_time = Mathf.Min(holding_time, 2f);

                if (Input.GetButtonDown("Fire1") && ball_ready)
                {
                    holding = true;
                    if (draw_line)
                    {
                        gunLine.enabled = true;
                    }
                }

                if (Input.GetButtonUp("Fire1") && ball_ready)
                {
                    if (playerController.facingRight && playerToMouse.x < 0f ||
                        !playerController.facingRight && playerToMouse.x > 0f)
                    {
                        playerController.Flip();
                    }

                    ball_ready = false;

                    anim.SetTrigger("Throw");
                }
            }
        }
        else
        {
            gunLine.enabled = false;
            air_time += Time.deltaTime;
        }

        if (air_time > 1f && ball_shooted != null)
        {
            if (ball_shooted.GetComponent<SphereCollider>())
            {
                Physics.IgnoreCollision(ball_shooted.GetComponent<SphereCollider>(), this.GetComponent<BoxCollider>(),
                    false);
            }
        }
    }

    GameObject ball_shooted;

    public void ball_throw()
    {
        ball.SetActive(false);


        ball_shooted = Instantiate(ball_prefab, ball.transform.position, Quaternion.identity) as GameObject;

        ball_shooted.GetComponent<Rigidbody>().AddForce(playerToMouse.normalized * holding_time * Force);

        Physics.IgnoreCollision(ball_shooted.GetComponent<SphereCollider>(), this.GetComponent<BoxCollider>(), true);

        pa.play_sfx(audioClip_throw, 0.25f + 0.25f * holding_time / 2f);

        holding_time = 0;
        holding = false;
        air_time = 0;
    }

    private void FixedUpdate()
    {
        if (is_magneting_ball && magnet_ball != null)
        {
            delta += Time.deltaTime;
            if ((magnet_ball.transform.position - ball.transform.position).magnitude > 0.2)
            {
                //magnet_ball.transform.position = Vector3.Lerp(magnet_ball.transform.position, ball.transform.position, 0.4f);
                magnet_ball.transform.position = magnet_ball.transform.position +
                                                 (ball.transform.position - magnet_ball.transform.position).normalized *
                                                 delta * delta * 2;
            }
            else
            {
                pa.play_sfx(audioClip_magnet, 1f);

                ball.SetActive(true);
                ball_ready = true;
                Object.Destroy(magnet_ball);
                is_magneting_ball = false;
            }
        }
    }


    bool is_magneting_ball;
    float delta;
    GameObject magnet_ball;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Ball") || !(air_time > 1f)) return;

        is_magneting_ball = true;
        magnet_ball = collision.gameObject;
        Object.Destroy(magnet_ball.GetComponent<Rigidbody>());
        Object.Destroy(magnet_ball.GetComponent<SphereCollider>());
        delta = 0;
    }
}