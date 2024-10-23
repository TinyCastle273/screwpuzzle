using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class AudioManager : MonoManagerBase
{
    public override ReinitializationPolicy ReInitPolicy => ReinitializationPolicy.NOT_ALLOWED;

    protected override void EndInitializationBehavior()
    {
        //
    }

    protected override void StartInitializationBehavior()
    {
        EndInitialize(true);
    }
}