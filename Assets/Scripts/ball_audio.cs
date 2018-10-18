using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_audio : MonoBehaviour {


    AudioSource ball_audiosource;
    public AudioClip ball_bounce; 

	// Use this for initialization
	void Start () {
        ball_audiosource = GetComponent<AudioSource>();
	}
	
    private void OnCollisionEnter(Collision collision)
    {
        ball_audiosource.PlayOneShot(ball_bounce, 0.75f + 0.25f * collision.relativeVelocity.magnitude / 15f); 
    }
}
