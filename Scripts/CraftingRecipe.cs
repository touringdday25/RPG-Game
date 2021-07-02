using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public Item item;
    [Range(1, 2000000000)]
    public int amount;

    public ItemAmount(Item Item, int Amount = 1)
    {
        item = Item;
        amount = Amount;
    }

    public ItemAmount GetCopy()
    {
        return this;
    }

    public void Destroy()
    {

    }
}

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe", order = 3)]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> materials;
    public List<ItemAmount> Results;
    public SkillList skill;
    public uint requiredLevel;
    public uint expToGain;

    public bool CanCraft(CraftingContainers itemContainer)
    {
        return HasMaterials(itemContainer);
    }

    

    private bool HasMaterials(CraftingContainers itemContainer)
    {
        foreach (ItemAmount itemAmount in materials)
        {
            if (itemContainer.ItemCount(itemAmount.item.ID) < itemAmount.amount)
            {
                Debug.LogWarning("Not Enough Materials!");
                return false;
            }
        }
        return true;
    }
    /*
    public void Craft(CraftingContainers itemContainer)
    {
        if (CanCraft(itemContainer))
        {
            foreach (ItemAmount itemAmount in materials)
            {
                for (int i = 0; i < itemAmount.amount; i++)
                {
                    Debug.Log(itemAmount.item.ID + " :" + itemAmount.item);
                    itemContainer.RemoveItem(itemAmount.item.ID);
                    //Destroy(oldItem);
                }
            }

            foreach (ItemAmount itemAmount in Results)
            {
                for (int i = 0; i < itemAmount.amount; i++)
                {
                    itemContainer.AddItem(itemAmount.item.GetCopy());
                }
            }
            
            //Add XP To Gain Here
            //expToGain
        }
    }
    */
}
