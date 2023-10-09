using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameEventListener))]
public class TorchSystem : MonoBehaviour
{
    [SerializeField] GameEvent torchUICall;
    [SerializeField] GameObject torchLight_1;
    [SerializeField] GameObject torchLight_2;
    public void TorchListener(Component sender , List<object> data)
    {
        if (data.Count != 1)
            return;
        if (data[0] is not TorchControl)
            return;
        TorchControl castEnum = (TorchControl)data[0];
        if (castEnum != TorchControl.PlayLogic)
            return;


        List<object> dataUI = new List<object>
            {
                TorchControl.PlayUI
            };
        torchUICall.Raise(this, dataUI);

        if (torchLight_1.activeSelf)
        {
            torchLight_1.SetActive(false);
            torchLight_2.SetActive(false);
        }
        else
        {
            torchLight_1.SetActive(true);
            torchLight_2.SetActive(true);
        }
        
    }
}
