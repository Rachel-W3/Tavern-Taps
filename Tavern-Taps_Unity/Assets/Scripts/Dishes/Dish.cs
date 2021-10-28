using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Dish : ScriptableObject
{
    public string Name;
    public string Description;
    public Texture2D Image;
    public int goldOutput;
}
