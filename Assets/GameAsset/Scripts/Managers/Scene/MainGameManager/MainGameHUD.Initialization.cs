using System;
using System.Collections.Generic;

public partial class MainGameHUD : MonoManagerBase
{

    public override ReinitializationPolicy ReInitPolicy => ReinitializationPolicy.NOT_ALLOWED;

    protected override void EndInitializationBehavior()
    {
        _coins = GM.Instance.Player.GetResource(GlobalConstants.COINS_RESOURCE);
        UpdateCoins();
    }

    protected override void StartInitializationBehavior()
    {
        EndInitialize(true);
    }

}