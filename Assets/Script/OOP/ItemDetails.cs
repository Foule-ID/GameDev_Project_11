using UnityEngine;

[CreateAssetMenu(fileName = "New Item Details", menuName = "Inventory/Item Details")]
public class ItemDetails : ScriptableObject
{
    public string ItemName;
    public Sprite ItemIcon;
    public int Quantity = 1;
    [SerializeField,TextArea] private string ItemDescription;
    
    public string Description => ItemDescription;
}