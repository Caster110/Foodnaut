using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Recipe
{
    public List<string> ingredients; // Список названий спрайтов ингредиентов
    public string result; // Название спрайта результата крафта
}

public class CraftingManager : MonoBehaviour
{
    public List<Recipe> recipes; // Список всех рецептов
    public InventorySlot resultSlot; // Ссылка на слот для результата крафта
    public Button craftButton; // Кнопка для начала крафта
    // Словарь для хранения соответствия названий спрайтов и данных предметов
    [SerializeField] private Dictionary<string, ItemScriptableObject> itemsDictionary = new Dictionary<string, ItemScriptableObject>();
    public ItemScriptableObject rawPotatoData; 
    public ItemScriptableObject rawMeatData; 
    public ItemScriptableObject slicedMeatData; 
    public ItemScriptableObject knifeData; 
    public ItemScriptableObject carrotData; 
    public ItemScriptableObject slicedCarrotData; 
    public ItemScriptableObject cleanPotatoData; 
    public ItemScriptableObject onionData; 
    public ItemScriptableObject slicedOnionData; 
    public ItemScriptableObject tomatoData; 
    public ItemScriptableObject slicedTomatoData; 
    public ItemScriptableObject cabbageData; 
    public ItemScriptableObject slicedCabbageData; 
    public ItemScriptableObject oilData; 
    public ItemScriptableObject saltData; 
    public ItemScriptableObject saladData; 
    public ItemScriptableObject plateData; 
    public Animator arrowAnimator;

    // Метод для начала крафта
    public void Craft()
    {
        List<string> ingredients = new List<string>(); // Список ингредиентов, которые сейчас в слотах
        for (int i = 1; i <= 12; i++) // Проходим по всем слотам
        {
            GameObject slot = GameObject.Find("Slot_Image" + i);
            Image itemImage = slot.transform.Find("Item_Image").GetComponent<Image>();
            if (itemImage.sprite != null)
            {
                ingredients.Add(itemImage.sprite.name); // Добавляем название спрайта в список ингредиентов
            }
        }

        string ingredientList = string.Join(", ", ingredients); // Собираем все ингредиенты в одну строку для вывода
        Debug.Log("Ингредиенты в слотах: " + ingredientList); // Выводим список всех найденных ингредиентов

        foreach (Recipe recipe in recipes) // Проверяем каждый рецепт
        {
            if (RecipeMatches(ingredients, recipe) && (resultSlot.GetData() == null)) // Если ингредиенты соответствуют рецепту
            {
                StartCoroutine(PerformCraft(recipe)); // Запускаем процесс крафта
                return;
            }
        }
        Debug.Log("Recipe not found."); // Рецепт не найден
    }

    // Корутина для выполнения крафта
    private IEnumerator PerformCraft(Recipe recipe)
    {
        craftButton.interactable = false; // Отключаем кнопку во время крафта
        // Здесь можно добавить анимацию стрелочки
        ClearIngredients(); // Очищаем ингредиенты
        arrowAnimator.SetTrigger("StartCrafting");
        yield return new WaitForSeconds(2); // Имитация процесса крафта
        arrowAnimator.SetTrigger("StopCrafting");

        // Получаем данные предмета по названию спрайта, который является результатом рецепта
        ItemScriptableObject resultItemData = GetItemDataByName(recipe.result);
        if (resultItemData != null)
        {
            // Устанавливаем данные предмета в слот результатов
            resultSlot.SetData(resultItemData);
        }
        else
        {
            Debug.LogWarning("Не удалось найти ItemScriptableObject для результата: " + recipe.result);
        }

        craftButton.interactable = true; // Включаем кнопку после крафта
    }
    
