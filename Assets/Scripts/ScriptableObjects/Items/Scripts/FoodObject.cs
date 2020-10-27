using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory/Items/Food")]
public class FoodObject : ItemObject
{
    public float restoreHealthValue;
    public float restoreManaValue;

   private void Awake()
    {
        type = ITEM_TYPE.FOOD;
    } 
}
