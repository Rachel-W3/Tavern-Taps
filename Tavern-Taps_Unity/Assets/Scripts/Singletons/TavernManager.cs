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
    private List<Vector2>                   seatPositions; // Might make it easier for adding new seats down the line
    private Dictionary<Vector2, GameObject> seatingChart; // Key : Value = seatPosition : NPC occupant
    // Dish display
    public Dictionary<Dish, int>            Dishes;
    [SerializeField] private GameObject     bar;

    // Properties
    public int Gold { get => gold; set => gold = value; }
    public Dictionary<Vector2, GameObject> SeatingChart { get => seatingChart; }

    private void Awake()
    {
        // Initializing singleton
        if (instance == null) instance = this;
        // Seating
        seatPositions = new List<Vector2>();
        seatingChart = new Dictionary<Vector2, GameObject>();
        // Dishes
        Dishes = new Dictionary<Dish, int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initializing seating positions
        for(int i = -2; i <= 2; i++)
        {
            seatPositions.Add(new Vector2(i, -4));
        }

        foreach(Vector2 pos in seatPositions)
        {
            seatingChart.Add(pos, null);
        }

        // Initializing bar for dish display
        RectTransform barRT = (RectTransform)bar.transform;
        Debug.Log("Position: " + barRT.rect.width);
    }
}