    private void ClearIngredients()
    {
        for (int i = 1; i <= 12; i++)
        {
            GameObject slotGameObject = GameObject.Find("Slot_Image" + i);
            if (slotGameObject != null)
            {
                InventorySlot slot = slotGameObject.GetComponent<InventorySlot>();
                if (slot != null)
                {
                    ItemScriptableObject itemData = slot.GetData();
                    if ((itemData != null) && (itemData.name != "Knife") && (itemData.name != "Oil") && (itemData.name != "Salt"))
                    {
                        slot.Clear(); 
                    }
                }
                else
                {
                    Debug.LogError("InventorySlot component not found on object: " + slotGameObject.name);
                }
            }
            else
            {
                Debug.LogError("Slot GameObject not found: Slot_Image" + i);
            }
        }
    }

    // Проверка соответствия рецепта списку ингредиентов
    private bool RecipeMatches(List<string> ingredients, Recipe recipe)
    {
        List<string> recipeIngredients = new List<string>(recipe.ingredients);
        foreach (string ingredient in ingredients)
        {
            if (recipeIngredients.Contains(ingredient))
            {
                recipeIngredients.Remove(ingredient);
            }
            else
            {
                return false; // Если ингредиент не часть рецепта, возвращаем false
            }
        }
        return recipeIngredients.Count == 0; // Если все ингредиенты использованы, возвращаем true
    }

    // Метод для получения данных предмета по названию спрайта
    private ItemScriptableObject GetItemDataByName(string name)
    {
        if (itemsDictionary.TryGetValue(name, out ItemScriptableObject itemData))
        {
            return itemData;
        }
        Debug.LogWarning("Item data not found for sprite name: " + name);
        return null;
    }

    private void Start()
    {
        arrowAnimator.SetTrigger("StopCrafting");
        
        // Предметы в словаре
        itemsDictionary.Add("RawPotato", rawPotatoData);
        itemsDictionary.Add("RawMeat", rawMeatData);
        itemsDictionary.Add("SlicedMeat", slicedMeatData);
        itemsDictionary.Add("Knife", knifeData);
        itemsDictionary.Add("Carrot", carrotData);
        itemsDictionary.Add("SlicedCarrot", slicedCarrotData);
        itemsDictionary.Add("CleanPotato", cleanPotatoData);
        itemsDictionary.Add("Onion", onionData);
        itemsDictionary.Add("SlicedOnion", slicedOnionData);
        itemsDictionary.Add("Tomato", tomatoData);
        itemsDictionary.Add("SlicedTomato", slicedTomatoData);
        itemsDictionary.Add("Cabbage", cabbageData);
        itemsDictionary.Add("SlicedCabbage", slicedCabbageData);
        itemsDictionary.Add("Oil", oilData);
        itemsDictionary.Add("Salt", saltData);
        itemsDictionary.Add("Salad", saladData);
        itemsDictionary.Add("Plate", plateData);

        // Рецепт "Нарезанное мясо"
        recipes.Add(new Recipe() { ingredients = new List<string> { "Knife", "RawMeat" }, result = "SlicedMeat" });
        // Рецепт "Нарезанная морковь"
        recipes.Add(new Recipe() { ingredients = new List<string> { "Knife", "Carrot" }, result = "SlicedCarrot" });
        // Рецепт "Очищенный картофель"
        recipes.Add(new Recipe() { ingredients = new List<string> { "Knife", "RawPotato" }, result = "CleanPotato" });
        // Рецепт "Нарезанный лук"
        recipes.Add(new Recipe() { ingredients = new List<string> { "Knife", "Onion" }, result = "SlicedOnion" });
        // Рецепт "Нарезанный помидор"
        recipes.Add(new Recipe() { ingredients = new List<string> { "Knife", "Tomato" }, result = "SlicedTomato" });
        // Рецепт "Нарезанная капуста"
        recipes.Add(new Recipe() { ingredients = new List<string> { "Knife", "Cabbage" }, result = "SlicedCabbage" });
        // Рецепт "Простой салат"
        recipes.Add(new Recipe() { ingredients = new List<string> { "Plate", "Oil", "Salt", "Sliced_Cabbage", "Sliced_Onion", "Sliced_Tomato" }, result = "Salad" });
    }
}