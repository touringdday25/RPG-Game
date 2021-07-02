
using System.Collections.Generic;
using UnityEngine;

public enum Shoptype { GeneralStore, WeaponShop, ArmourShop, PotionShop };

public class ShopScript : Inventory
{
    [SerializeField]Inventory playerInventory;
    [SerializeField]public Item Currency;
    private float restockTimer;
    [SerializeField]private float DefaultTime = 2500;
    [SerializeField]private List<ItemAmount> stock;
    [SerializeField]private ScriptableShop shop;
    [SerializeField]public Shoptype shoptype;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        shop.SetStock();
        StockShop();
        //stock = GetStartingItems;
    }

    private void OnEnable()
    {
        //StockShop();
    }

    private void Update()
    {
        if(restockTimer > 0)
        {
            restockTimer -= Time.deltaTime;
            Debug.Log("Time:" + restockTimer);
        }
        else
        {
            //SetStartingItems();
            StockShop();
            Debug.Log("Update Stock.");
            restockTimer = DefaultTime;
        }
    }

    //Checks the users inventory for the currency that is required to purchase items and makes sure you can afford the item.
    public bool HasGold(Item itemToBuy)
    {
        var pInv = playerInventory.itemSlots;
        for (int i = 0; i < pInv.Count; i++)
        {
            if(pInv[i].Item == Currency)
            {
                //Debug.Log(pInv[i].Item.itemName + " " + pInv[i].Amount);

                {
                    pInv[i].Amount -= itemToBuy.value;
                    return true;
                }
                
            }
        }
        return false;
    }

    //Creates the value of the item when selling to the shop.
    public int SellValue(Item item)
    {
        float sellValue = item.value;
        sellValue *= (float)0.70;
        return Mathf.RoundToInt(sellValue);

    }

    //This allows items to be sold to the shop.
    public void SellItem(BaseItemSlot itemSlot)
    {
            AddItem(itemSlot.Item);
            playerInventory.AddItem(Currency, SellValue(itemSlot.Item));
            playerInventory.RemoveItem(itemSlot.Item);
            //currentShop.UpdateStock();

    }

    //This allows items to be purchased from the store.
    public void BuyItem(BaseItemSlot itemSlot)
    {
        if (HasGold(itemSlot.Item))
        {
            playerInventory.AddItem(itemSlot.Item);
            itemSlot.Amount--;
            UpdateStock();
            //currentShop.UpdateStock();
        }
    }

    public void UpdateStock()
    {
        shop.stock.Clear();
        foreach (ItemSlot item in itemSlots)
        {

            shop.stock.Add(new ItemAmount(item.Item, item.Amount));
        }
        
    }

    public void StockShop()
    {
        Clear();
        foreach (ItemAmount item in shop.stock)
        {
            Debug.Log("Stock Shop item:" + item);
            if(item.item != null)
            {
                AddItem(item.item, item.amount);

            }
        }
    }

    public void ReStock()
    {
        stock.Clear();
        foreach (ItemSlot item in itemSlots)
        {

            stock.Add(new ItemAmount(item.Item, item.Amount));
        }

    }

}
