using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Add/Item")]
public class ItemAttribute : ScriptableObject
{
    [Header("ID")]
    [SerializeField] private int mItemID;
    public int ItemID
    {
        get
        {
            return mItemID;
        }
    }

    [Header("Name")]
    [SerializeField] private string mItemName;
    public string ItemName
    {
        get
        {
            return mItemName;
        }
    }

    [Header("Sprite")]
    [SerializeField] private Sprite mItemImage;
    public Sprite ItemImage
    {
        get
        {
            return mItemImage;
        }
    }

    [Header("Price")]
    [SerializeField] private int mItemPrice;
    public int ItemPrice
    {
        get
        {
            return mItemPrice;
        }
    }
}