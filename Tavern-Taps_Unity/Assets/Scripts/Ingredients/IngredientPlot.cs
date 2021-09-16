using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPlot : MonoBehaviour
{   
    //Fields
    [SerializeField] IngredientManager.IngredientTypes type;
    [SerializeField] static float growthRate = 1.0f;
    [SerializeField] int growthAmount = 1;
    [SerializeField] int ingredientCapacity = 100;
    private int storedIngredient = 0;

    //Properties
    public IngredientManager.IngredientTypes Type { get => type; }
    public float GrowthRate { get => growthRate; set => growthRate = value; }
    public int GrowthAmount { get => growthAmount; set => growthAmount = value; }
    public int IngredientCapacity { get => ingredientCapacity; set => ingredientCapacity = value; }
    public int StoredIngredient { get => storedIngredient; set => storedIngredient = Mathf.Clamp(value, 0, ingredientCapacity); }

    WaitForSeconds waitForGrowth = new WaitForSeconds(growthRate);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(createIngredient());
    }

    public bool harvest()
    {
        if (storedIngredient > 0)
        {
            IngredientManager.Ingredients.addIngredient(storedIngredient, type);
            return true;
        }
        return false; 
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
            if (storedIngredient + growthAmount < ingredientCapacity)
            {
                storedIngredient += growthAmount;
            }
            else { storedIngredient = ingredientCapacity; }
        }
    }
}
