using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;

    [SerializeField] private AudioSource boss;
    [SerializeField] private AudioSource chill;
    [SerializeField] private AudioSource danger;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            PlayChillMusic();
        }
        else
        {
            Destroy(this.gameObject);
        }
  
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.2f;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }

    public void PlayBossMusic()
    {
        StartCoroutine(FadeOut(chill, 0.5f));
        StartCoroutine(FadeOut(danger, 0.5f));

        // start game music
        StartCoroutine(FadeIn(boss, 5f));
    }

    public void PlayDangerMusic()
    {
        StartCoroutine(FadeOut(chill, 0.5f));
        StartCoroutine(FadeOut(boss, 0.5f));

        // start game music
        StartCoroutine(FadeIn(danger, 5f));
    }

    public void PlayChillMusic()
    {
        StartCoroutine(FadeOut(danger, 0.5f));
        StartCoroutine(FadeOut(boss, 0.5f));

        // start game music
        StartCoroutine(FadeIn(chill, 5f));
    }

    
}
