using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chair : MonoBehaviour
{

    private bool occupied;
    private GameObject occupant;
    [SerializeField] private Sprite npcSprite;
    [SerializeField] private Sprite bubbleSprite; // we shouldn't need this variable in the future
    [SerializeField] private Table table;

    public GameObject Occupant { get => occupant; }
    public bool Occupied { get => occupied; }

    void Start()
    {
        occupied = false;
        occupant = null;
    }

    void Update()
    {
        Dish selectedDish;
        if(occupied && table.Empty)
        {
            if(selectedDish = occupant.GetComponent<NPC>().SelectedDish )
            {
                table.setDish(selectedDish);
            }
        }
    }

    public void setNPC(GameObject npc)
    {
        // Access food icon sprite in game object hierarchy [NPC -> foodRequestIcon -> bubble & foodRequest]
        //GameObject requestIcon = npc.GetComponentInChildren<Transform>().gameObject;
        //Image bubble = requestIcon.GetComponentInChildren<Image>();
        //Debug.Log(bubble.sprite.name);
        // These two lines of code feel redundant. Need to look into
        // why our canvas only accepts images and not sprites
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.GetComponent<Image>().sprite = npcSprite;
        occupied = true;
        occupant = npc; 
    }

    public void clearNPC()
    {
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.GetComponent<Image>().sprite = null;
        table.clearDish();
        occupied = false;
        
        Destroy(occupant);
        occupant = null; 
    }
}
