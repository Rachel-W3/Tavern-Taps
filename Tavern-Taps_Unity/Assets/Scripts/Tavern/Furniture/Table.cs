using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    private bool empty;

    public bool Empty { get => empty; }  

    void Start()
    {
        empty = true;
    }

    public void setDish(Dish targetDish)
    {
        gameObject.GetComponent<Image>().enabled = true; 
        gameObject.GetComponent<Image>().sprite = targetDish.sprite;
        empty = false;
    }

    public void clearDish()
    {
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.GetComponent<Image>().sprite = null;
        empty = true;
    }
}
