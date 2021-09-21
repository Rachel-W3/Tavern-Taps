using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class RecipeMenu : MonoBehaviour
{
    private List<Recipe> recipes = new List<Recipe>();
    VisualElement recipeTest;

    public void OnEnable()
    {
        //Load the values for all of the dishes
        loadRecipes();

        //Establish the UI elements you're going to be appending to
        var root = GetComponent<UIDocument>().rootVisualElement;
        var recipeContainer = root.Q<VisualElement>("recipesContainer");

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
            recipeButton.clicked += () => { loadRecipePage(recipe); };
            recipeButton.style.backgroundImage = recipe.FinishedProduct.Image;

            

        }
    }

    private void loadRecipes()
    {
        //Get the raw paths of the dishes
        string[] rawRecipes = AssetDatabase.FindAssets("t:Recipe", new[] { "Assets/Scripts/Recipes" });
        
        //Load the assets at all of those paths (This may be slow at scale)
        foreach (var rawRecipe in rawRecipes)
        {
            recipes.Add(AssetDatabase.LoadAssetAtPath<Recipe>(AssetDatabase.GUIDToAssetPath(rawRecipe)));
        }
    }

    private void loadRecipePage(Recipe recipe)
    {
        Debug.Log(recipe.FinishedProduct.Name);
    }
}
