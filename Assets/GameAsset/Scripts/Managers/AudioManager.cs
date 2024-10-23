using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class AudioManager
{
    [SerializeField]
    private List<Sound> bgm, sfx, powerUpSfx;

    [SerializeField]
    private AudioSource bgmSource, sfxSource;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMusic()
    {
        Log.Info("Request Mute/Unmute BGM");
        bgmSource.mute = !bgmSource.mute;
    }

    public void ToggleSFX()
    {
        Log.Info("Request Mute/Unmute SFX");
        sfxSource.mute = !sfxSource.mute;
    }

    public void SetMusicVolume(float volume)
    {
        Log.Info("Request change volume BGM");
        bgmSource.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        Log.Info("Request change volume SFX");
        sfxSource.volume = volume;
    }

    public void PlayMusic(LibraryMusics msc)
    {
        Sound music = null;

        if (msc == LibraryMusics.Menu)
        {
            music = bgm[0];
        }
        else
        {
            music = bgm[1];
        }

        if (music == null)
        {
            Log.Warn("Missing background music not found");
        }
        else
        {
            Log.Info("Background music should play");
            bgmSource.clip = music.clip;
            bgmSource.Play();
        }
    }

    public void PauseMucsic()
    {
        bgmSource.Pause();
    }

    public void PlaySFX(LibrarySounds _sfx)
    {
        Sound s = null;

        switch (_sfx)
        {
            case LibrarySounds.Button:
                s = sfx[0];
                break;

            case LibrarySounds.Cheering:
                s = sfx[1];
                break;

            case LibrarySounds.Win:
                s = sfx[2];
                PauseMucsic();
                break;

            case LibrarySounds.Lose:
                s = sfx[3];
                PauseMucsic();
                break;
        }

        if (s == null)
        {
            Log.Warn("SFX background music not found");
        }
        else
        {
            Log.Info("SFX music should play");
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }

    }

    public void PlaySFX(PowerUpSounds _sfx)
    {
        Sound s = null;

        switch (_sfx)
        {
            case PowerUpSounds.Return:
                s = sfx[0];
                break;

            case PowerUpSounds.Replay:
                s = sfx[1];
                break;

            case PowerUpSounds.Drill:
                s = sfx[2];
                break;
        }

        if (s == null)
        {
            Log.Warn("SFX background music not found");
        }
        else
        {
            Log.Info("SFX music should play");
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }
}
