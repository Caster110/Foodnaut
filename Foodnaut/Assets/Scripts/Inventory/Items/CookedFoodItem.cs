using UnityEngine;

[CreateAssetMenu(fileName = "Cooked Food Item", menuName = "Inventory/New Cooked Food Item")]
public class CookedFoodItem : ItemScriptableObject
{
    [SerializeField] private int grade;
}
