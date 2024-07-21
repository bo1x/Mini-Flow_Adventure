using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;

    [SerializeField] private AudioSource boss;
    [SerializeField] private AudioSource chill;
    [SerializeField] private AudioSource danger;
    [SerializeField] private AudioSource menu;

    [SerializeField] private AudioSource Win;
    [SerializeField] private AudioSource Lose;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            PlayMenuMusic();
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
        StartCoroutine(FadeOut(menu, 0.5f));
        StartCoroutine(FadeOut(Win, 0.5f));
        StartCoroutine(FadeOut(Lose, 0.5f));


        // start game music
        StartCoroutine(FadeIn(boss, 2.5f));
    }

    public void PlayDangerMusic()
    {
        StartCoroutine(FadeOut(chill, 0.5f));
        StartCoroutine(FadeOut(boss, 0.5f));
        StartCoroutine(FadeOut(menu, 0.5f));
        StartCoroutine(FadeOut(Win, 0.5f));
        StartCoroutine(FadeOut(Lose, 0.5f));


        // start game music
        StartCoroutine(FadeIn(danger, 2.5f));
    }

    public void PlayChillMusic()
    {
        StartCoroutine(FadeOut(danger, 0.5f));
        StartCoroutine(FadeOut(boss, 0.5f));
        StartCoroutine(FadeOut(menu, 0.5f));
        StartCoroutine(FadeOut(Win, 0.5f));
        StartCoroutine(FadeOut(Lose, 0.5f));

        // start game music
        StartCoroutine(FadeIn(chill, 2.5f));
    }

    public void PlayMenuMusic()
    {
        StartCoroutine(FadeOut(danger, 0.5f));
        StartCoroutine(FadeOut(boss, 0.5f));
        StartCoroutine(FadeOut(chill, 0.5f));
        StartCoroutine(FadeOut(Win, 0.5f));
        StartCoroutine(FadeOut(Lose, 0.5f));


        // start game music
        StartCoroutine(FadeIn(menu, 2.5f));
    }

    public void PlayWinMusic()
    {
        StartCoroutine(FadeOut(danger, 0.5f));
        StartCoroutine(FadeOut(boss, 0.5f));
        StartCoroutine(FadeOut(chill, 0.5f));
        StartCoroutine(FadeOut(menu, 0.5f));
        StartCoroutine(FadeOut(Lose, 0.5f));


        StartCoroutine(FadeIn(Win, 1f));
    }

    public void PlayLoseMusic()
    {
        StartCoroutine(FadeOut(danger, 0.5f));
        StartCoroutine(FadeOut(boss, 0.5f));
        StartCoroutine(FadeOut(chill, 0.5f));
        StartCoroutine(FadeOut(menu, 0.5f));
        StartCoroutine(FadeOut(Win, 0.5f));

        StartCoroutine(FadeIn(Lose, 1f));
        

    }
}
