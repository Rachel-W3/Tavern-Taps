using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    // Fields
    /**************/ private float timePickingFood = 1.0f; // NPCs take 1 second to order food
    /**************/ private float totalEatingTime = 5.0f;
    /**************/ private bool  eating;
    /**************/ private float timer = 0.0f;
    /**************/ private Dish  selectedDish;
    /**************/ private bool  satisfied;
    // TODO: Variable for food request bubble (UI)

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
            TavernManager.Instance.Gold += selectedDish.goldOutput;
            timer = 0.0f;
        }
    }

    void TakeFood()
    {
        int numDishes = TavernManager.Instance.getNumDishes();
        if(numDishes > 0)
        {
            //Simple Selection, needs to be changed
            List<Dish> possibleDishes = new List<Dish>(TavernManager.Instance.Dishes.Keys);
            if( possibleDishes.Count == 1 )
                selectedDish = possibleDishes[0];
            
            else
            {
                float rngCap = 0;
                float probabilityCursor= 0;

                //Get the total rarities of all ingredients
                foreach (Dish dish in possibleDishes)
                {
                    rngCap += 0.1f;
                }

                //Randomly generate a number based on the number of possible ingredients
                float probabilityTarget = UnityEngine.Random.Range(0f, rngCap);

                foreach (Dish dish in possibleDishes)
                {
                    probabilityCursor += 0.1f;
                    if (probabilityCursor >= probabilityTarget)
                    {
                        selectedDish = dish;
                        return;
                    }

                }
            }

            TavernManager.Instance.removeDish(selectedDish);
            eating = true;
        }
    }
}
