using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [Range(0,8)] public int Volume = 5;
    
    [Header("Audio Clips")]
    [SerializeField] AudioClip _ambientForestClip;
    [SerializeField] AudioClip _barGramophoneClip;
    [SerializeField] AudioClip _minigameClip;

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

        yield return new WaitForSeconds(0.1f);

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

    //Triggered by OnSceneIn game event
    public void OnSceneChange(){

        //Apparently ToString() can't be used in a switch expression
        if (SceneManager.GetActiveScene().name == ScenesEnum._8_Bar.ToString()){ SwapTrack(_barGramophoneClip);}
        else if (SceneManager.GetActiveScene().name == ScenesEnum.ChessMinigame.ToString() ||
                 SceneManager.GetActiveScene().name == ScenesEnum.FionnaMinigame.ToString() ||
                 SceneManager.GetActiveScene().name == ScenesEnum.NudoMinigame.ToString())
             { SwapTrack(_minigameClip);}
        else { SwapTrack(_ambientForestClip);}
    }
}
