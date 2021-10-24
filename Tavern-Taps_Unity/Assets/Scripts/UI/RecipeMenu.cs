using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class RecipeMenu : MonoBehaviour
{
    private List<Recipe> recipes = new List<Recipe>();
    private int DishIndex = 0;
    [SerializeField] private VisualTreeAsset recipeTemplate;

    public void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var recipeContainer = root.Q<VisualElement>("recipesContainer");

        //Set onclick handlers
        var DishExitButton = root.Q<Button>("ExitButton");
        DishExitButton.clicked += hideDishMenu;

        var NextRecipeButton = root.Q<Button>("NextRecipe");
        NextRecipeButton.clicked += nextRecipe;

        var PrevRecipeButton = root.Q<Button>("PrevRecipe");
        PrevRecipeButton.clicked += prevRecipe;

        var DishCookButton = root.Q<Button>("CookButton");
        DishCookButton.clicked += () => recipes[DishIndex].Cook();

    }

    public void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var recipeContainer = root.Q<VisualElement>("recipesContainer");

        //Load the values for all of the dishes
        loadRecipes();

        //Hide the dish menu
        var DishMenu = root.Q<VisualElement>("DishMenu");
        DishMenu.style.display = StyleKeyword.None;

        //For each dish in dishes, create a recipe menu item and add it to the container
        foreach (Recipe recipe in recipes)
        {
            recipeTemplate.CloneTree(recipeContainer);

            //Set the name and image of the newly created menu item
            var recipeName = recipeContainer.Query<Label>().Last();
            recipeName.text = recipe.FinishedProduct.Name;

            var recipeButton = recipeContainer.Query<Button>().Last();
            recipeButton.style.backgroundImage = recipe.FinishedProduct.Image;
            recipeButton.clicked += () => showDishMenu(recipe);

        }

    }

    public void refreshUI()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var recipeContainer = root.Q<VisualElement>("recipesContainer");

        //Set onclick handlers
        var DishExitButton = root.Q<Button>("ExitButton");
        DishExitButton.clicked += hideDishMenu;

        var NextRecipeButton = root.Q<Button>("NextRecipe");
        NextRecipeButton.clicked += nextRecipe;

        var PrevRecipeButton = root.Q<Button>("PrevRecipe");
        PrevRecipeButton.clicked += prevRecipe;

        var DishCookButton = root.Q<Button>("CookButton");
        DishCookButton.clicked += () => recipes[DishIndex].Cook();

        //Hide the dish menu
        var DishMenu = root.Q<VisualElement>("DishMenu");
        DishMenu.style.display = StyleKeyword.None;

        //For each dish in dishes, create a recipe menu item and add it to the container
        foreach (Recipe recipe in recipes)
        {
            recipeTemplate.CloneTree(recipeContainer);

            //Set the name and image of the newly created menu item
            var recipeName = recipeContainer.Query<Label>().Last();
            recipeName.text = recipe.FinishedProduct.Name;

            var recipeButton = recipeContainer.Query<Button>().Last();
            recipeButton.style.backgroundImage = recipe.FinishedProduct.Image;
            recipeButton.clicked += () => showDishMenu(recipe);

        }

    }

    private void loadRecipes()
    {
        Recipe[] rawRecipes = Resources.LoadAll<Recipe>("");
        foreach(Recipe recipe in rawRecipes)
            recipes.Add(recipe);
    }

    private void nextRecipe()
    {
        DishIndex += 1;
        if (DishIndex >= recipes.Count)
            DishIndex = 0;

        showDishMenu(recipes[DishIndex]);

    }

    private void prevRecipe()
    {
        DishIndex -= 1;
        if (DishIndex < 0)
            DishIndex = recipes.Count - 1;

        showDishMenu(recipes[DishIndex]);
    }

    private void showDishMenu(Recipe recipe)
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var DishMenu = root.Q<VisualElement>("DishMenu");

        DishIndex = FindRecipeIndex(recipe);

        if (DishMenu.style.display == StyleKeyword.None)
        {
            DishMenu.style.display = StyleKeyword.Initial;
        }

        var DishName = root.Q<Label>("DishName");
        DishName.text = recipe.FinishedProduct.Name;

        var DishImage = root.Q<VisualElement>("DishImage");

        DishImage.style.backgroundImage = recipe.FinishedProduct.Image;

        var DishDescripton = root.Q<Label>("Description");
        DishDescripton.text = recipe.FinishedProduct.Description;
    }

    private void hideDishMenu()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var DishMenu = root.Q<VisualElement>("DishMenu");
        DishMenu.style.display = StyleKeyword.None;
    }

    private int FindRecipeIndex(Recipe recipe)
    {
        int index = 0;
        for (; index < recipes.Count; index++)
        {
            if (recipes[index].FinishedProduct.Name == recipe.FinishedProduct.Name)
                return index;
        }
        return -1;
    }
}
