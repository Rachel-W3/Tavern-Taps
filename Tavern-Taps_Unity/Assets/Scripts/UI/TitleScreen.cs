using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var titleScreen = root.Q<Button>("titleScreen");
        titleScreen.clicked += disableTitleScreen;

        MainMenu.disableIngredientPlots();
        MainMenu.disableBars();
    }

    void disableTitleScreen()
    {
        GetComponent<UIDocument>().enabled = false;
        MainMenu.enableIngredientPlots();
        MainMenu.enableBars();
    }
}
