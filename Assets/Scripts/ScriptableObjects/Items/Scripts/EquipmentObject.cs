using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public float strBonus;
    public float intBonus;
    public float agiBonus;
    public float stamBonus;
    public float defenceBonus;

    private void Awake()
    {
        type = ITEM_TYPE.EQUIPMENT;
    }
}
