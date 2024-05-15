using UnityEngine;
public enum ItemType {CookedFood, ExpirationableIngredient, UnexpirationableIngredient, Instrument, KitchenAppliance};
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private string _name;
    [SerializeField] private ItemType itemType; 
    public Sprite icon;
    public GameObject itemPrefab;
}
