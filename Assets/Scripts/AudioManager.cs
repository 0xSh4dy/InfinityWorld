using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    void Awake(){
        foreach(Sound s in sounds){
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.volume = s.volume;
        }
    }
    public void PlaySound(string name){
        Sound s = Array.Find(sounds,sound=>sound.clipName==name);
        if(s.clipName=="MainAudio"){
            s.audioSource.loop = true;
        }
        s.audioSource.Play();
    }
    public void PauseSound(string name){
        Sound s = Array.Find(sounds,sound=>sound.clipName==name);
        s.audioSource.Pause();
        
    }
    public void StopSound(string name){
        Sound s = Array.Find(sounds,sound=>sound.clipName==name);
        s.audioSource.Stop();
        
    }
}