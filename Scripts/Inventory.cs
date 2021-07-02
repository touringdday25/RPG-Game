using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : ItemContainer
{
    
    [SerializeField] List<ItemAmount> startingItems;
    [SerializeField] Transform itemsParent;

    protected override void OnValidate()
    {
        if(itemsParent != null)
        {
            itemsParent.GetComponentsInChildren(includeInactive: true, result: itemSlots);
        }

        if (!Application.isPlaying)
        {
            SetStartingItems();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        SetStartingItems();
    }

    protected void SetStartingItems()
    {
        Clear();
        foreach (ItemAmount item in startingItems)
        {
            if(item.item != null)
            {
                AddItem(item.item, item.amount);

            }
        }
    }

    protected List<ItemAmount> GetStartingItems
    {
        get { return startingItems; }
    }
}
