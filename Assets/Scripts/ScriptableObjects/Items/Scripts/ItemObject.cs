using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ITEM_TYPE { FOOD,EQUIPMENT,DEFAULT}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ITEM_TYPE type;
    [TextArea(15,20)]
    public string description;
}
