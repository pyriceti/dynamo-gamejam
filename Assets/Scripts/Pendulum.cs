using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour {
    public float leftPushRange;
    public float rightPushRange;
    public float velocityThreeshold;
    
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(0f, 0f, velocityThreeshold);
    }

    private void Update() {
        Push();
    }

    private void Push() {
//        Debug.Log("transform.rotation.z = " + transform.rotation.z);
        Debug.Log("rb.angularVelocity.z = " + rb.angularVelocity.z);
        if (transform.rotation.z > 0
            && transform.rotation.z < rightPushRange
            && rb.angularVelocity.z > 0
            && rb.angularVelocity.z < velocityThreeshold) {
//            Debug.Log(1);
//            Debug.Log("transform.rotation.z = " + transform.rotation.z);
            rb.angularVelocity = new Vector3(0f, 0f, velocityThreeshold);
        }
        else if (transform.rotation.z < 0
                 && transform.rotation.z > leftPushRange
                 && rb.angularVelocity.z < 0
                 && rb.angularVelocity.z > velocityThreeshold * -1) {
//            Debug.Log(2);
//            Debug.Log("transform.rotation.z = " + transform.rotation.z);
            rb.angularVelocity = new Vector3(0f, 0f, velocityThreeshold) * -1;
        }
    }
}