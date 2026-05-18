using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songs;

    private int currentSong = 0;

    void Start()
    {
        PlaySong();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            NextSong();
        }
    }

    void PlaySong()
    {
        audioSource.clip = songs[currentSong];
        audioSource.Play();
    }

    void NextSong()
    {
        currentSong++;

        if (currentSong >= songs.Length)
        {
            currentSong = 0;
        }

        PlaySong();
    }
}
