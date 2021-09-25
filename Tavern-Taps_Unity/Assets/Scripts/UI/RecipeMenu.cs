using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class RecipeMenu : MonoBehaviour
{
    private List<Recipe> recipes = new List<Recipe>();
    private int DishIndex = 0;

    public void OnEnable()
    {
        //Load the values for all of the dishes
        loadRecipes();

        //Establish the UI elements you're going to be appending to
        var root = GetComponent<UIDocument>().rootVisualElement;
        var recipeContainer = root.Q<VisualElement>("recipesContainer");
        
        //Hide the dish menu[p
        var DishMenu = root.Q<VisualElement>("DishMenu");
        DishMenu.style.display = StyleKeyword.None;

        //Load the recipe template
        var recipeTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/RecipeMenuItem.uxml");

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

    private void loadRecipes()
    {
        //Get the raw paths of the dishes
        string[] rawRecipes = AssetDatabase.FindAssets("t:Recipe", new[] { "Assets/Scripts/Recipes" });
        
        //Load the assets at all of those paths (This may be slow at scale)
        foreach (var rawRecipe in rawRecipes)
        {
            Debug.Log(rawRecipe);
            recipes.Add(AssetDatabase.LoadAssetAtPath<Recipe>(AssetDatabase.GUIDToAssetPath(rawRecipe)));
        }
    }

    private void nextRecipe()
    {
        DishIndex += 1;
        if (DishIndex >= recipes.Count)
            DishIndex = 0;

        showDishMenu(recipes[DishIndex]);

        Debug.Log("Next");
    }

    private void prevRecipe()
    {
        DishIndex -= 1;
        if (DishIndex < 0)
            DishIndex = recipes.Count - 1;

        showDishMenu(recipes[DishIndex]);

        Debug.Log("Prev");
    }

    private void showDishMenu(Recipe recipe)
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var DishMenu = root.Q<VisualElement>("DishMenu");

        DishIndex = FindRecipeIndex(recipe);
        Debug.Log(DishIndex);

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
        for (;index < recipes.Count; index++)
        {
            if (recipes[index].FinishedProduct.Name == recipe.FinishedProduct.Name)
                return index;
        }
        return -1;
    }

}
