using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DroppedItem
{
    public Item item;
    public int amount;
    public int dropChance;
    public bool excludeMaxDrop;

}


public class Drops : MonoBehaviour
{
    [SerializeField]private DroppedItem[] myDrops;
    [SerializeField]private int maxDrops;
    [SerializeField] private GameObject itemPrefab;
    

    private void OnValidate()
    {
        itemPrefab = Resources.Load("Prefabs/ItemDrop", typeof(GameObject)) as GameObject;
        //OrderDrops(myDrops);
    }

    private void Start()
    {
        OrderDrops(myDrops);
    }

    public void RollDrop()
    {
        int drops = 0;
        foreach (DroppedItem item in myDrops)
        {
            if(drops < maxDrops || item.excludeMaxDrop)
            {
                int random = UnityEngine.Random.Range(1, item.dropChance);
                if(random == 1)
                {
                    //Drop Item.
                    var drop = Instantiate(itemPrefab, transform.position, transform.rotation);
                    drop.GetComponent<WorldItemScript>().ItemToDrop = item.item;
                    drop.GetComponent<WorldItemScript>().ItemDropAmount = item.amount;
                    drop.GetComponent<WorldItemScript>().SetImage();
                    if (!item.excludeMaxDrop)
                    {
                        drops++;

                    }
                }

            }
        }
    }

    private void OrderDrops(DroppedItem[] drops)
    {
        bool sorted = false;
        while (!sorted)
        {
            sorted = true;
            for (int i = 0; i < drops.Length - 1; i++)
            {
                if(drops[i].dropChance < drops[i + 1].dropChance)
                {
                    DroppedItem tempDrop = drops[i];
                    drops[i] = drops[i + 1];
                    drops[i + 1] = tempDrop;
                    sorted = false;
                }
            }
        }
        myDrops = drops;
    }

}
