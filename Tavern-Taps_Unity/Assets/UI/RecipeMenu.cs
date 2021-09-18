using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class RecipeMenu : MonoBehaviour
{
    private List<Dish> dishes = new List<Dish>();
    VisualElement recipeTest;

    public void OnEnable()
    {
        //Load the values for all of the dishes
        loadDishes();

        //Establish the UI elements you're going to be appending to
        var root = GetComponent<UIDocument>().rootVisualElement;
        var recipeContainer = root.Q<VisualElement>("recipesContainer");

        //Load the recipe template
        var recipeTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/RecipeMenuItem.uxml");

        //For each dish in dishes, create a recipe menu item and add it to the container
        foreach (Dish dish in dishes)
        {
            recipeTemplate.CloneTree(recipeContainer);

            //Set the name and image of the newly created menu item
            var recipeName = recipeContainer.Query<Label>().Last();   
            recipeName.text = dish.Name;

            var recipeImage = recipeContainer.Query<VisualElement>().Last();
            recipeImage.style.backgroundImage = dish.Image;

        }
    }

    private void loadDishes()
    {
        //Get the raw paths of the dishes
        string[] rawDishes = AssetDatabase.FindAssets("t:Dish", new[] { "Assets/Scripts/Dishes" });
        
        //Load the assets at all of those paths (This may be slow at scale)
        foreach (var rawDish in rawDishes)
        {
            dishes.Add(AssetDatabase.LoadAssetAtPath<Dish>(AssetDatabase.GUIDToAssetPath(rawDish)));
        }
    }
}
