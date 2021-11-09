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

    [SerializeField] public List<KeyValuePair<Ingredient, int>> ingredientInventory = new List<KeyValuePair<Ingredient, int>>();

    private void Awake()
    {
        if (ingredientManager == null)
            ingredientManager = this; 
    }


    //Add Ingredients
    public void addIngredient(Ingredient ingredient, int amt)
    {
        int newValue; 
        for(int i = 0; i < ingredientInventory.Count; i++)
        {
            KeyValuePair<Ingredient, int> kvp = ingredientInventory[i];
            if(kvp.Key.ingredientName == ingredient.ingredientName) 
            {
                newValue = kvp.Value + amt;
                ingredientInventory.Remove(kvp);
                ingredientInventory.Add(new KeyValuePair<Ingredient, int>(ingredient, newValue));
                return;
            }
        }
        ingredientInventory.Add(new KeyValuePair<Ingredient, int>(ingredient, amt));
    }


    //Check if there are ingredients
    public bool checkIngredient(int amt, Ingredient ingredient)
    {

        foreach(KeyValuePair<Ingredient, int> x in ingredientInventory)
        {
            if (x.Key.ingredientName == ingredient.ingredientName)
                return x.Value >= amt;
        }

        return false; 
        
    }


    //Remove Ingredients
    public void removeIngredient(Ingredient ingredient, int amt)
    {
        int newValue;
        for(int i = 0; i < ingredientInventory.Count; i++)
        {
            KeyValuePair<Ingredient, int> kvp = ingredientInventory[i];
            if (kvp.Key.ingredientName == ingredient.ingredientName)
            {
                newValue = kvp.Value - amt;
                ingredientInventory.Remove(kvp);
                ingredientInventory.Add(new KeyValuePair<Ingredient, int>(ingredient, newValue));
            }
                        
        }
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
                        addIngredient(animal, 1);
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
                        addIngredient(crop, 1);
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
                        addIngredient(process, 1);
                        return;
                    }
                }
                break;

            default:
                break;
        }
    }

}
