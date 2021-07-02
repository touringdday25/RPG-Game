using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
    public List<ItemSlot> itemSlots;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;

    protected virtual void OnValidate()
    {
        GetComponentsInChildren(includeInactive: true, result: itemSlots);
    }

    protected virtual void Awake()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].OnPointerEnterEvent += slot => EventHelper(slot, OnPointerEnterEvent);
            itemSlots[i].OnPointerExitEvent += slot => EventHelper(slot, OnPointerExitEvent);
            itemSlots[i].OnRightClickEvent += slot => EventHelper(slot, OnRightClickEvent);
            itemSlots[i].OnBeginDragEvent += slot => EventHelper(slot, OnBeginDragEvent);
            itemSlots[i].OnEndDragEvent += slot => EventHelper(slot, OnEndDragEvent);
            itemSlots[i].OnDragEvent += slot => EventHelper(slot, OnDragEvent);
            itemSlots[i].OnDropEvent += slot => EventHelper(slot, OnDropEvent);
        }
    }

    private void EventHelper(BaseItemSlot itemSlot, Action<BaseItemSlot> action)
    {
        if(action != null)
        {
            action(itemSlot);
        }
    }

    public virtual bool CanAddItem(Item item, int amount = 1)
    {
        int freeSpaces = 0;

        foreach (ItemSlot itemSlot in itemSlots)
        {
            if(itemSlot.Item == null || itemSlot.Item.ID == item.ID)
            {
                freeSpaces += item.maxStack - itemSlot.Amount;
                //Debug.Log(itemSlot.Item.name);
            }
        }
        return freeSpaces >= amount;
    }

    public virtual bool AddItem(Item item, int amount = 1)
    {
        //Debug.Log(item);
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].CanAddStack(item, amount))//Fourth Note in Null refrence shop glitch.
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount += amount;
                return true;
            }
        }

        for (int i = 0; i < itemSlots.Count; i++)
        {
            if(itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount += amount;
                return true;
            }
        }
        return false;
    }


    public virtual int ItemCount(string itemID)
    {
        int number = 0;

        for (int i = 0; i < itemSlots.Count; i++)
        {
            Item item = itemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
                number += itemSlots[i].Amount;
            }
        }
        return number;
    }

    public virtual Item RemoveItem(string itemID, int amount = 1)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            Item item = itemSlots[i].Item;
            if(item != null && item.ID == itemID)
            {
                itemSlots[i].Amount -= amount;
                return item;
            }
        }
        return null;
    }

    public virtual bool RemoveItem(Item item, int amount = 1)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if(itemSlots[i].Item == item)
            {
                itemSlots[i].Amount -= amount;
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if(itemSlots[i].Item != null && Application.isPlaying)
            {
                itemSlots[i].Item.Destroy();
            }
            itemSlots[i].Item = null;
            itemSlots[i].Amount = 0;
        }
    }
}
