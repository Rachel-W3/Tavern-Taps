using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct IngredientAmounts
{
    public IngredientManager.IngredientTypes Type;
    [Range(1, 999)] public int Amount;
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
            if (!IngredientManager.Ingredients.checkIngredient(Ingredient.Amount, Ingredient.Type))
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
                IngredientManager.Ingredients.removeIngredient(Ingredient.Amount, Ingredient.Type);
            }

            IngredientManager.Ingredients.Dishes.Add(FinishedProduct);
            Debug.Log("Number Of Dishes " + IngredientManager.Ingredients.Dishes.Count);
        }
    }
}
