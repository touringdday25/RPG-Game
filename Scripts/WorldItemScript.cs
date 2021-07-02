using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemScript : MonoBehaviour
{
         
    [SerializeField]SpriteRenderer itemImage;
    [SerializeField]Item droppedItem;
    int amount = 1;
    int size = 1;
    Vector2 newSize = new Vector2(1 , 1);
 

    private void OnValidate()
    {
        itemImage = GetComponent<SpriteRenderer>();
    }


    private void LateUpdate()
    {

        transform.LookAt(Camera.main.transform);
    }

    public void SetImage()
    {
        itemImage.sprite = droppedItem.itemIcon;
        itemImage.size = newSize;

    }

    public Item ItemToDrop { get { return droppedItem; } set { droppedItem = value; } }
    public int ItemDropAmount { get { return amount; } set { amount = value; } }
}
