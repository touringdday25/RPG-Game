using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using My.CharacterStats;

public class Character : MonoBehaviour
{
    public CharacterStats Strength;
    public CharacterStats Intelligence;
    public CharacterStats Vitality;
    public CharacterStats Armor;

    public Inventory pInventory
    {
        get { return inventory; }
    }

    public Skills pSkills
    {
        get { return skills; }
    }

    public ShopScript Shop
    {
        get { return currentShop; }
        set { currentShop = value; }
    }

    public bool ShopSet { get => shopSet; }

    [SerializeField] Combat combat;
    [SerializeField] Health playerHealth;
    [SerializeField] Skills skills;
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] ShopScript currentShop;
    [SerializeField] CraftingContainers craftingContainer;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemToolTip toolTip;
    [SerializeField] Image draggableItem;

    private BaseItemSlot draggedSlot;

    private RaycastHit hit;

    bool shopSet = false;

    private void OnValidate()
    {
        if (toolTip == null)
        {
            toolTip = FindObjectOfType<ItemToolTip>();
        }
    }

    private void Awake()
    {
        statPanel.SetStats(Strength, Intelligence, Vitality, Armor);
        statPanel.UpdateStatValues();

        //Set Up Events:
        //Right Click.
        inventory.OnRightClickEvent += InventoryRightClick;
        equipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
        //currentShop.OnRightClickEvent += BuyItem;
        //craftingContainer.OnRightClickEvent += CraftingContainerRightClick;
        //Pointer Enter.
        inventory.OnPointerEnterEvent += ShowToolTip;
        equipmentPanel.OnPointerEnterEvent += ShowToolTip;
        //Pointer Exit
        inventory.OnPointerExitEvent += HideToolTip;
        equipmentPanel.OnPointerExitEvent += HideToolTip;
        //Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        //currentShop.OnBeginDragEvent += BeginDrag;
        //craftingContainer.OnBeginDragEvent += BeginDrag;
        //End Drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        //currentShop.OnEndDragEvent += EndDrag;
        //craftingContainer.OnEndDragEvent += EndDrag;
        //Drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        //currentShop.OnDragEvent += Drag;
        //craftingContainer.OnDragEvent += Drag;
        //Drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
        //currentShop.OnDropEvent += SellItem;
        //craftingContainer.OnDropEvent += Drop;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3))
        {
            var hitObject = hit.transform.GetComponent<WorldItemScript>();
            if (hitObject && Input.GetKeyDown(KeyCode.E))
            {
                PickUpItem(hitObject.ItemToDrop, hitObject.ItemDropAmount);
                Destroy(hit.transform.gameObject);
            }
        }
    }

    public void SetCurrentShop()//should only run when shop is opened for the first time.
    {
        currentShop.OnRightClickEvent += Shop.BuyItem;
        shopSet = true;
    }

    public void InventoryRightClick(BaseItemSlot itemSlot)
    {
        if(currentShop != null)
        {
            Shop.SellItem(itemSlot);
            return;
        }
        else if (itemSlot.Item is EquipableItem)
        {
            Equip((EquipableItem)itemSlot.Item);
            combat.UpdateDamage();
            playerHealth.StatsUpdate();
        }

        /*
        else if(itemSlot.Item is UsableItem)
        {
            UsableItem usableItem = (UsableItem)itemSlot.Item;
            usableItem.Use(this);

            if (usableItem.IsConsumable)
            {
                itemSlot.Amount--;
                usableItem.Destroy();
            }
        }
        */
    }
    /*
    public void CraftingContainerRightClick(BaseItemSlot itemSlot)
    {
        if (itemSlot.isResultSlot)
        {
            Item tmpItem = itemSlot.Item;
            if (craftingContainer.RemoveItem(itemSlot.Item))
            {
                inventory.AddItem(tmpItem);
                
            }
        }
    }
    */

    public void EquipmentPanelRightClick(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquipableItem)
        {
            UnEquip((EquipableItem)itemSlot.Item);
            combat.UpdateDamage();
            playerHealth.StatsUpdate();
        }
    }

    public void ShowToolTip(BaseItemSlot itemSlot)
    {

        if (itemSlot.Item != null)
        {
            toolTip.ShowTooltip(itemSlot);
        }
    }

    public void HideToolTip(BaseItemSlot itemSlot)
    {
        toolTip.HideTooltip();
    }

    public void BeginDrag(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.itemIcon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    public void EndDrag(BaseItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    public void Drag(BaseItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    public void Drop(BaseItemSlot dropItemSlot)
    {
        if (draggedSlot == null) return;
        if (dropItemSlot.CanAddStack(draggedSlot.Item))
        {
            AddStacks(dropItemSlot);
        }
        else if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
        {
            SwapItems(dropItemSlot);

        }
    }

    private void SwapItems(BaseItemSlot itemSlot)
    {
        EquipableItem dragItem = draggedSlot.Item as EquipableItem;
        EquipableItem dropItem = itemSlot.Item as EquipableItem;
        //Needs Fixed Equipable Item Class.
        if (draggedSlot is EquipmentSlot)
        {
            if (dragItem != null) dragItem.UnEquip(this);
            if (dropItem != null) dropItem.Equip(this);
        }

        if (itemSlot is EquipmentSlot)
        {
            if (dragItem != null) dragItem.Equip(this);
            if (dropItem != null) dropItem.UnEquip(this);
        }
        statPanel.UpdateStatValues();

        Item draggedItem = draggedSlot.Item;
        int draggedAmount = draggedSlot.Amount;

        draggedSlot.Item = itemSlot.Item;
        draggedSlot.Amount = itemSlot.Amount;

        itemSlot.Item = draggedItem;
        itemSlot.Amount = draggedAmount;
    }

    private void AddStacks(BaseItemSlot itemSlot)
    {
        int numAddableStacks = itemSlot.Item.maxStack - itemSlot.Amount;
        int stacksToAdd = Mathf.Min(numAddableStacks, draggedSlot.Amount);

        itemSlot.Amount += stacksToAdd;
        draggedSlot.Amount -= stacksToAdd;
    }

    public void Equip(EquipableItem item)
    {
        if (inventory.RemoveItem(item))//if item is in inventory and can be removed
        {
            EquipableItem prevItem;
            if (equipmentPanel.AddItem(item, out prevItem))
            {
                if (prevItem != null)
                {
                    inventory.AddItem(prevItem);
                    prevItem.UnEquip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void UnEquip(EquipableItem item)
    {
        Debug.Log("Item Should have been removed and new item will be added.");
        if (inventory.CanAddItem(item) && equipmentPanel.RemoveItem(item))
        {
            item.UnEquip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }

    public void PickUpItem(Item itemToTake, int amount = 1)
    {
        if(itemToTake != null)
        {
            if (inventory.CanAddItem(itemToTake, amount))
            {
                inventory.AddItem(itemToTake, amount);
            }
        }
    }
}
