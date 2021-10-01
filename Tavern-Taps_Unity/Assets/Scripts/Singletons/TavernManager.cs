using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author(s): Rachel Wong

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
    private int coins;
    private int tavernLevel;

    // Properties
    public int Coins { get => coins; }

    private void Awake()
    {
        // Initializing singleton
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
