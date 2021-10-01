using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum NPCTypes
{
    Normal,
    Adventurer
}

public class NPCManager : MonoBehaviour
{
    // Singleton
    private static NPCManager instance;
    public static NPCManager Instance { get => instance;}

    // Fields
    [SerializeField] private GameObject        npcPrefab;
    /**************/ private Vector2           spawnPos;
    /**************/ private float             spawnCoolDown; // Time between customers entering the tavern
    /**************/ private float             timer = 0.0f;
    /**************/ private int               guestCapacity;
    /**************/ private List<GameObject>  guests;

    private void Awake()
    {
        // Initializing singleton
        if (instance == null) 
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // May scale these fields to tavern level in the future
        guestCapacity = 4;
        spawnCoolDown = Random.Range(2.0f, 6.0f); 

        guests = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnCoolDown)
        {
            SpawnNPC();
            timer = 0.0f;
        }
    }

    void SpawnNPC()
    {
        // Look for available seats
        foreach(Vector2 seatSpot in TavernManager.Instance.SeatAvailability.Keys)
        {
            bool occupied = TavernManager.Instance.SeatAvailability[seatSpot];

            if(!occupied)
            {
                GameObject newNPC = Instantiate(npcPrefab);
                newNPC.transform.position = seatSpot;
                TavernManager.Instance.SeatAvailability[seatSpot] = true;
                return;
            }
        }
    }
}
