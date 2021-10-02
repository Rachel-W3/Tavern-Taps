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

    private int Rice_Amount;
    private int Pasta_Amount;
    private int Boar_Meat_Amount;
    private int Milk_Amount;
    private int Butter_Amount;
    private int Mandrake_Amount;
    private int Lettuce_Amount;
    private int Corn_Amount;
    private int Eggs_Amount;

    [SerializeField] private int DairyMeat_Amount;
    [SerializeField] private int Veggies_Amount;
    [SerializeField] private int Grain_Amount;

    public List<Dish> Dishes;

    //Properties
    public int DairyMeatAmt { get => DairyMeat_Amount; }
    public int VeggiesAmt { get => Veggies_Amount; }
    public int GrainAmt { get => Grain_Amount; }

    private void Awake()
    {
        if (ingredientManager == null)
            ingredientManager = this; 
    }

    private void OnGUI()
    {
        string DishString = "";

        foreach(Dish dish in Dishes)
            DishString += "\n" + dish.Name;
        
        GUI.Box(new Rect(Screen.width * 2/3, Screen.width/4, Screen.width/3, Screen.height/2), "Dairy/Meat: " + DairyMeat_Amount + "\nVeggies: " + Veggies_Amount + "\nGrain: " + Grain_Amount + "\n\nDishes:" + DishString);
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
        DairyMeat_Amount += amt; 
    }

    private void addVeg(int amt)
    {
        Veggies_Amount += amt;
    }

    private void addGrn(int amt)
    {
        Grain_Amount += amt;
    }

    //Check if there are ingredients
    public bool checkIngredient(int amt, IngredientTypes ingredient)
    {
        switch (ingredient)
        {
            case IngredientTypes.DairyMeat:
                return checkDryMeat(amt);

            case IngredientTypes.Veggies:
                return checkVeg(amt);

            case IngredientTypes.Grain:
                return checkGrn(amt);                
        }
        return false;
    }

    private bool checkDryMeat(int amt)
    {
        if (DairyMeat_Amount - amt >= 0)
        {
            return true;
        }
        return false;   
    }

    private bool checkVeg(int amt)
    {
        if (Veggies_Amount - amt >= 0)
        {
            return true;
        }
        return false;
    }

    private bool checkGrn(int amt)
    {
        if (Grain_Amount - amt >= 0)
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
                return subDryMeat(amt);

            case IngredientTypes.Veggies:
                return subVeg(amt);

            case IngredientTypes.Grain:
                return subGrn(amt);
        }
        return false;
    }

    private bool subDryMeat(int amt)
    {
        if (DairyMeat_Amount - amt >= 0)
        {
            DairyMeat_Amount -= amt;
            return true;
        }
        return false;
    }

    private bool subVeg(int amt)
    {
        if (Veggies_Amount - amt >= 0)
        {
            Veggies_Amount -= amt;
            return true;
        }
        return false;
    }

    private bool subGrn(int amt)
    {
        if (Grain_Amount - amt >= 0)
        {
            Grain_Amount -= amt;
            return true;
        }
        return false;
    }
}
