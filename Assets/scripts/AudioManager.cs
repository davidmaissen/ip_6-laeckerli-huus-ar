using System;
using UnityEngine.Audio;
using UnityEngine;

// Based on source https://www.youtube.com/watch?v=6OT43pvUyfY

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake() 
    {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // play sound of array if it exists and is not playing
    public void Play(string name) 
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null) { 
            Debug.Log("Can't find file with name " + name);
            return; 
        }
        if (!s.source.isPlaying) s.source.Play();
    }

    // stop playing of sound if it exists
    public void Stop(string name) 
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null) { 
            Debug.Log("Can't find file with name " + name);
            return; 
        }
        s.source.Stop();
    }
    
    public AudioSource GetSoundSource(string name) 
    {
        return Array.Find(sounds, sounds => sounds.name == name).source;
    }
}
