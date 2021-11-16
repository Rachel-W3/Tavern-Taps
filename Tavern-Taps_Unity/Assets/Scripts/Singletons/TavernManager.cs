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

    // Fields
    private int                             gold;
    private int                             tavernLevel;
    // Seating
    private Chair[] chairs;

    // Dish display
    public List<Dish>                       Dishes;
    [SerializeField] private GameObject     bar;

    // Properties
    public int Gold { get => gold; set => setGold(value); }
    public Chair[] Chairs { get => chairs; }

    private void Awake()
    {
        // Initializing singleton
        if (instance == null) instance = this;
        chairs = FindObjectsOfType<Chair>();

        Dishes = new List<Dish>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //chairs = FindObjectsOfType<Chair>();

        // Initializing bar for dish display
        RectTransform barRT = (RectTransform)bar.transform;
        Debug.Log("Position: " + barRT.rect.width);
    }

    public void addDish(Dish dish)
    {
        foreach(Dish d in Dishes)
        {
            if(dish.Name == d.Name )
            {
                d.quantity++;
                break;
            }
        }
        bar.GetComponent<Bar>().refresh();
    }

    public void removeDish(Dish dish)
    {
        foreach(Dish d in Dishes)
        {
            if(dish.Name == d.Name )
            {
                d.quantity--;
                break;
            }
        }
    }
    
    public int getNumDishes()
    {
        int total = 0;

        foreach (Dish d in Dishes)
            total += d.quantity;

        return total; 
    }

    private void setGold(int value)
    {
        gold = value;
        MainMenu.updateMoneyUI(value);
    }
}
