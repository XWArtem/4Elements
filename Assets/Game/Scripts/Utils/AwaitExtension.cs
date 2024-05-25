using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class AwaitExtension
{
    public static void ActionDelayed(this MonoBehaviour mono, float delay, UnityAction action)
    {
        mono.StartCoroutine(ExecuteAction(delay, action));
    }

    public static void ActionDelayedRealtime(this MonoBehaviour mono, float delay, UnityAction action)
    {
        mono.StartCoroutine(ExecuteActionRealtime(delay, action));
    }

    private static IEnumerator ExecuteAction(float delay, UnityAction action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
        yield break;
    }

    private static IEnumerator ExecuteActionRealtime(float delay, UnityAction action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
        yield break;
    }
}