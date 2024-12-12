using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Add/Item")]
public class ItemAttribute : ScriptableObject
{
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
    [SerializeField] private float mItemPrice;
    public float ItemPrice
    {
        get
        {
            return mItemPrice;
        }
    }
}