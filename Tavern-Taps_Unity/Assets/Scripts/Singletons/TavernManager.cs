using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Upgrades
{
    Seats,
}

/// <summary>
/// Class deals with tavern stats that can be upgraded
/// </summary>
public class TavernManager : MonoBehaviour
{
    // Singleton
    private static TavernManager instance;
    public static TavernManager Instance { get => instance;}
    [SerializeField] private List<Recipe> knownRecipes;

    // Fields
    private int                             gold;
    private int                             tavernLevel;

    // Seating
    private Chair[] chairs;

    // Dish display
    public Dictionary<Dish, int>            Dishes;
    [SerializeField] private GameObject     bar;

    // Properties
    public int Gold { get => gold; set => setGold(value); }
    public List<Recipe> KnownRecipes { get => knownRecipes; }
    public Chair[] Chairs { get => chairs; }

    private void Awake()
    {
        // Initializing singleton
        if (instance == null) instance = this;
        chairs = FindObjectsOfType<Chair>();
        Dishes = new Dictionary<Dish, int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //chairs = FindObjectsOfType<Chair>();

        // Initializing bar for dish display
        RectTransform barRT = (RectTransform)bar.transform;
        Debug.Log("Position: " + barRT.rect.width);
    }

    public void addNewRecipe(Recipe newRecipe)
    {
        knownRecipes.Add(newRecipe);
    }

    public void addDish(Dish dish)
    {
        if (Dishes.ContainsKey(dish))
        {
            Dishes[dish] += 1;
            bar.GetComponent<Bar>().refresh();
            return; 
        }
        Dishes.Add(dish, 1);
        bar.GetComponent<Bar>().refresh();
    }

    public bool removeDish(Dish dish)
    {
        if (Dishes.ContainsKey(dish))
        {
            Dishes[dish] -= 1;

            bar.GetComponent<Bar>().refresh();

            if (Dishes[dish] <= 0)
                Dishes.Remove(dish);

            return true;
        }

        return false;
    }
    
    public int getNumDishes()
    {
        int total = 0;

        foreach (KeyValuePair<Dish, int> dish in Dishes)
            total += dish.Value;

        return total; 
    }

    private void setGold(int value)
    {
        gold = value;
        MainMenu.updateMoneyUI(value);
    }
}
