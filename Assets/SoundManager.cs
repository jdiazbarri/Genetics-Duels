using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestor global de sonido del juego.
//
// Se encarga tanto de la reproducción continua de música
// como de los efectos de sonido asociados a eventos del combate.
public class SoundManager : MonoBehaviour
{
    // Instancia global accesible para otro elementos del sistema
    public static SoundManager instance;

    // AudioSource dedicado a música
    [Header("Music")]
    public AudioSource musicSource;
    // Lista de canciones
    public AudioClip[] songs;

    // AudioSource dedicado a efectos de sonido
    [Header("SFX")]
    public AudioSource sfxSource;

    // Sonidos asociados a distintos eventos del juego
    public AudioClip playerHitSound;

    public AudioClip enemyHitSound;

    public AudioClip battleStartSound;

    public AudioClip levelCompleteSound;

    private int currentSong = 0;

    private void Awake()
    {
        instance = this;
    }

    // =========================
    // Canciones
    // =========================

    void Start()
    {
        PlaySong();
    }

    void Update()
    {
        if (!musicSource.isPlaying)
        {
            NextSong();
        }
    }

    void PlaySong()
    {
        musicSource.clip = songs[currentSong];
        musicSource.Play();
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

    // =========================
    // SFX
    // =========================

    public void PlayPlayerHitSound()
    {
        sfxSource.PlayOneShot(playerHitSound);
    }

    public void PlayEnemyHitSound()
    {
        sfxSource.PlayOneShot(enemyHitSound);
    }

    public void PlayBattleStartSound()
    {
        sfxSource.PlayOneShot(battleStartSound);
    }

    public void PlayLevelCompleteSound()
    {
        sfxSource.PlayOneShot(levelCompleteSound);
    }
}
