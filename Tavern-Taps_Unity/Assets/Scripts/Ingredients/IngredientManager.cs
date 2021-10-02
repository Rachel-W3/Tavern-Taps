using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    // Singleton
    private static IngredientManager ingredientManager;
    public static IngredientManager Ingredients { get => ingredientManager; }

    //Fields
    public enum IngredientTypes
    {
        DairyMeat,
        Veggies,
        Grain
    }

    [SerializeField] private int redAmt;
    [SerializeField] private int vegAmt;
    [SerializeField] private int dryAmt;
    [SerializeField] private int grnAmt;

    public List<Dish> Dishes;

    //Properties
    public int MeatAmt { get => redAmt; }
    public int VeggiesAmt { get => vegAmt; }
    public int DairyAmt { get => dryAmt; }
    public int GrainAmt { get => grnAmt; }

    private void Awake()
    {
        if (ingredientManager == null)
            ingredientManager = this; 
    }

    //Add Ingredients
    public void addIngredient(int amt, IngredientTypes ingredient)
    {
        switch (ingredient)
        {
            case IngredientTypes.DairyMeat:
                addDairyMeat(amt);
                break;
            
            case IngredientTypes.Veggies:
                addVeg(amt);
                break;

            case IngredientTypes.Grain:
                addGrn(amt);
                break;
        }   
    }

    private void addDairyMeat(int amt)
    {
        redAmt += amt; 
    }

    private void addVeg(int amt)
    {
        vegAmt += amt;
    }

    private void addDry(int amt)
    {
        dryAmt += amt;
    }

    private void addGrn(int amt)
    {
        grnAmt += amt;
    }

    //Check if there are ingredients
    public bool checkIngredient(int amt, IngredientTypes ingredient)
    {
        switch (ingredient)
        {
            case IngredientTypes.DairyMeat:
                return checkMeat(amt);

            case IngredientTypes.Veggies:
                return checkVeg(amt);

            case IngredientTypes.Grain:
                return checkGrn(amt);                
        }
        return false;
    }

    private bool checkMeat(int amt)
    {
        if (redAmt - amt >= 0)
        {
            return true;
        }
        return false;   
    }

    private bool checkVeg(int amt)
    {
        if (vegAmt - amt >= 0)
        {
            return true;
        }
        return false;
    }

    private bool checkDry(int amt)
    {
        if (dryAmt - amt >= 0)
        {
            return true;
        }
        return false;
    }

    private bool checkGrn(int amt)
    {
        if (grnAmt - amt >= 0)
        {
            return true;
        }
        return false;
    }

    //Remove Ingredients
    public bool removeIngredient(int amt, IngredientTypes ingredient)
    {
        switch (ingredient)
        {
            case IngredientTypes.DairyMeat:
                return subMeat(amt);

            case IngredientTypes.Veggies:
                return subVeg(amt);

            case IngredientTypes.Grain:
                return subGrn(amt);
        }
        return false;
    }

    private bool subMeat(int amt)
    {
        if (redAmt - amt >= 0)
        {
            redAmt -= amt;
            return true;
        }
        return false;
    }

    private bool subVeg(int amt)
    {
        if (vegAmt - amt >= 0)
        {
            vegAmt -= amt;
            return true;
        }
        return false;
    }

    private bool subGrn(int amt)
    {
        if (grnAmt - amt >= 0)
        {
            grnAmt -= amt;
            return true;
        }
        return false;
    }
}
