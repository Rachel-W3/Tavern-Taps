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

    public void refresh()
    {
        foreach(KeyValuePair<Dish, int> dish in TavernManager.Instance.Dishes)
        {
            //This is rushed and bad, It will need to be changed
            if (dish.Value > 0)
            {
                if (dish.Key.Name == "Dire bacon and eggs")
                {
                    ShowObject(bacon_n_eggs);
                    bacon_n_eggs.GetComponentInChildren<Text>().text = dish.Value.ToString();
                }

                else if (dish.Key.Name == "Mandrake Stirfry")
                {
                    ShowObject(mandrake_stirfry);
                    mandrake_stirfry.GetComponentInChildren<Text>().text = dish.Value.ToString();
                }
            }

            if (dish.Value <= 0)
            { 
                if (dish.Key.Name == "Dire bacon and eggs")
                    HideObject(bacon_n_eggs);

                if (dish.Key.Name == "Mandrake Stirfry")
                    HideObject(mandrake_stirfry);
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
