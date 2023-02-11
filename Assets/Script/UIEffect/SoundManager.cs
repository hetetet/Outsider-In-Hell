using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource BgmSound;
    AudioSource EffectSound;
    AudioSource UISound;
    public static SoundManager Instance = null;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else if(Instance!=this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        BgmSound = transform.Find("BgmSound").GetComponent<AudioSource>();
        BgmSound.loop = true;
        EffectSound = transform.Find("EffectSound").GetComponent<AudioSource>();
        UISound = transform.Find("UISound").GetComponent<AudioSource>();
    }

    public void setBgmVolumn(float value)
    {
        BgmSound.volume = value;
    }
    public void setEffectVolumn(float value)
    {
        EffectSound.volume = value;
        UISound.volume = value;
    }

    public void playBGM(AudioClip sound)
    {
        BgmSound.clip = sound;
        BgmSound.Play();
    }

    public IEnumerator fadeBgmCorut(AudioClip sound, float fadeTime)
    {       
        float currentTime = 0;
        float targetVolume=BgmSound.volume;
        BgmSound.volume = 0f;

        BgmSound.clip = sound;
        BgmSound.Play();
        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            BgmSound.volume = Mathf.Lerp(0, targetVolume, currentTime / fadeTime);
            yield return null;
        }
        BgmSound.volume = targetVolume;
        yield break;
    }

    public void playEffectSound(AudioClip sound)
    {
        EffectSound.clip = sound;
        EffectSound.Play();
    }

    public void playUISound(AudioClip sound)
    {
        UISound.clip = sound;
        UISound.Play();
    }

    public void stopBGM()
    {
        BgmSound.Stop();
    }
}
