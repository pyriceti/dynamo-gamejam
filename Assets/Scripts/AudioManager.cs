using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    public AudioMixer MasterMixer;
    public Slider Slider;
    public AudioSource SFXSource;

    public List<AudioClip> SfxClips;

    public float HealthThreshold = 50f;
    public float BaseSfxVol = -30f;

    public float MinMusicVol = -40;
    
    private float maxVol = 0f;
    private float pitchTop = 1.2f;
    private float pitchVarianceSpeed = .2f;

    private bool pitchGoingUp;
    private bool pitchGoingDown;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start() {
        SetMusicLevel(MinMusicVol);
        SetSfxLevel(BaseSfxVol);
        MasterMixer.SetFloat("choirsPitch", 1f);
        StartCoroutine(RandomSfxLoop());
    }

    private void Update() {
        var health = Slider.value;
        var newLevel = health * MinMusicVol / 100;
        float currentVol;
        MasterMixer.GetFloat("musicVol", out currentVol);
        SetMusicLevel(Mathf.Lerp(currentVol, newLevel, Time.deltaTime));

        float currentPitch;
        MasterMixer.GetFloat("choirsPitch", out currentPitch);

        if (health < HealthThreshold && !pitchGoingUp && Math.Abs(currentPitch - pitchTop) > float.Epsilon)
            StartCoroutine(ToHighPitch());
        else if (health >= HealthThreshold && !pitchGoingDown && Math.Abs(currentPitch - 1f) > float.Epsilon) {
            StartCoroutine(ToNormalPitch());
        }
    }

    private IEnumerator ToHighPitch() {
        pitchGoingUp = true;
        pitchGoingDown = false;
        float time = 0f;
        float startingPitch;
        MasterMixer.GetFloat("choirsPitch", out startingPitch);
        float currentPitch = startingPitch;
        while (Math.Abs(currentPitch - pitchTop) > float.Epsilon && !pitchGoingDown) {
            MasterMixer.GetFloat("choirsPitch", out currentPitch);
            MasterMixer.SetFloat("choirsPitch",
                Math.Min(Mathf.Lerp(startingPitch, pitchTop, time * pitchVarianceSpeed), pitchTop));
            time += Time.deltaTime;
            yield return null;
        }

        pitchGoingUp = false;
    }

    private IEnumerator ToNormalPitch() {
        pitchGoingDown = true;
        pitchGoingUp = false;

        float time = 0f;
        float startingPitch;
        MasterMixer.GetFloat("choirsPitch", out startingPitch);

        float currentPitch = startingPitch;
        while (Math.Abs(currentPitch - 1f) > float.Epsilon && !pitchGoingUp) {
            MasterMixer.GetFloat("choirsPitch", out currentPitch);
            MasterMixer.SetFloat("choirsPitch", Math.Max(Mathf.Lerp(startingPitch, 1f, time * pitchVarianceSpeed), 1f));
            time += Time.deltaTime;

            yield return null;
        }

        pitchGoingDown = false;
    }

    public void SetSfxLevel(float sfxLevel) {
        MasterMixer.SetFloat("sfxVol", sfxLevel);
    }

    public void SetMusicLevel(float musicLevel) {
        MasterMixer.SetFloat("musicVol", musicLevel);
    }

    private IEnumerator RandomSfxLoop() {
        while (true) {
            if (!SFXSource.isPlaying) {
                SFXSource.clip = SfxClips[Random.Range(0, SfxClips.Count)];
                SFXSource.Play();
                yield return new WaitForSeconds(Random.Range(7.5f, 10f));
            }
            else yield return new WaitForSeconds(1f);
        }
    }
}