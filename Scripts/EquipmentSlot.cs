using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType equipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + " Slot";
    }

    public override bool CanReceiveItem(Item item)
    {
        if(item == null)
        {
            return true;
        }
        EquipableItem equipableItem = item as EquipableItem;
        Debug.Log("equipment Type:" + equipmentType + ", " + equipableItem);
        return equipableItem != null && equipableItem.equipmentType == equipmentType;
    }
}
