using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpecialNPC : NPC
{

    //Fields
    string name;
    string[] dialogue; //0 = greeting, 1 = description, 2 = exit
    int level = 1;
    int experience = 0;
    string bio;
    Ingredient[] possibleIngredients;



    // Start is called before the first frame update
    void Start()
    {
        name = "My Name";
        dialogue = new string[] { "greeting", "description", "exit" };
        bio = "this is my background";
        possibleIngredients = new Ingredient[] {new Ingredient(), new Ingredient()}; 
    }

    void Start(string name, string[] dialogue, string bio, Ingredient[] possibleIngredients)
    {
        this.name = name;
        this.dialogue = dialogue;
        this.bio = bio;
        this.possibleIngredients = possibleIngredients;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
