using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour
{
    [Header("Trade")]
    [SerializeField] private ItemAttribute item;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    private void Start()
    {
        GenerateTradeUI();
    }

    void GenerateTradeUI()
    {
        itemImage.sprite = item.ItemImage;
        itemName.text = item.ItemName;
        itemPriceText.text = $"{item.ItemPrice}G";
    }
}
