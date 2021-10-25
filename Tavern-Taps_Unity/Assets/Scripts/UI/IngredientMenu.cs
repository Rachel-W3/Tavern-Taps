using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IngredientMenu : MonoBehaviour
{
    private List<Ingredient> ingredients = new List<Ingredient>();
    [SerializeField] private VisualTreeAsset ingredientTemplate;
    private int ingredientIndex = 0;

    public void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        //Hide the ingredient menu label
        var ingredientMenuLabel = root.Q<Label>("IngredientMenuLabel");
        ingredientMenuLabel.style.display = StyleKeyword.None;

        //Hide the ingredient view
        var ingredientView = root.Q<VisualElement>("IngredientView");
        ingredientView.style.display = StyleKeyword.None;
    }

    public void refreshUI()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var ingredientContainer = root.Q<VisualElement>("ingredientsContainer");

        //Set onclick handlers
        var ingredientExitButton = root.Q<Button>("ExitButton");
        ingredientExitButton.clicked += hideIngredientView;

        var nextIngredientButton = root.Q<Button>("NextIngredient");
        nextIngredientButton.clicked += nextIngredient;

        var prevIngredientButton = root.Q<Button>("PrevIngredient");
        prevIngredientButton.clicked += prevIngredient;

        //Hide the ingredient view
        var ingredientView = root.Q<VisualElement>("IngredientView");
        ingredientView.style.display = StyleKeyword.None;

        //For each unique ingredient in the ingredient inventory, create a recipe menu item and add it to the container
        foreach (Ingredient ingredient in ingredients)
        {
            ingredientTemplate.CloneTree(ingredientContainer);

            //Set the name and image of the newly created menu item
            var ingredientName = ingredientContainer.Query<Label>().Last();
            ingredientName.text = ingredient.ingredientName;

            var ingredientButton = ingredientContainer.Query<Button>().Last();
            ingredientButton.style.backgroundImage = ingredient.image;
            ingredientButton.clicked += () => showIngredientView(ingredient);

        }

    }

    private void nextIngredient()
    {
        ingredientIndex += 1;
        if (ingredientIndex >= ingredients.Count)
            ingredientIndex = 0;

        showIngredientView(ingredients[ingredientIndex]);

    }

    private void prevIngredient()
    {
        ingredientIndex -= 1;
        if (ingredientIndex < 0)
            ingredientIndex = ingredients.Count - 1;

        showIngredientView(ingredients[ingredientIndex]);
    }

    private void showIngredientView(Ingredient ingredient)
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var ingredientView = root.Q<VisualElement>("IngredientView");

        ingredientIndex = FindIngredientIndex(ingredient);

        if (ingredientView.style.display == StyleKeyword.None)
        {
            ingredientView.style.display = StyleKeyword.Initial;
        }

        var ingredientName = root.Q<Label>("IngredientName");
        ingredientName.text = ingredient.ingredientName;

        var ingredientImage = root.Q<VisualElement>("IngredientImage");

        ingredientImage.style.backgroundImage = ingredient.image;

        var ingredientDescripton = root.Q<Label>("Description");
        ingredientDescripton.text = ingredient.description;
    }

    private void hideIngredientView()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var ingredientView = root.Q<VisualElement>("ingredientView");
        ingredientView.style.display = StyleKeyword.None;
    }

    private int FindIngredientIndex(Ingredient ingredient)
    {
        int index = 0;
        for (; index < ingredients.Count; index++)
        {
            if (ingredients[index].ingredientName == ingredient.ingredientName)
                return index;
        }
        return -1;
    }
}
