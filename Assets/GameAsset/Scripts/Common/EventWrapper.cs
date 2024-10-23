using System;
using UnityEngine.Events;

[Serializable]
public class EventWrapper
{
    public event Action FunctionalEvent;
    public UnityEvent UnityEvent;

    public void Invoke()
    {
        FunctionalEvent?.Invoke();
        UnityEvent?.Invoke();
    }
    public static EventWrapper operator +(EventWrapper o, Action e)
    {
        o.FunctionalEvent += e;
        return o;
    }

    public static EventWrapper operator -(EventWrapper o, Action e)
    {
        o.FunctionalEvent -= e;
        return o;
    }
}