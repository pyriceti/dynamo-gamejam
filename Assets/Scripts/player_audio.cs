using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_audio : MonoBehaviour {

    player_shoot ps;
    
    public AudioSource audioSource_hold;
    public AudioSource audioSource_sfx;


    public Animator anim;

    public float max_pitch_hold;
    public float max_volume_hold;

    public AudioSource WalkingSource;
    public AudioSource BreathingSource;


    float start_pitch;
    float start_volume;

    private bool isWalking;
    private bool isBreathing;

    public float BreathDecreaseSpeed = .2f;


    // Use this for initialization
    void Start () {
        ps = GetComponent<player_shoot>();

        start_pitch = audioSource_hold.pitch;
//        start_volume = audioSource_hold.volume; 


    }
    private void Update()
    {
        if(ps.holding)
        {
            if(!audioSource_hold.isPlaying)
            {
                audioSource_hold.Play();
            }

            audioSource_hold.pitch = start_pitch + (max_pitch_hold - start_pitch) * ps.holding_time / 2f;
//            audioSource_hold.volume = start_volume + (max_volume_hold - start_volume) * ps.holding_time / 2f;

        }
        else
        {
            audioSource_hold.Stop();
            audioSource_hold.pitch = start_pitch;
//            audioSource_hold.volume = start_volume; 
        }
        
        if (anim.GetBool("IsWalking") && !isWalking) {
            isWalking = true;
            WalkingSource.Play();
            BreathingSource.Stop();
        }
        else if (!anim.GetBool("IsWalking") && isWalking) {
            isWalking = false;
            WalkingSource.Stop();
            if (!isBreathing) StartCoroutine(StartBreathing());
        }

    }

    IEnumerator StartBreathing() {
        isBreathing = true;
        float t = 0f;
        BreathingSource.Play();
        while (t < 5f) {
            BreathingSource.volume -= Time.deltaTime * BreathDecreaseSpeed;
            t += Time.deltaTime;
            yield return null;
        }

        BreathingSource.volume = 1f;
        isBreathing = false;
    }

    public void play_sfx(AudioClip clip , float volume)
    {
//        audioSource_sfx.volume = volume;
        audioSource_sfx.PlayOneShot(clip); 
    }


}
