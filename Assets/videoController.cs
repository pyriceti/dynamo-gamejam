using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoController : MonoBehaviour {

    public Light Light;
    public GameObject Text;
    public GameObject Text2;


    private VideoPlayer videoPlayer;

    private float maxIntensity = 15f;
    private bool videoOver;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Use this for initialization
    void Start() {
        Text.SetActive(false);
        Text2.SetActive(false);

        videoPlayer.Play();
        StartCoroutine(HandleVideoEnd());
    }


    IEnumerator HandleVideoEnd() {
        yield return new WaitForSeconds(2f);
        while (!videoOver) {
            if (!videoPlayer.isPlaying) {
                videoPlayer.enabled = false;
                StartCoroutine(LightenScene());
                videoOver = true;
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator LightenScene() {
        yield return new WaitForSeconds(2f);
        Text.SetActive(true);
        Text2.SetActive(true);

        float t = 0f;
        while (Light.intensity <= maxIntensity) {
            Light.intensity = Mathf.Lerp(0f, maxIntensity, t);
            t += Time.deltaTime;
            yield return null;
        }

    }
}
