using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;

    [SerializeField] int itemsInChest = 1;
    [SerializeField] int itemsAddedPerUse = 1;
    [SerializeField]private bool isInRange;
    private bool isEmpty;

    private void Update()
    {
        if (isInRange && !isEmpty && Input.GetKeyDown(itemPickupKeyCode))
        {

            Item itemCopy = item.GetCopy();
            if (inventory.AddItem(itemCopy, itemsAddedPerUse))
            {

                itemsInChest--;
                if (itemsInChest <= 0)//Chest Has Become Empty
                {
                    isEmpty = true;
                }
            }
            else
            {
                itemCopy.Destroy();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckCollision(other.gameObject, false);
    }

    private void CheckCollision(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            isInRange = state;
            if (state == true)
            {
                inventory = gameObject.GetComponent<Character>().pInventory;
                Debug.Log(inventory);
            }
            else
            {
                inventory = null;
            }
        }
    }
}
