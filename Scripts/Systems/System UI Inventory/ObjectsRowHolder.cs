using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsRowHolder : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsHolder;
    [SerializeField] private GameEvent descriptionEventCall;

    private List<ItemData> items;
    private int rowID;
    public void SetItems(List<ItemData> items, int rowID)
    {
        this.items = items;
        this.rowID = rowID;
        SetItemTOHolder();
    }

    private void SetItemTOHolder()
    {

        for(int index = 0; index < items.Count; index++)
        {
            if(index != 0 && rowID != 0)
            {
                objectsHolder[index].GetComponent<ObjectHolder>().AsignData(items[index], false);
            }
            else
            {
                objectsHolder[index].GetComponent<ObjectHolder>().AsignData(items[index], true);
            }
            
            objectsHolder[index].SetActive(true);
        }
    }
}
