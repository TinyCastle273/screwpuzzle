using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GM
{
    public override ReinitializationPolicy ReInitPolicy => ReinitializationPolicy.NOT_ALLOWED;

    private bool primaryLock = false;
    private bool secondaryLock = false;

    protected override void StartInitializationBehavior()
    {
        primaryLock = false;
        secondaryLock = false;

        InitializePrimary();
    }

    private void UpdateInitialization(float dt)
    {
        if (State != InitializationState.INITIALIZING) return;

        if (!primaryLock)
        {
            primaryLock = true;
            InitializeSecondary();
        }

        if (!secondaryLock)
        {
            secondaryLock = true;
            EndInitialize(true);
        }

        _loadingScreen.RequestLoad(beforeOutAction: () =>
        {
            RequestGoTo(Screen.MENU, true);
        });


    }

    protected override void EndInitializationBehavior()
    {
        // After Initialization
        var pref = _playerManager.GetPreference();
        AudioManager.SetMusicVolume(pref.MusicVolume > 0 ? 0.5f : 0f);
        AudioManager.SetSfxVolume(pref.SfxVolume > 0 ? 0.5f : 0f);

    }

    private void InitializePrimary()
    {
        _primaryManagers.ForEach(x => x.Initialize());
    }

    private void InitializeSecondary()
    {
        _secondaryManagers.ForEach(x => x.Initialize());
    }

}