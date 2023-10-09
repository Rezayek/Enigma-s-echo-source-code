using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameEventListener))]
public class TorchUISystem : MonoBehaviour
{
    [SerializeField] private GameObject torchLight;
    public void TorchDisplayController(Component sender, List<object> data)
    {
        if (data.Count != 1)
            return;
        if (data[0] is not TorchControl)
            return;
        TorchControl castEnum = (TorchControl)data[0];
        if (castEnum != TorchControl.PlayUI)
            return;

        if(torchLight.activeSelf)
        {
            torchLight.SetActive(false);
        }
        else
        {
            torchLight.SetActive(true);
        }
    }
}
