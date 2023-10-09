using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [SerializeField] private ItemData item;

    public ItemData GetData
    {
        get
        {
            return item;
        }
    }
}
