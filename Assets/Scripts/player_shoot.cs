using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_shoot : MonoBehaviour {


    public int floorMask;

    public GameObject ball;
    public GameObject ball_prefab;

    public float Force;

    bool holding; 
    bool ball_ready;

    float holding_time; 



    void Start () {
        holding = false;
        holding_time = 0.0f;
        ball_ready = true; 
	}
	
	void Update () {
    
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit target_hit;


        if (Physics.Raycast(camRay, out target_hit, 20, 1 << floorMask))
        {

//            Debug.Log(holding_time); 

            Vector3 playerToMouse = target_hit.point - transform.position;

            Debug.DrawLine(this.transform.position, target_hit.point, new Color(255, 0, 0));

            if (holding)
                holding_time += Time.deltaTime; 

            if(Input.GetButtonDown("Fire1") && ball_ready)
            {
                holding = true;
            }

            if(Input.GetButtonUp("Fire1") && ball_ready)
            {

                ball.SetActive(false);

                GameObject ball_shooted = Instantiate(ball_prefab, ball.transform.position, Quaternion.identity) as GameObject;

                ball_shooted.GetComponent<Rigidbody>().AddForce(playerToMouse.normalized * holding_time * Force);

                holding_time = 0; 
                ball_ready = false;
                holding = false; 
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ball")
        {
            ball.SetActive(true);
            ball_ready = true;
            Object.Destroy(collision.gameObject); 
        }
    }


}
