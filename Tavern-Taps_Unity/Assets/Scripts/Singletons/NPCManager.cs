using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    /**************/ private int               guestCount;

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
        guestCapacity = 5;
        guestCount = 0;
        spawnCoolDown = Random.Range(0.5f, 6.0f); 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnCoolDown && guestCount < guestCapacity)
        {
            //SpawnNPC();
            timer = 0.0f;
        }

        UpdateGuestList();
    }

    void SpawnNPC()
    {
        // Look for available seats
        foreach(Vector2 seatSpot in TavernManager.Instance.SeatingChart.Keys)
        {
            GameObject occupant = TavernManager.Instance.SeatingChart[seatSpot];

            if(occupant == null)
            {
                GameObject newNPC = Instantiate(npcPrefab);
                newNPC.transform.position = seatSpot;
                TavernManager.Instance.SeatingChart[seatSpot] = newNPC;
                guestCount++;
                return;
            }
        }
    }

    void UpdateGuestList()
    {
        // Check if guests are done eating
        foreach(Vector2 seatSpot in TavernManager.Instance.SeatingChart.Keys)
        {
            GameObject occupant = TavernManager.Instance.SeatingChart[seatSpot];

            if(occupant != null && occupant.GetComponent<NPC>().Satisfied)
            {
                TavernManager.Instance.SeatingChart[seatSpot] = null;
                guestCount--;
                Destroy(occupant);
                return;
            }
        }
    }
}
