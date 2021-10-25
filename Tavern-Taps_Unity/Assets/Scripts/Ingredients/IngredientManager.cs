using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    // Singleton
    private static IngredientManager ingredientManager;
    public static IngredientManager Ingredients { get => ingredientManager; }

    public enum IngredientType
    {
        Crop, Animal, Process 
    }

    [SerializeField] private Ingredient[] cropList;
    [SerializeField] private Ingredient[] animalList;
    [SerializeField] private Ingredient[] processList;

    public List<Ingredient> ingredientInventory = new List<Ingredient>();
    public List<Dish> Dishes;


    private void Awake()
    {
        if (ingredientManager == null)
            ingredientManager = this; 
    }


    //Add Ingredients
    public void addIngredient(int amt, Ingredient ingredient)
    {
        for (int i = 0; i < amt; i++)
            ingredientInventory.Add(ingredient);
    }


    //Check if there are ingredients
    public bool checkIngredient(int amt, Ingredient ingredient)
    {
        int ingredientCnt = 0;

        foreach(Ingredient x in ingredientInventory)
        {
            if (x.ingredientName == ingredient.ingredientName)
                ingredientCnt++;
        }

        return ingredientCnt >= amt;
    }


    //Remove Ingredients
    public void removeIngredient(int amt, Ingredient ingredient)
    {
        for (int i = 0; i < amt; i++)
            ingredientInventory.Remove(ingredient);
    }

    //Add a random ingredient from an ingredient list;
    public void addRandomIngredient(IngredientType type)
    {
        float rngCap = 0;
        float probabilityTarget;

        //Add numbers to this variable based on the ingredient type until it
        //is greater than the randomly generated number
        int probabilityCursor = 0; 

        switch(type)
        {
            case IngredientType.Animal:
                //Get the total rarities of all ingredients
                foreach (Ingredient animal in animalList)
                {
                    rngCap += animal.rarity;
                }

                Debug.Log(rngCap);
                //Randomly generate a number based on the rarities of all possible ingredients 
                probabilityTarget = UnityEngine.Random.Range(0f, rngCap);
               
                foreach (Ingredient animal in animalList)
                {
                    probabilityCursor += animal.rarity;
                    if (probabilityCursor >= probabilityTarget)
                    {
                        Debug.Log("Adding... " + animal.ingredientName);
                        ingredientInventory.Add(animal);
                        return;
                    }
                    
                }
                break;

            case IngredientType.Crop:
                foreach (Ingredient crop in cropList)
                {
                    rngCap += crop.rarity;
                }

                probabilityTarget = UnityEngine.Random.Range(0f, rngCap);

                foreach (Ingredient crop in cropList)
                {
                    probabilityCursor += crop.rarity;
                    if (probabilityCursor >= probabilityTarget)
                    { 
                        ingredientInventory.Add(crop);
                        return;
                    }
                }
                break;

            case IngredientType.Process:
                foreach (Ingredient process in processList)
                {
                    rngCap += process.rarity;
                }

                probabilityTarget = UnityEngine.Random.Range(0f, rngCap);

                foreach (Ingredient process in processList)
                {
                    probabilityCursor += process.rarity;
                    if (probabilityCursor >= probabilityTarget)
                    {
                        ingredientInventory.Add(process);
                        return;
                    }
                }
                break;

            default:
                break;
        }
    }

}
