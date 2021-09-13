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
        Red,
        Yellow,
        Blue,
        Green
    }

    [SerializeField] private int redAmt;
    [SerializeField] private int ylwAmt;
    [SerializeField] private int bluAmt;
    [SerializeField] private int grnAmt;


    //Properties
    public int RedAmt { get => redAmt; }
    public int YellowAmt { get => ylwAmt; }
    public int BlueAmt { get => bluAmt; }
    public int GreenAmt { get => grnAmt; }

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
            case IngredientTypes.Red:
                addRed(amt);
                break;
            
            case IngredientTypes.Yellow:
                addYlw(amt);
                break;

            case IngredientTypes.Blue:
                addBlu(amt);
                break;

            case IngredientTypes.Green:
                addGrn(amt);
                break;
        }   
    }

    private void addRed(int amt)
    {
        redAmt += amt; 
    }

    private void addYlw(int amt)
    {
        ylwAmt += amt;
    }

    private void addBlu(int amt)
    {
        bluAmt += amt;
    }

    private void addGrn(int amt)
    {
        grnAmt += amt;
    }

    //Remove Ingredients
    public bool removeIngredient(int amt, IngredientTypes ingredient)
    {
        switch (ingredient)
        {
            case IngredientTypes.Red:
                return subRed(amt);

            case IngredientTypes.Yellow:
                return subYlw(amt);

            case IngredientTypes.Blue:
                return subBlu(amt);

            case IngredientTypes.Green:
                return subGrn(amt);                
        }
        return false;
    }

    private bool subRed(int amt)
    {
        if (redAmt - amt >= 0)
        {
            redAmt -= amt;
            return true;
        }
        return false;   
    }

    private bool subYlw(int amt)
    {
        if (ylwAmt - amt >= 0)
        {
            ylwAmt -= amt;
            return true;
        }
        return false;
    }

    private bool subBlu(int amt)
    {
        if (bluAmt - amt >= 0)
        {
            bluAmt -= amt;
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
