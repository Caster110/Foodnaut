using UnityEngine;

public enum ItemType {CookedFood, ExpirationableIngredientItem, UnexpirationableIngredientItem, Instrument };
public class ItemScriptableObject : ScriptableObject
{
    public Sprite icon;
    protected ItemType itemType; //private?
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private string _name;
}
