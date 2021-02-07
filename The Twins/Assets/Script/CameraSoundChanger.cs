using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSoundChanger : MonoBehaviour
{
    public AudioSource audio;
    public void changeAudio(AudioClip music)
    {
        audio.Stop();
        audio.clip = music;
        audio.Play();
    }
}
