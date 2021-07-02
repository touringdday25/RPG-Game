using UnityEngine;
using System;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] KeyCode[] toggleInventoryKeys;
    [SerializeField] GameObject inventoryGameObject;
    [SerializeField] Movement playerMovement;
    [SerializeField] GameObject SkillingObjects;
    private bool invOpen = false;
    public event Action<bool> InvOpen = delegate { };

    private void Start()
    {
        
        inventoryGameObject.SetActive(false);
        //SkillingObjects.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                ToggleInventory();
                break;
            }
        }
    }

    public void ToggleInventory()
    {
        invOpen = !invOpen;
        if (invOpen)
        {
            ShowMouseCursor();
        }
        else
        {
            HideMouseCursor();
        }
        InvOpen(invOpen);
        inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
    }


    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
