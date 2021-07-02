using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceToggle : MonoBehaviour
{
    [SerializeField] private GameObject uInterface;//Interface For Crafting
    [SerializeField] private InventoryInput invInput;//Inventory controller Script
    //[SerializeField] private Character player;//Player Object in World
    [SerializeField] private KeyCode key;//Key to be Pressed
    private bool inRange = false;


    //This allows the user to trigger the Interfaces
    private void Update()
    {
        if(inRange && Input.GetKeyDown(key))
        {
            uInterface.SetActive(!uInterface.activeSelf);
            invInput.ToggleInventory();
        }
    }

    //This will let the trigger system know that the user has entered the area of the interface.
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<Character>())
        {
            inRange = true;
        }
    }

    //This will let the trigger system know that the user has left the area of the interface.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Character>())
        {
            inRange = false;
        }
    }
}
