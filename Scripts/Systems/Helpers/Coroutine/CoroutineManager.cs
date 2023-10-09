using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private List<Coroutine> runningCoroutines = new List<Coroutine>();

    public void StartNewCoroutine(IEnumerator coroutine)
    {
        Coroutine newCoroutine = StartCoroutine(TrackCoroutine(coroutine));
        runningCoroutines.Add(newCoroutine);
    }

    private IEnumerator TrackCoroutine(IEnumerator coroutine)
    {
        yield return StartCoroutine(coroutine);
        RemoveCoroutineFromList(coroutine);
    }

    private void RemoveCoroutineFromList(IEnumerator coroutine)
    {
        Coroutine foundCoroutine = null;
        foreach (Coroutine c in runningCoroutines)
        {
            if (c.ToString() == coroutine.ToString())
            {
                foundCoroutine = c;
                break;
            }
        }

        if (foundCoroutine != null)
            runningCoroutines.Remove(foundCoroutine);

        if (runningCoroutines.Count == 0)
            Debug.Log("All coroutines finished"); // Optional: Add any additional actions when all coroutines finish here.
    }

    public bool AreAnyCoroutinesRunning()
    {
        return runningCoroutines.Count > 0;
    }

}