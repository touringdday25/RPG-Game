using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemQuantityText;
    [SerializeField] Text itemTypeText;
    [SerializeField] Text itemDescriptionText;
    [SerializeField] Text itemValueText;
    /*
    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            gameObject.transform.position = Input.mousePosition;

        }
    }
    */
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void ShowTooltip(BaseItemSlot itemSlot)
    {
        
        var item = itemSlot.Item;
        var quantity = itemSlot.Amount;
        itemNameText.text = item.itemName;
        itemQuantityText.text = "Quantity: " + quantity.ToString();
        itemTypeText.text = item.GetItemType();
        itemDescriptionText.text = item.GetDescription();
        itemValueText.text = item.GetItemValue() + " * " + quantity.ToString();
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

}
