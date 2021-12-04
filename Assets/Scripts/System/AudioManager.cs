using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Range(0,8)] public int Volume = 5;

    private AudioSource[] _audioSources;
    private AudioClip _nextClip;
    private bool _isTrack1Playing;

    void Start()
    {
        _audioSources = GetComponents<AudioSource>();
        _isTrack1Playing = true;
    }

    public void SwapTrack(AudioClip newClip){ 

        StopAllCoroutines();
        StartCoroutine(FadeTrack(newClip));

    }

    IEnumerator FadeTrack(AudioClip newClip){

        float timeToFade = 0.5f;
        float timeElapsed = 0;

        if(_isTrack1Playing){

            _audioSources[1].clip = newClip;
            _audioSources[1].Play();

            while (timeElapsed < timeToFade){

                _audioSources[1].volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                _audioSources[0].volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _audioSources[0].Stop();
        }
        else {
            _audioSources[0].clip = newClip;
            _audioSources[0].Play();

            while (timeElapsed < timeToFade){

                _audioSources[0].volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                _audioSources[1].volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _audioSources[1].Stop();
        }
    }
}
