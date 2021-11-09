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
            if (!IngredientManager.Instance.checkIngredient(Ingredient.Amount, Ingredient.Type))
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
                IngredientManager.Instance.removeIngredient(Ingredient.Amount, Ingredient.Type);
            }
            if(TavernManager.Instance.Dishes.ContainsKey(FinishedProduct))
            {
                TavernManager.Instance.Dishes[FinishedProduct]++;
            }
            else
            {
                TavernManager.Instance.Dishes.Add(FinishedProduct, 1);
            }
        }
    }
}
