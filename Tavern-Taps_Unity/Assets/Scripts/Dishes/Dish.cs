using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Dish : ScriptableObject
{
    public string Name;
    public string Description;
    public Texture2D Image;
    public Sprite sprite;
    public int starRating;
    public int quantity; // Adding this here so we can get rid of the dictionary
    private int baseGoldOutput = 5; // Gold output scales to rating of food

    public int GoldOutput { get => baseGoldOutput * starRating;}
}
