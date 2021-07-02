using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingContainers : MonoBehaviour
{
    //[SerializeField] ItemSlot[] itemSlots;
    [SerializeField] Character myPlayer;
    [SerializeField] Transform containerParent;
    [SerializeField]private CraftingRecipe currentCraftingRecipe;
    [SerializeField] CraftingRecipe[] craftingRecipes;
    public CraftingRecipe CurrentCraftingRecipe
    {
        get { return currentCraftingRecipe; }
        set { currentCraftingRecipe = value; }
        //set { SetCraftingRecipe(value); }
    }

    /*
    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;
    */

    private void Awake()
    {
        /*
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }
        */
    }

    private void OnValidate()
    {
        //itemSlots = containerParent.GetComponentsInChildren<ItemSlot>();
    }

    private void Update()
    {
        /*
        if((itemSlots[0].Item != null && currentCraftingRecipe != null) && currentCraftingRecipe.CanCraft(this))
        {
            currentCraftingRecipe.Craft(this);
        }
        else if(itemSlots[0].Item != null)
        {
            currentCraftingRecipe = SelectRecipe(craftingRecipes);
        }
        */
    }

    public CraftingRecipe SelectRecipe(CraftingRecipe[] craftingRecipes)
    {
        for (int i = 0; i < craftingRecipes.Length; i++)
        {
            if (craftingRecipes[i].CanCraft(this))
            {
                return craftingRecipes[i];
            }
        }
        return null;
    }

    public virtual int ItemCount(string itemID)
    {
        int number = 0;

        for (int i = 0; i < myPlayer.pInventory.itemSlots.Count; i++)
        {
            Item item = myPlayer.pInventory.itemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
                number += myPlayer.pInventory.itemSlots[i].Amount;
            }
        }
        return number;
    }

    public virtual bool AddItem(Item item)
    {
        for (int i = 0; i < myPlayer.pInventory.itemSlots.Count; i++)
        {
            if (myPlayer.pInventory.itemSlots[i].CanAddStack(item) || myPlayer.pInventory.itemSlots[i].Item == null)
            {
                myPlayer.pInventory.itemSlots[i].Item = item;
                myPlayer.pInventory.itemSlots[i].Amount++;
                return true;
            }

        }

        return false;
    }


    public virtual Item RemoveItem(string itemID)//Needs a rework
    {
        //Debug.Log(itemID + " " + myPlayer.pInventory.itemSlots.Count);
        for (int i = 0; i < myPlayer.pInventory.itemSlots.Count; i++)
        {
            
            if (myPlayer.pInventory.itemSlots[i].Item != null && itemID == myPlayer.pInventory.itemSlots[i].Item.ID)//Check to see if slot is not null and if item id matches slot item id
            {
                //Debug.Log("ItemId Match" + i);
                myPlayer.pInventory.itemSlots[i].Amount--;
                return myPlayer.pInventory.itemSlots[i].Item;
            }
            
        }
        //Debug.Log("All Slots Checked. Or Are They?");
        return null;
    }
    /*
    public virtual bool RemoveItem(Item item)
    {
        if (item != null && itemSlots[2].Item == item)
        {
            itemSlots[2].Amount--;
            return true;
        }
        return false;
    }
    */
    public void Craft()
    {
        if (currentCraftingRecipe.CanCraft(this) && myPlayer.pSkills.HasLevel(currentCraftingRecipe.skill, (int)currentCraftingRecipe.requiredLevel))
        {
            foreach (ItemAmount itemAmount in currentCraftingRecipe.materials)
            {
                for (int i = 0; i < itemAmount.amount; i++)
                {
                    //Debug.Log(itemAmount.item.ID + " :" + itemAmount.item);
                    RemoveItem(itemAmount.item.ID);
                    //Destroy(oldItem);
                }
            }

            foreach (ItemAmount itemAmount in currentCraftingRecipe.Results)
            {
                for (int i = 0; i < itemAmount.amount; i++)
                {
                    AddItem(itemAmount.item.GetCopy());
                }
            }
            myPlayer.pSkills.CalculateNextLevel(currentCraftingRecipe.skill, currentCraftingRecipe.expToGain);
            //Add XP To Gain Here
            //expToGain
        }
        else if(currentCraftingRecipe.CanCraft(this) && !myPlayer.pSkills.HasLevel(currentCraftingRecipe.skill, (int)currentCraftingRecipe.requiredLevel))
        {
            Debug.Log("You do not meet the " + currentCraftingRecipe.skill + " level to craft. Level Needed " + currentCraftingRecipe.requiredLevel + ".");
        }
    }
}
