using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreHandler : MonoBehaviour
{
    [SerializeField] ShopScript myShop;
    [SerializeField] GameObject storeCanvas;
    [SerializeField] InventoryInput inventory;
    bool shopOpen = false;
    //bool shopSet = false;
    bool inRange = false;
    KeyCode openKey = KeyCode.E;

    private void OnValidate()
    {
        if (myShop == null)
        {
            myShop = GetComponent<ShopScript>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(openKey) && inRange)
        {
            OpenShop();//First Note in Null refrence shop glitch.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player)
        {
            player.Shop = myShop;
            if (!player.ShopSet)
            {
                player.SetCurrentShop();
                
            }
            inRange = true;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            if (storeCanvas.activeSelf)
            {
                storeCanvas.SetActive(false);

            }
            inRange = false;
            other.GetComponent<Character>().Shop = null;


        }
    }

    private void OpenShop()
    {
        shopOpen = !shopOpen;
        myShop.StockShop();
        inventory.ToggleInventory();
        storeCanvas.SetActive(shopOpen);
        
    }
}
