using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MainMenu: MonoManagerBase
{
    public override ReinitializationPolicy ReInitPolicy => ReinitializationPolicy.NOT_ALLOWED;

    protected override void EndInitializationBehavior()
    {
        //
        gameObject.SetActive(false);
    }

    protected override void StartInitializationBehavior()
    {
        
        EndInitialize(true);
    }

}