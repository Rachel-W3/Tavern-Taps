using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    // Fields
    /**************/ private float       timePickingFood = 1.0f; // NPCs take 1 second to order food
    /**************/ private float       totalEatingTime = 5.0f;
    /**************/ private bool        eating;
    /**************/ private float       timer = 0.0f;
    /**************/ private Dish        selectedDish;
    /**************/ private bool        satisfied;
    /**************/ private GameObject  icon;
    [SerializeField] private GameObject  iconPrefab;
    /**************/ private ProgressBar progressBar;


    // Properties
    public bool Satisfied { get => satisfied; }
    public bool Eating { get => eating; }
    public Dish SelectedDish { get => selectedDish; }

    // Start is called before the first frame update
    void Start()
    {
        progressBar = GetComponentsInChildren<ProgressBar>()[0];
        progressBar.setupProgressBar(totalEatingTime);
        progressBar.updateProgressBar(0.0f);

        eating = false;
        satisfied = false;
        iconPrefab.GetComponent<RectTransform>().position = new Vector3(0, 3.57f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!eating && timer >= timePickingFood)
        {
            if(!selectedDish)
                OrderFood();

            EatFood();
            timer = 0.0f;
        }

        if(eating) 
        {
            progressBar.updateProgressBar(timer);

            if (timer >= totalEatingTime)
            {
                satisfied = true;
                TavernManager.Instance.Gold += selectedDish.GoldOutput;
                timer = 0.0f;
            }
        }
    }

    /// <summary>
    /// NPC picks their selection based on the list of available recipes,
    /// and if it's already cooked and ready to eat, NPC just takes it off
    /// the bar
    /// </summary>
    void OrderFood()
    {
        // Alias
        List<Recipe> orderOptions = TavernManager.Instance.KnownRecipes;
        if(orderOptions.Count > 0)
        {
            //Simple Selection, needs to be changed
            if( orderOptions.Count == 1 )
                selectedDish = orderOptions[0].FinishedProduct;
            
            else
            {
                float rngCap = 0;
                float probabilityCursor= 0;

                //Get the total rarities of all ingredients
                foreach (Recipe recipe in orderOptions)
                {
                    rngCap += 0.1f;
                }

                //Randomly generate a number based on the number of possible ingredients
                float probabilityTarget = Random.Range(0f, rngCap);

                foreach (Recipe recipe in orderOptions)
                {
                    probabilityCursor += 0.1f;
                    if (probabilityCursor >= probabilityTarget)
                    {
                        selectedDish = recipe.FinishedProduct;
                        break;
                    }

                }
            }

            // Setting the requested food icon to the right sprite
            icon = Instantiate(iconPrefab, gameObject.transform);
            GameObject requestedFood = icon.transform.GetChild(0).gameObject;
            requestedFood.GetComponent<SpriteRenderer>().sprite = selectedDish.sprite;
        }
    }

    /// <summary>
    /// NPC takes cooked dishes from dictionary in tavern manager and enters eating state
    /// </summary>
    bool EatFood()
    {

        if (TavernManager.Instance.removeDish(selectedDish))
        { 
            icon.SetActive(false);
            eating = true;
            return true;
        }

        return false;
    }
}
