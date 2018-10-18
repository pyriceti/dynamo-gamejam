using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    public AudioMixer MasterMixer;
    public Slider Slider;

    private float minVol = -40;
    private float maxVol = 0f;
    private float pitchTop = 1.5f;
    private float pitchVarianceSpeed = .4f;

    private bool pitchGoingUp;
    private bool pitchGoingDown;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start() {
        SetMusicLevel(minVol);
        MasterMixer.SetFloat("choirsPitch", 1f);
    }

    private void Update() {
        var health = Slider.value;
        var newLevel = health * minVol / 100;
        float currentVol;
        MasterMixer.GetFloat("musicVol", out currentVol);
        SetMusicLevel(Mathf.Lerp(currentVol, newLevel, Time.deltaTime));

        float currentPitch;
        MasterMixer.GetFloat("choirsPitch", out currentPitch);

        if (health < 95 && !pitchGoingUp && Math.Abs(currentPitch - pitchTop) > float.Epsilon)
            StartCoroutine(ToHightPitch());
        else if (health >= 95 && !pitchGoingDown && Math.Abs(currentPitch - 1f) > float.Epsilon) {
            StartCoroutine(ToNormalPitch());
        }
    }

    private IEnumerator ToHightPitch() {
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
}