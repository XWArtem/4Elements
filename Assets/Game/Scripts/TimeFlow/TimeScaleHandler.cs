using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleHandler : ITimeScaleHandler
{
    private readonly List<ITimeScaleHandler> _handlers = new List<ITimeScaleHandler>();

    public float CustomTimeScale { get; private set; }

    public void Register(ITimeScaleHandler handler)
    {
        _handlers.Add(handler);
    }

    public void UnRegister(ITimeScaleHandler handler)
    {
        _handlers.Remove(handler);
    }

    public void SetTimeScale(float newValue)
    {
        CustomTimeScale = newValue;
        Time.timeScale = CustomTimeScale;
    }
}
