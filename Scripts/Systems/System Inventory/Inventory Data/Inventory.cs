using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game Data/Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemData> itemsData;

    [SerializeField] private List<ItemData> initObjects;
    public void InitInventory()
    {
        itemsData = new List<ItemData>();
        foreach(ItemData item in initObjects)
        {
            itemsData.Add(item);
        }
    }
    public void AddItem(ItemData item)
    {
        itemsData.Add(item);
    }

    public void RemoveItem(ItemData item)
    {
        itemsData.Remove(item);
        
    }

    public ItemData GetConsummable(ConsummableType consummableType)
    {
        return itemsData.Find(item => item.consummableType == consummableType);

    }
    public ItemData GetObjectByType(ObjectType objectType)
    {
        return itemsData.Find(item => item.objectType == objectType);
    }
    public ItemData GetObjectByIndex(int index)
    {
        return itemsData[index];
    }

    public List<ItemData> GetObjects(ObjectType objectType)
    {
        return itemsData.FindAll(item => item.objectType == objectType);
    }

    public bool HasTheItem(ItemData item)
    {
        if (itemsData.IndexOf(item) != -1)
            return true;
        return false;
    }
}
