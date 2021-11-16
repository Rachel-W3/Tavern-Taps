using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chair : MonoBehaviour
{

    private bool occupied;
    private GameObject occupant;
    [SerializeField] private Sprite npcSprite;
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
        //Dish selectedDish = occupant.GetComponent<NPC>().SelectedDish;
        //if(occupied && table.Empty && selectedDish)
        //{
        //    if(selectedDish == occupant.GetComponent<NPC>().SelectedDish && occupant.GetComponent<NPC>().Eating )
        //    {
        //        table.setDish(selectedDish);
        //    }
        //}
    }

    public void setNPC(GameObject npc)
    {
        // These two lines of code feel redundant. Need to look into
        // why our canvas only accepts images and not sprites
        //gameObject.GetComponent<Image>().enabled = true;
        //gameObject.GetComponent<Image>().sprite = npcSprite;
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
