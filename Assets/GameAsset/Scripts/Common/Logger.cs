using UnityEngine;

public class Logger
{

    public static bool ShouldLog = true;

    public static void Log(object message)
    {
        if (!ShouldLog) return;
        Debug.Log(message);
    }

    public static void Log(object Log, Object context)
    {
        if (!ShouldLog) return;
        Debug.Log(Log, context);
    }

    public static void Warn(object Log)
    {
        if (!ShouldLog) return;
        Debug.LogWarning(Log);
    }

    public static void Warn(object Logm, Object context)
    {
        if (!ShouldLog) return;
        Debug.LogWarning(Logm, context);
    }

    public static void Error(object Log)
    {
        if (!ShouldLog) return;
        Debug.LogError(Log);
    }

    public static void Error(object Log, Object context)
    {
        if (!ShouldLog) return;
        Debug.LogError(Log, context);
    }

}
