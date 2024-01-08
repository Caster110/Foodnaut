using UnityEngine;

[CreateAssetMenu(fileName = "Cooked Food Item", menuName = "Game Items/New Cooked Food Item")]
public class CookedFoodItem : ItemScriptableObject
{
    [SerializeField] private int grade;
    //[SerializeField] private int timeTo; время до просрочки
}
