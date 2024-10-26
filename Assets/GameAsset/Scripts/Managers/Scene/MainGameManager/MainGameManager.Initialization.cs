using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public partial class MainGameManager : MonoManagerBase
{
    public override ReinitializationPolicy ReInitPolicy => ReinitializationPolicy.NOT_ALLOWED;

    protected override void EndInitializationBehavior()
    {
        // 
        //GM.Instance.Player.GetLevelState("" + currentLevelIndex, out var completion);

    }

    protected override void StartInitializationBehavior()
    {
        gameObject.SetActive(false);
        EndInitialize(true);
    }
}
