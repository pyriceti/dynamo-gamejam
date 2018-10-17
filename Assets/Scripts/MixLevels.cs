using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {
    public AudioMixer MasterMixer;

    public void SetSfxLevel(float sfxLevel) {
        MasterMixer.SetFloat("sfxVol", sfxLevel);
    }

    public void SetMusicLevel(float musicLevel) {
        MasterMixer.SetFloat("musicVol", musicLevel);
    }
}