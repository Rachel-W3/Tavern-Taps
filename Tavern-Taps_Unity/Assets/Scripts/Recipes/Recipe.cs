using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct IngredientAmounts
{
    public Ingredient ingredient;
    [Range(1, 999)] public int amount;
}

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public List<IngredientAmounts> RequiredIngredients;
    public Dish FinishedProduct;

    public bool CanCook()
    {
        foreach (IngredientAmounts Ingredient in RequiredIngredients)
        {
            if (!IngredientManager.Ingredients.checkIngredient(Ingredient.amount, Ingredient.ingredient))
                return false;
        }
        return true;
    }

    public void Cook()
    {
        if(CanCook())
        {
            foreach (IngredientAmounts Ingredient in RequiredIngredients)
            {
                IngredientManager.Ingredients.removeIngredient(Ingredient.amount, Ingredient.ingredient);
            }

            IngredientManager.Ingredients.Dishes.Add(FinishedProduct);
            Debug.Log("Number Of Dishes " + IngredientManager.Ingredients.Dishes.Count);
        }
    }
}
