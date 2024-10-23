using System;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupBehaviourPause : UIPopupBehaviour
{
    private bool mute = false;
    private bool halfVolume = false;

    private bool muteSfx = false;
    private bool halfVolumeSfx = false;

    public void OnHomeButton()
    {
        GM.Instance.MainGame.ReturnMainMenu();

        Popup.Hide();
    }

    public void OnRetryButton()
    {
        GM.Instance.MainGame.RestartCurrentLevel();
        Popup.Hide();
    }

    public void OnMusicToggle()
    {
        if (!mute)
        {
            if (!halfVolume)
            {
                setVolume(0.5f);
                halfVolume = true;
            }
            else
            {
                AudioManager.Instance.ToggleMusic();
                halfVolume = false;
                mute = true;
            }
        }
        else
        {
            setVolume(1.0f);
            AudioManager.Instance.ToggleMusic();
            mute = false;
        }
    }

    private void setVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }

    public void OnSfxToggle()
    {
        if (!muteSfx)
        {
            if (!halfVolumeSfx)
            {
                setVolumeSfx(0.5f);
                halfVolumeSfx = true;
            }
            else
            {
                AudioManager.Instance.ToggleSFX();
                halfVolumeSfx = false;
                muteSfx = true;
            }
        }
        else
        {
            setVolumeSfx(1.0f);
            AudioManager.Instance.ToggleSFX();
            muteSfx = false;
        }
    }

    private void setVolumeSfx(float volume)
    {
        AudioManager.Instance.SetSfxVolume(volume);
    }

    public void OnVibrationToggle()
    {
    }

    public void OnNotiButtonToggle()
    {
    }
}