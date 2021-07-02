using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : BaseItemSlot, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] ItemToolTip tooltip;

    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;

    private Color dragColor = new Color(1, 1, 1, 0.5f);

    Vector2 originalPosition;

    public override bool CanAddStack(Item item, int amount = 1)
    {
        //Debug.Log("item:" + item);
        return base.CanAddStack(item, amount) && Amount + amount <= item.maxStack;//Fifth Note in Null refrence shop glitch.

    }

    public override bool CanReceiveItem(Item item)
    {
        return true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(Item != null)
        {
            image.color = dragColor;
        }
        OnBeginDragEvent?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Item != null)
        {
            image.color = normalColor;
        }
        OnEndDragEvent?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke(this);
    }
}
