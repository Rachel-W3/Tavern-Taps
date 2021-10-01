using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    // Fields
    [SerializeField] private int   npcLevel;
    /**************/ private float timePickingFood = 1.0f; // NPCs take 1 second to order food
    /**************/ private float totalEatingTime = 5.0f;
    /**************/ private bool  eating;
    /**************/ private float timer = 0.0f;
    /**************/ private bool  satisfied;
    // TODO: Variable for food request bubble (UI)

    // Properties
    public bool Satisfied { get => satisfied; }

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
            timer = 0.0f;
        }
    }

    void TakeFood()
    {
        int numDishes = IngredientManager.Ingredients.Dishes.Count;

        if(numDishes > 0)
        {
            IngredientManager.Ingredients.Dishes.RemoveAt(0);
            eating = true;
        }
    }
}
