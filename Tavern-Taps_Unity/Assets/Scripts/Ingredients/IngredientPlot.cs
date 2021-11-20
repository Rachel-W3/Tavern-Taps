using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPlot : MonoBehaviour
{
    //Fields
    public Button plotButton;
    [SerializeField] string plotName;
    [SerializeField] IngredientManager.IngredientType type;
    [SerializeField] static float growthRate = .1f;
    [SerializeField] float growthAmount = 1.0f;
    [SerializeField] int ingredientCapacity = 100;
    [SerializeField] private float storedIngredient = 0.0f;

    //Properties
    public IngredientManager.IngredientType Type { get => type; }
    public float GrowthRate { get => growthRate; set => growthRate = value; }
    public float GrowthAmount { get => growthAmount; set => growthAmount = value; }
    public int IngredientCapacity { get => ingredientCapacity; set => ingredientCapacity = value; }
    public float StoredIngredient { get => storedIngredient; set => storedIngredient = Mathf.Clamp(value, 0, ingredientCapacity); }

    WaitForSeconds waitForGrowth = new WaitForSeconds(growthRate);

    // Start is called before the first frame update
    void Start()
    {
        plotButton.onClick.AddListener(harvestOnClick);
        StartCoroutine(createIngredient());
    }

    public void harvest()
    {
        for (int i = 0; i < Math.Floor(storedIngredient); i++)
        {
            IngredientManager.Ingredients.addRandomIngredient(type);
        }
        storedIngredient = 0;

    }

    public void harvestOnClick()
    {
        harvest();
    }

    IEnumerator createIngredient()
    {
        while(true)
        {
            yield return waitForGrowth;
            if (storedIngredient + growthAmount <= ingredientCapacity)
            {
                storedIngredient += growthAmount;
                plotButton.GetComponentInChildren<Text>().text = plotName + " (" + Math.Floor(storedIngredient).ToString() + ")";
            }
            else { storedIngredient = ingredientCapacity; }
        }
    }
}
