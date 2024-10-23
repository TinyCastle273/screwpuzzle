public enum ReinitializationPolicy
{
    NOT_ALLOWED = 0,
    ALLOW_ON_FAILED,
    ALLOW_ALL_TIME,
}
public enum InitializationState
{
    NOT_INITIALIZED = 0,
    INITIALIZING,
    SUCCESSFUL,
    FAILED
}

public interface IIntializable 
{
    public bool Usable { get; }

    public InitializationState State { get; }

    public ReinitializationPolicy ReInitPolicy { get; }

    public void Initialize();

}
