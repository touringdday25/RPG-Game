using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CreateAssetMenu(fileName = "New Store", menuName = "Store")]
public class ScriptableShop : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string shopname;
    [SerializeField] public Shoptype shoptype;
    //[SerializeField] Inventory playerInventory;
    [SerializeField] public Item Currency;
    [SerializeField] public List<ItemAmount> stock;
    [SerializeField] public List<ItemAmount> startingItems;

    protected static readonly StringBuilder sb = new StringBuilder();


    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
    public virtual ScriptableShop GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {

    }

    public virtual void SetStock()
    {
       stock = startingItems;
    }


}
