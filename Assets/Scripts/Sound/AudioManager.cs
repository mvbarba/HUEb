using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public Sound[] sounds;

    private void Start()
    {
        Play("Song");
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public static AudioManager Instance()
    {
        return instance;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    int num;

    public void PlayRandomSwitch()
    {
        num++;
        Play((num % 2 == 0) ? "Switch1" : "Switch2");
    }

    public void ChangeVolume(float multiplier)
    {
        foreach (Sound s in sounds)
        {
            if (!s.music)
                s.source.volume = s.volume * multiplier;
        }
    }

    public void ChangeMusicVolume(float multiplier)
    {
        foreach (Sound s in sounds)
        {
            if (s.music)
                s.source.volume = s.volume * multiplier;
        }
    }
}
