using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteractor : MonoBehaviour
{
    
    protected EnvActions action;

    public abstract EnvActions GetEvent();
}
