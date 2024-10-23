using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoManagerBase : MonoBehaviour, IIntializable
{
    public InitializationState State { get; private set; } = InitializationState.NOT_INITIALIZED;

    public bool Usable => State == InitializationState.SUCCESSFUL;

    public abstract ReinitializationPolicy ReInitPolicy { get; }

    protected virtual string LogName => GetType().Name;

    protected readonly LogObj Log = new LogObj();

    public void Initialize()
    {
        Log?.SetName(LogName);

        if (State == InitializationState.SUCCESSFUL && ReInitPolicy != ReinitializationPolicy.ALLOW_ALL_TIME)
        {
            Log?.Warn("Component is already has successful initialization " +
                    "(and not allowing reinitialization at all)");
            return;
        }
        else if (State == InitializationState.FAILED && ReInitPolicy == ReinitializationPolicy.NOT_ALLOWED)
        {
            Log?.Warn($"Component is already has failed initialization " +
                        "(and not allowing reinitialization.)");
            return;
        }
        else if (State == InitializationState.INITIALIZING)
        {
            Log?.Warn("Component is already initializing.");
            return;
        }

        Log?.Info("Initialization commerced...");
        State = InitializationState.INITIALIZING;
        StartInitializationBehavior();
    }

    protected void EndInitialize(bool status)
    {
        State = status ? InitializationState.SUCCESSFUL : InitializationState.FAILED;
        EndInitializationBehavior();
        Log?.Info($"Initialization completed with status {status}");
    }

    protected abstract void StartInitializationBehavior();
    protected abstract void EndInitializationBehavior();
}
