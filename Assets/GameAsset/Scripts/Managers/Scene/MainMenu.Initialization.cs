using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MainMenu: MonoManagerBase
{
    public override ReinitializationPolicy ReInitPolicy => ReinitializationPolicy.NOT_ALLOWED;

    protected override void EndInitializationBehavior()
    {
        var coins = "" + GM.Instance.Player.GetResource(GlobalConstants.COINS_RESOURCE);
        SetCoinText(coins);
    }

    protected override void StartInitializationBehavior()
    {

        gameObject.SetActive(false);
        EndInitialize(true);
    }
}