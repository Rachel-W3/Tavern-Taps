using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public string description;
    public Texture2D image;
    [Range(1, 100)] public int rarity;
}
