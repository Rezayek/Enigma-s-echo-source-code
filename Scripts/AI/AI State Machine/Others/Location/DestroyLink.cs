using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLink : MonoBehaviour
{
    [SerializeField] private GameObject refObj;
    [SerializeField] private GameEvent frenzyCall;
    private void OnDestroy()
    {
        frenzyCall.Raise(this, new List<object> { States.Frenzy });
        Destroy(refObj);
    }
}
