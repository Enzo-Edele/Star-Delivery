using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]AudioSource music;
    AudioClip currentClip;
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip firstLevelmusic;

    [SerializeField] List<AudioSource> playingEffect;
    [SerializeField] List<float> timerEffect;
    public static SoundManager Instance { get; private set; }
    void Start()
    {
        Instance = this;
        StartMusic(mainMenuMusic);
    }

    void Update()
    {
        if (Input.GetKeyDown("m"))
            ChangeMusic(firstLevelmusic);
        for(int i = 0; i < playingEffect.Count; i++)
        {
            if (timerEffect[i] < 0)
            {
                AudioSource delete = playingEffect[i];
                playingEffect.RemoveAt(i);
                Destroy(delete);
                timerEffect.RemoveAt(i);
            }
            else
                timerEffect[i] -= Time.deltaTime;
        }
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

    public void PlaySoundEffect(AudioClip clip)
    {
        AudioSource effect = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        playingEffect.Add(effect);
        effect.clip = clip;
        effect.volume = 0.25f;
        timerEffect.Add(effect.clip.length);
        effect.Play();
    }
}
