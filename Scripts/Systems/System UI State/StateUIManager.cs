using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateUIManager : MonoBehaviour
{

    public abstract void IncreaseListener(Component sender, List<object> data);
    public abstract void DecreaseListener(Component sender, List<object> data);
    public abstract void PlayeImmersiveState();

}
