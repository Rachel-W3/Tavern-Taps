using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public GameObject bacon_n_eggs;
    public GameObject mandrake_stirfry;

    void Awake()
    {
        HideObject(bacon_n_eggs);
        HideObject(mandrake_stirfry);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void refresh()
    {
        foreach(Dish dish in TavernManager.Instance.Dishes)
        {
            //This is rushed and bad, It will need to be changed
            if (dish.quantity > 0)
            {
                if (dish.Name == "Dire bacon and eggs")
                {
                    ShowObject(bacon_n_eggs);
                    bacon_n_eggs.GetComponentInChildren<Text>().text = dish.quantity.ToString();
                }

                else if (dish.Name == "Mandrake Stirfry")
                {
                    ShowObject(mandrake_stirfry);
                    Debug.Log(dish.quantity);
                    mandrake_stirfry.GetComponentInChildren<Text>().text = dish.quantity.ToString();
                }
            }
        }
        return;
    }

    private void HideObject(GameObject gameObject)
    {
        Canvas canvas;

        if (canvas = gameObject.GetComponent<Canvas>())
        {
            canvas.enabled = false;
        }

    }

    private void ShowObject(GameObject gameObject)
    {
        Canvas canvas;

        if (canvas = gameObject.GetComponent<Canvas>())
        {
            canvas.enabled = true;
        }
    }
}
