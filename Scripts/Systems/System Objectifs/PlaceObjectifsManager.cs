using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class objectifsHolder
{
    public Transform transform;
    public bool hasObjectif;
}


[RequireComponent(typeof(GameEventListener))]
public class PlaceObjectifsManager : MonoBehaviour
{

    
    [SerializeField] private List<objectifsHolder> objectifsHolders;
    [SerializeField] private GameEvent switchStateExternalCall;
    [SerializeField] private Enemy enemy;
    public void ItemPlaceListener(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;
        if (data[0] is not RequestsType && data[1] is not ItemData)
            return;

        RequestsType castenum = (RequestsType)data[0];
        if (castenum is not RequestsType.PlaceItem)
            return;

        ItemData item = (ItemData)data[1];
        for (int i = 0; i < objectifsHolders.Count; i++)
        {
            if(objectifsHolders[i].hasObjectif == false)
            {
                Debug.Log("<color=green> Call Request Placement Has been recieved and executed</color>");
                // Instantiate the prefab
                GameObject instantiatedObject = Instantiate(item.itemPrefab, objectifsHolders[i].transform.position, Quaternion.identity);
                instantiatedObject.tag = "Untagged";
                // Set the parent of the instantiated object to null
                instantiatedObject.transform.parent = null;
                objectifsHolders[i].hasObjectif = true;
                if (FinalSceneTrigger())
                {
                    switchStateExternalCall.Raise(this, new List<object> { enemy, States.Death });
                }
                return;
            }
        }

        
    }


    private bool FinalSceneTrigger()
    {
        foreach(objectifsHolder obj in objectifsHolders)
        {
            if(!obj.hasObjectif)
            {
                return false;
            }
        }
        return true;
    }
}
