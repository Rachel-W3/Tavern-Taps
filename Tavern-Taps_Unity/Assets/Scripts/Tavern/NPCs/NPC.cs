using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    // Fields
    private float timePickingFood = 1.0f; // NPCs take 1 second to order food
    private float totalEatingTime = 5.0f;
    private bool  eating;
    private float timer = 0.0f;
    private Dish  selectedDish;
    private bool  satisfied;

    // Properties
    public bool Eating { get => eating; }
    public bool Satisfied { get => satisfied; }
    public Dish SelectedDish { get => selectedDish; }

    // Start is called before the first frame update
    void Start()
    {
        eating = false;
        satisfied = false;

        // Pick random dish from list of discovered dishes
        selectedDish = SelectDish();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(!eating && timer >= timePickingFood)
        {
            TakeFood();
            timer = 0.0f;
        }

        if(eating && timer >= totalEatingTime)
        {
            satisfied = true;
            TavernManager.Instance.Gold += selectedDish.GoldOutput; 
            timer = 0.0f;
        }
    }

    /// <summary>
    /// NPCs request a dish from the list of available recipes in the recipe menu
    /// and make their selection
    /// </summary>
    /// <returns> selected dish </returns>
    Dish SelectDish()
    {
        float selectionValue = Random.value;
        //Simple Selection, needs to be changed
        GameObject recipeMenu = GameObject.Find("RecipeMenu"); // Extremely slow, but no better choice yet. May consider turning menus to singletons for easy access
        List<Recipe> recipes;
        if(recipeMenu != null)
        {
            recipes = recipeMenu.GetComponent<RecipeMenu>().Recipes;
            if( recipes.Count == 1 )
                selectedDish = recipes[0].FinishedProduct;
            
            else
            {
                if (selectionValue > .5f)
                    selectedDish = recipes[0].FinishedProduct;
                else
                    selectedDish = recipes[1].FinishedProduct;
            }
        }
        return selectedDish;
    }

    void TakeFood()
    {
        int numDishes = TavernManager.Instance.getNumDishes();
        if(numDishes > 0)
        {
            TavernManager.Instance.removeDish(selectedDish);
            eating = true;
        }
    }
}
