using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]AudioSource music;
    AudioClip currentClip;
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip firstLevelmusic;

    [SerializeField] AudioSource barks;
    [SerializeField] AudioClip soundUn;


    public static SoundManager Instance { get; private set; }
    void Start()
    {
        Instance = this;
        StartMusic(mainMenuMusic);
    }

    private void Update()
    {
        if (Input.GetKeyDown("m"))
            ChangeMusic(firstLevelmusic);
    }

    void StartMusic(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }

    void ChangeMusic(AudioClip clip) //maybe une coroutine
    {
        music.clip = clip;
        music.Play();
    }
}
