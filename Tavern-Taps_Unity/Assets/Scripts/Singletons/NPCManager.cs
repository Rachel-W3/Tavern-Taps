using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    // Singleton
    private static NPCManager instance;
    public static NPCManager Instance { get => instance;}

    // Fields
    [SerializeField] private GameObject        npcPrefab;
    /**************/ private Vector2           spawnPos;
    /**************/ private float             spawnRate;
    /**************/ private float             rateVariance;
    /**************/ private int               guestCapacity;
    /**************/ private List<GameObject>  guests;

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
