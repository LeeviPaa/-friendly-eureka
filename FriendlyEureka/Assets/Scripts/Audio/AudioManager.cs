using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    
    protected override void Awake()
    {
        base.Awake();
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }

        s.source.Play();
    }

    public void Pause (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }

        s.source.Pause();
    }

    public void Crossfade (string name1, string name2)
    {
        Sound s1 = Array.Find(sounds, sound => sound.name == name1);
        Sound s2 = Array.Find(sounds, sound => sound.name == name2);
        if (s1 == null || s2 == null)
        {
            Debug.LogWarning($"Sound: {name1} or {name2} not found!");
            return;
        }

        s1.source.Pause();
        s2.source.Play();
    }
    
    public void PlayFromBeginning (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }

        s.source.time = 0;
        s.source.Play();
    }

    public void NullCheck (Sound s)
    {
        if (s == null)
        {
            Debug.LogWarning($"Sound: {s.name} not found!");
            return;
        }
    }
}
