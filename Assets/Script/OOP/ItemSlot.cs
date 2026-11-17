using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int itemQuantity;
    public Sprite itemIcon;
    public bool isFull;
    public ItemDetails itemDetailsData;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private int maxStackSize = 99;

    [SerializeField] public GameObject selectedPanel;
    public bool isItemSelected;

    public InventoryManager inventoryManager;

    public void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    public void AddItem(ItemDetails item)
    {
        itemDetailsData = item;
        itemName = item.ItemName;
        itemIcon = item.ItemIcon;
        maxStackSize = item.maxStack;
        AddQuantity(item.Quantity);
    }

    public bool CanStackWith(string otherItemName)
    {
        return !isFull && itemName == otherItemName;
    }

    public void AddQuantity(int amount)
    {
        itemQuantity += amount;
        isFull = itemQuantity >= maxStackSize;
        
        quantityText.text = itemQuantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemIcon;
        itemImage.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick();
        }
    }

    private void LeftClick()
    {
        inventoryManager.DesselectAllSlot();
        selectedPanel.SetActive(true);
        isItemSelected = true;

        ItemDetailPanel panelKanan = FindAnyObjectByType<ItemDetailPanel>();
        if (panelKanan != null)
        {
            if (itemQuantity > 0 && itemDetailsData != null) 
            {
                panelKanan.Show(itemDetailsData, itemQuantity);
            } 
            else 
            {
                panelKanan.clear();
            }
        }
    }

   private void RightClick()
    {
        if (itemQuantity > 0 && itemDetailsData != null)
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        if (itemDetailsData.healAmount > 0)
        {
            PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
            if (player != null)
            {
                player.Heal(itemDetailsData.healAmount);
                KurangiJumlah(1);
            }
        }
    }

    private void KurangiJumlah(int amount)
    {
        itemQuantity -= amount;
        isFull = itemQuantity >= maxStackSize;

        if (itemQuantity <= 0)
        {
            ClearSlot();
        }
        else
        {
            quantityText.text = itemQuantity.ToString();

            ItemDetailPanel panelKanan = FindAnyObjectByType<ItemDetailPanel>();
            if (panelKanan != null && isItemSelected)
            {
                panelKanan.Show(itemDetailsData, itemQuantity);
            }
        }
    }

    private void ClearSlot()
    {
        itemDetailsData = null;
        itemName = "";
        itemQuantity = 0;
        itemIcon = null;
        isFull = false;

        quantityText.text = "";
        quantityText.enabled = false;
        itemImage.sprite = null;
        itemImage.enabled = false;

        ItemDetailPanel panelKanan = FindAnyObjectByType<ItemDetailPanel>();
        if (panelKanan != null && isItemSelected)
        {
            panelKanan.clear();
        }
    }
}