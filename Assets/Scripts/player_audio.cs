using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_audio : MonoBehaviour {

    player_shoot ps;
    
    public AudioSource audioSource_hold;
    public AudioSource audioSource_sfx;


    public float max_pitch_hold;
    public float max_volume_hold;


    float start_pitch;
    float start_volume;


    // Use this for initialization
    void Start () {
        ps = GetComponent<player_shoot>();

        start_pitch = audioSource_hold.pitch;
        start_volume = audioSource_hold.volume; 


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
            audioSource_hold.volume = start_volume + (max_volume_hold - start_volume) * ps.holding_time / 2f;

        }
        else
        {
            audioSource_hold.Stop();
            audioSource_hold.pitch = start_pitch;
            audioSource_hold.volume = start_volume; 
        }
    }

    public void play_sfx(AudioClip clip , float volume)
    {
        audioSource_sfx.volume = volume;
        audioSource_sfx.PlayOneShot(clip, volume); 
    }


}
