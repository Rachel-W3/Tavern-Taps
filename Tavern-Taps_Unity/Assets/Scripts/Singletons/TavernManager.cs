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
    private int                       coins;
    private int                       tavernLevel;
    private List<Vector2>             seatPositions; // Might make it easier for adding new seats down the line
    private Dictionary<Vector2, bool> seatAvailability; // Key : Value = seatPosition : occupied

    // Properties
    public int Coins { get => coins; }
    public Dictionary<Vector2, bool> SeatAvailability { get => seatAvailability; }

    private void Awake()
    {
        // Initializing singleton
        if (instance == null) instance = this;
        seatPositions = new List<Vector2>();
        seatAvailability = new Dictionary<Vector2, bool>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = -2; i <= 2; i++)
        {
            seatPositions.Add(new Vector2(i, -4));
        }

        foreach(Vector2 pos in seatPositions)
        {
            seatAvailability.Add(pos, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
