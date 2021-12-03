using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IngredientMenu : MonoBehaviour
{
    public List<KeyValuePair<Ingredient, int>> ingredients;
    [SerializeField] private VisualTreeAsset ingredientTemplate;
    private int ingredientIndex = 0; 

    public void refreshUI()
    {
        ingredients = IngredientManager.Ingredients.ingredientInventory;
        var root = GetComponent<UIDocument>().rootVisualElement;
        var ingredientContainer = root.Q<VisualElement>("ingredientsContainer");

        //Set onclick handlers
        var menuExitButton = root.Q<Button>("MenuExitButton");
        menuExitButton.clicked += hideIngredientMenu;

        var ingredientExitButton = root.Q<Button>("ViewExitButton");
        ingredientExitButton.clicked += hideIngredientView;

        var nextIngredientButton = root.Q<Button>("NextIngredient");
        nextIngredientButton.clicked += nextIngredient;

        var prevIngredientButton = root.Q<Button>("PrevIngredient");
        prevIngredientButton.clicked += prevIngredient;

        //Hide the ingredient view
        var ingredientView = root.Q<VisualElement>("IngredientView"); 
        ingredientView.style.display = StyleKeyword.None;

        //For each unique ingredient in the ingredient inventory, create a recipe menu item and add it to the container
        foreach (KeyValuePair<Ingredient, int> ingredient in ingredients)
        {
            ingredientTemplate.CloneTree(ingredientContainer);

            //Set the name and image of the newly created menu item
            var ingredientName = ingredientContainer.Query<Label>().Last();
            ingredientName.text = ingredient.Key.ingredientName;

            var ingredientButton = ingredientContainer.Query<Button>().Last();
            ingredientButton.style.backgroundImage = ingredient.Key.image;
            ingredientButton.clicked += () => showIngredientView(ingredient.Key, ingredient.Value);

        }

    }

    private void nextIngredient()
    {
        ingredientIndex += 1;
        if (ingredientIndex >= ingredients.Count)
            ingredientIndex = 0;

        showIngredientView(ingredients[ingredientIndex].Key, ingredients[ingredientIndex].Value);
    }

    private void prevIngredient()
    {
        ingredientIndex -= 1;
        if (ingredientIndex < 0)
            ingredientIndex = ingredients.Count - 1;

        showIngredientView(ingredients[ingredientIndex].Key, ingredients[ingredientIndex].Value);
    }

    private void showIngredientView(Ingredient ingredient, int qty)
    {
        var root = GetComponent<UIDocument>().rootVisualElement; 
        var ingredientView = root.Q<VisualElement>("IngredientView");

        ingredientIndex = FindIngredientIndex(ingredient);

        if (ingredientView.style.display == StyleKeyword.None)
        {
            ingredientView.style.display = StyleKeyword.Initial;
        }

        var ingredientName = root.Q<Label>("IngredientViewName");
        ingredientName.text = ingredient.ingredientName;

        var ingredientImage = root.Q<VisualElement>("IngredientImage");
        ingredientImage.style.backgroundImage = ingredient.image;

        var ingredientDescripton = root.Q<Label>("Description");
        ingredientDescripton.text = ingredient.description;

        var ingredientQty = root.Q<Label>("IngredientQty");
        ingredientQty.text = qty.ToString();
    }

    private void hideIngredientView()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var ingredientView = root.Q<VisualElement>("IngredientView");
        ingredientView.style.display = StyleKeyword.None;
    }

    private void hideIngredientMenu()
    {
        GetComponent<UIDocument>().enabled = false;
        MainMenu.enableIngredientPlots();
    }

    private int FindIngredientIndex(Ingredient ingredient)
    {
        int index = 0;
        for (; index < ingredients.Count; index++)
        {
            if (ingredients[index].Key.ingredientName == ingredient.ingredientName)
                return index;
        }
        return -1;
    }
    
}
