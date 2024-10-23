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
            // InitializeSecondary();
        }

        if (!secondaryLock)
        {
            secondaryLock = true;            
            EndInitialize(true);
        }

    }

    protected override void EndInitializationBehavior()
    {
        // After Initialization

    }

    private void InitializePrimary()
    {
        _primaryManagers.ForEach(x => x.Initialize());
    }
}