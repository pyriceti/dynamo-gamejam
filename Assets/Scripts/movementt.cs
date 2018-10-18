using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class movementt : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        UnityEditor.SerializedObject halo = new SerializedObject(rb.GetComponent("Halo"));
        halo.FindProperty("m_Size").floatValue += 0.1f;
        halo.FindProperty("m_Enabled").boolValue = true;
        halo.FindProperty("m_Color").colorValue = Color.white;
        halo.ApplyModifiedProperties();

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown("space"))
        {
            this.rb.velocity = new Vector3(0, 0, 0);
            this.rb.angularVelocity = new Vector3(0, 0, 0);
        }

    }

    private void OnCollisionEnter(Collision other) {
        if (audioSource != null)
          audioSource.Play();
    }
}
