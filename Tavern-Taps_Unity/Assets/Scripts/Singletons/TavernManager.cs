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
    public Dictionary<Dish, int>            Dishes;
    [SerializeField] private GameObject     bar;

    // Properties
    public int Gold { get => gold; set => gold = value; }
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
}
