using UnityEngine;

public class PlayerSfx : MonoBehaviour {
    public AudioSource AudioSource;
    public AudioClip WalkingClip;
    public AudioClip BreathingClip;

    private void Start() {
        AudioSource.loop = true;
    }

    public void StartWalking() {
        AudioSource.pitch = 1f;
        AudioSource.clip = WalkingClip;
        AudioSource.Play();
    }

    public void StartBreathing() {
        AudioSource.clip = BreathingClip;
        AudioSource.Play();
    }

    public void StopPlay() {
        AudioSource.Stop();
    }
}