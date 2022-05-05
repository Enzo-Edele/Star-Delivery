using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Sound[] tutoSounds;
    public Sound[] soundsEffects;
    public Sound[] musics;

    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;

        foreach(Sound s in soundsEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixerGroup;

            s.source.volume = s.volume;

            s.source.loop = s.loop;
        }
        foreach (Sound s in tutoSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixerGroup;

            s.source.volume = s.volume;

            s.source.loop = s.loop;
        }
        foreach (Sound s in musics)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixerGroup;

            s.source.volume = s.volume;

            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(soundsEffects, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("sound name not find : " + name);
            return;
        }
        s.source.Play();
    }
    public float PlayTime(int i)
    {
        Sound s = tutoSounds[i];
        if (s == null)
        {
            Debug.Log("sound name not find : " + name);
            return 0;
        }
        s.source.Play();
        return s.clip.length;
    }
    public void PlayMusic()
    {

    }

    public void StopAllSoud()
    {
        foreach (Sound s in soundsEffects)
        {
            s.source.Stop();
        }
        foreach (Sound s in tutoSounds)
        {
            s.source.Stop();
        }
        foreach (Sound s in musics)
        {
            s.source.Stop();
        }
    }
}
