using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game Data/Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Category category;
    public ObjectType objectType;
    public ConsummableType consummableType;
    public int Qte = 1;
    public Sprite sprite;
    public GameObject itemPrefab;
    [Range(1,50f)]
    public int consummeGain;
    public Vector3 itemInspectorScale;
    [TextArea(3,20)]
    public string itemDescription;
    public List<Pages> pages;


    
}

[System.Serializable]
public class Pages
{
    [TextArea(3, 50)]
    public string text;
}



