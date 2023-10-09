using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameEvent gameNotifier;
    [SerializeField] private GameEvent inspector;
    [SerializeField] private GameEvent consumme;
    [SerializeField] private GameEvent placeObjectifs;
    [SerializeField] private GameEvent inventoryUICall;
    [SerializeField] private GameEvent puzzleCall;


    private void Start()
    {
        inventory.InitInventory();
    }
    public void AddItem(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;

        if (data[0] is not RequestsType && data[1] is not ItemData)
            return;

        RequestsType castEnum = (RequestsType)data[0];
        if (castEnum is not RequestsType.AddItem)
            return;

        ItemData itemData = (ItemData)data[1];
        Debug.Log("<color=green> Call Add Item </color>");
        inventory.AddItem(itemData);

        //Notify Call
        List<object> notifierData = new List<object>
        {
            NotifierMessageType.ItemAdd,
            itemData.itemName,
            itemData.sprite
        };

        
        gameNotifier.Raise(this, notifierData);
    }

    public void GetKey(Component sender, List<object> data)
    {
        //TODO: Potential Bug
        if (data.Count != 2)
            return;
        if (data[0] is not EnvActions.KeyAction)
            return;
        if (!inventory.HasTheItem((ItemData)data[1]))
            return;
        RemoveItem((ItemData)data[1], NotifierMessageType.ItemPlace);
        puzzleCall.Raise(this, new List<object> { sender, EnvActions.KeyAction, true});

    }

    public void GetPuzzelItem(Component sender, List<object> data)
    {
        if (data.Count != 3)
            return;
        if (data[0] is not EnvActions.PuzzelAction)
            return;
        if (!inventory.HasTheItem((ItemData)data[1]))
            return;
        RemoveItem((ItemData)data[1], NotifierMessageType.ItemPlace);
        puzzleCall.Raise(this, new List<object> { data[2], EnvActions.PuzzelAction, true});
    }


    public void RequestInspect(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;

        if (data[0] is not RequestsType)
            return;

        if(data[1] is not ItemData)
            return;

        RequestsType castEnum = (RequestsType)data[0];
        if (castEnum is not RequestsType.InspectItem)
        {
            return;
        }
        else
        {
            Debug.Log("<color=green> Call Request Inspect </color>");
            Debug.Log("<color=green> sender </color>" + sender);
            Debug.Log("<color=green> (ItemData)data[1] </color>" + (ItemData)data[1]);
            Debug.Log("<color=green> castEnum </color>" + castEnum);
            ItemData itemData = (ItemData)data[1];

            //TODO: Add Inspector Call
            List<object> inspectorData = new List<object>{
            itemData
            };

            inspector.Raise(this, inspectorData);
        } 

    }

    public void RequestPlacement(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;

        if (data[0] is not RequestsType && data[1] is not ObjectType)
            return;

        RequestsType castEnum1 = (RequestsType)data[0];
        if (castEnum1 is not RequestsType.PlaceItem )
            return;

        ObjectType castEnum2 = (ObjectType)data[1];
        if (castEnum2 is not ObjectType.Artifact)
            return; 
        
        Debug.Log("<color=green> Call Request Placement </color>");

        try
        {
            ItemData itemData = inventory.GetObjectByType(castEnum2);

            //Add Place Call

            List<object> itemDataPlace = new List<object>
            {
                RequestsType.PlaceItem,
                itemData,
            };

            placeObjectifs.Raise(this, itemDataPlace);

            //Add Notifier Remove Call
            RemoveItem(itemData, NotifierMessageType.ItemPlace);
        }
        catch
        {
            return;
        }

    }

    public void RequestConsumme(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;

        if (data[0] is not RequestsType && data[1] is not ConsummableType)
            return;

        RequestsType castEnum1 = (RequestsType)data[0];

        if (castEnum1 is not RequestsType.ConsummeItem)
            return;
        ConsummableType castEnum2 = (ConsummableType)data[1];
        


        try
        {
            ItemData itemData = inventory.GetConsummable(castEnum2);

            //TODO: Add Consumme Call
            List<object> consummeData = new List<object>
            {
                castEnum2,
                itemData.consummeGain,
            };
            consumme.Raise(this, consummeData);

            //Add Notifier Remove Call
            if (ConsummableType.Health == castEnum2)
            {
                Debug.Log("<color=green> Call Request Health Consumme </color>");
                RemoveItem(itemData, NotifierMessageType.ItemHealthConsumme);
            }
            else if (ConsummableType.Sanity == castEnum2)
            {
                Debug.Log("<color=green> Call Request Sanity Consumme </color>");
                RemoveItem(itemData, NotifierMessageType.ItemSanityConsumme);
            }
            

        }
        catch
        {
            return;
        }


    }

    public void RequestItems(Component sender, List<object> data)
    {
        Debug.Log("<color=green> Call Request Items </color>");
        if (data.Count != 1)
            return;

        if (data[0] is not RequestsType)
            return;
        RequestsType castEnum1 = (RequestsType)data[0];

        if (castEnum1 is not RequestsType.AllItems)
            return;

        Debug.Log("<color=green> Call Request Items </color>");

        try
        {
            List<ItemData> items = inventory.itemsData;
            //TODO: Add Inventory Desplay Call

            List<object> itemsData = new List<object>
            {
                items
            };
            inventoryUICall.Raise(this, itemsData);

        }
        catch
        {
            Debug.Log("<color=green> Something went wrong </color>");
        }

        



        

    }

    private void RemoveItem(ItemData itemData, NotifierMessageType notifierMessageType)
    {
        inventory.RemoveItem(itemData);

        //Notify Call
        List<object> notifierData = new List<object>
        {
            notifierMessageType,
            itemData.itemName,
            itemData.sprite
        };


        gameNotifier.Raise(this, notifierData);
    }

}



