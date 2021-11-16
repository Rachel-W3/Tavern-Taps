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
        guestCapacity = TavernManager.Instance.Chairs.Length;
        guestCount = 0;
        spawnCoolDown = Random.Range(0.5f, 6.0f); 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnCoolDown && guestCount < guestCapacity)
        {
            SpawnNPC();
            timer = 0.0f;
        }

        UpdateGuestList();
    }

    void SpawnNPC()
    {
        Chair[] chairs = TavernManager.Instance.Chairs;
        // Look for available seats
        foreach (Chair chair in chairs)
        {
            if(!chair.Occupied)
            {
                GameObject newNPC = Instantiate(npcPrefab);
                newNPC.transform.position = chair.transform.position; 
                chair.setNPC(newNPC);
                guestCount++;
                return;
            }
        }
    }

    void UpdateGuestList()
    {
        Chair[] chairs = TavernManager.Instance.Chairs;

        // Check if guests are done eating
        foreach (Chair chair in chairs)
        {
            GameObject occupant = chair.Occupant;
            if(chair.Occupied && occupant.GetComponent<NPC>().Satisfied)
            {
                chair.clearNPC();
                guestCount--;
                Destroy(occupant);
            }
        }
    }
}
