using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailPanel : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameText;

    [SerializeField] private TMP_Text itemQtyText;
    [SerializeField] private TMP_Text itemDescriptionText;

    public void Show(ItemDetails item,int quantity)
    {
        if (item == null)
        {
            clear();
            return;
        }

        itemIcon.sprite = item.ItemIcon;
        itemNameText.text = item.ItemName ;
        itemQtyText.text = "Quantity: " + quantity.ToString();
        itemDescriptionText.text = "Description: " + item.Description; 
    }

    public void clear()
    {
        itemIcon.sprite = null;
        itemNameText.text = "";
        itemQtyText.text = "";
        itemDescriptionText.text = "";
    }
}