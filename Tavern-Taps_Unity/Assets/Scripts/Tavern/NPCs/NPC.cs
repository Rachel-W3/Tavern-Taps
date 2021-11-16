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
    public bool Satisfied { get => satisfied; }
    public Dish SelectedDish { get => selectedDish; }

    // Start is called before the first frame update
    void Start()
    {
        eating = false;
        satisfied = false;
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

    Dish GetPickedDish()
    {
        float selectionValue = Random.value;
        //Simple Selection, needs to be changed
        List<Dish> possibleDishes = new List<Dish>(TavernManager.Instance.Dishes.Keys);
        if( possibleDishes.Count == 1 )
            selectedDish = possibleDishes[0];
            
        else
        {
            if (selectionValue > .5f)
                selectedDish = possibleDishes[0];
            else
                selectedDish = possibleDishes[1];
        }

        return selectedDish;
    }

    void TakeFood()
    {
        int numDishes = TavernManager.Instance.getNumDishes();
        if(numDishes > 0)
        {
            TavernManager.Instance.removeDish(GetPickedDish());
            eating = true;
        }
    }
}
