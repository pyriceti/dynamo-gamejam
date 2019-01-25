using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menu_behaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonUp("Fire1"))
        {

            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit target_hit;


            if (Physics.Raycast(camRay, out target_hit, 20))
            {
                if(target_hit.collider.tag == "PLAY")
                {
                    SceneManager.LoadScene(1); 
                }
            }

        }
    }
}
