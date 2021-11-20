using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements; 

public class MainMenu : MonoBehaviour
{
    private enum GAME_STATE
    {
        FARM, TAVERN, MAP
    }

    private GAME_STATE gameState;
    [SerializeField] private GameObject FarmUI;
    [SerializeField] private GameObject TavernUI;
    [SerializeField] private GameObject MapUI; 

    [SerializeField] private GameObject IngredientOverlay;
    [SerializeField] private GameObject RecipeOverlay;

    [SerializeField] private UnityEngine.UI.Button recipeButton;

    

    void Start()
    { 
        var root = GetComponent<UIDocument>().rootVisualElement; 

        ChangeGameState(GAME_STATE.TAVERN);

        var FarmButton = root.Q<UnityEngine.UIElements.Button>("FarmButton");
        FarmButton.clicked += () => ChangeGameState(GAME_STATE.FARM);

        var TavernButton = root.Q<UnityEngine.UIElements.Button>("TavernButton");
        TavernButton.clicked += () => ChangeGameState(GAME_STATE.TAVERN);

        var MapButton = root.Q<UnityEngine.UIElements.Button>("MapButton");
        MapButton.clicked += () => ChangeGameState(GAME_STATE.MAP);

        var IngredientButton = root.Q<UnityEngine.UIElements.Button>("IngredientButton");
        IngredientButton.clicked += () => showIngredientOverlay();

        recipeButton.onClick.AddListener(showRecipeOverlay);

    }

    private void ChangeGameState(GAME_STATE state)
    {

        hideAllOverlays();

        switch(state)
        {
            
            case GAME_STATE.FARM:
                if (gameState != GAME_STATE.FARM)
                { 
                    ShowObject(FarmUI);
                    HideObject(MapUI);
                    HideObject(TavernUI);
                    gameState = GAME_STATE.FARM;
                }
                break;

            case GAME_STATE.TAVERN:
                if (gameState != GAME_STATE.TAVERN)
                { 
                    ShowObject(TavernUI);
                    HideObject(MapUI);
                    HideObject(FarmUI);
                    gameState = GAME_STATE.TAVERN;
                }
                break;

            case GAME_STATE.MAP:
                if (gameState != GAME_STATE.MAP)
                {
                    ShowObject(MapUI);
                    HideObject(FarmUI);
                    HideObject(TavernUI);
                    gameState = GAME_STATE.MAP;
                }
                break;

            default:
                break;
        }
    }


    private void HideObject(GameObject gameObject)
    {
        Canvas canvas;
        UIDocument uiDoc;
        SpriteRenderer sprite;

        if ( canvas = gameObject.GetComponent<Canvas>() )
        {
            canvas.enabled = false;
        }

        if ( uiDoc = gameObject.GetComponent<UIDocument>() )
        {
            uiDoc.enabled = false;
        }

        if ( sprite = gameObject.GetComponent<SpriteRenderer>() )
        { 
            sprite.enabled = false;
        }

    }

    private void hideIngredientOverlay()
    {
        HideObject(IngredientOverlay);
    }

    private void hideRecipeOverlay()
    {
        HideObject(RecipeOverlay);
    }

    private void hideAllOverlays()
    {
        hideIngredientOverlay();
        hideRecipeOverlay();
    }

    private void ShowObject(GameObject gameObject)
    {
        Canvas canvas;
        UIDocument uiDoc;
        SpriteRenderer sprite;

        if ( canvas = gameObject.GetComponent<Canvas>() )
        {
            canvas.enabled = true;
        }

        if ( uiDoc = gameObject.GetComponent<UIDocument>() )
        {
            uiDoc.enabled = true;
        }

        if (sprite = gameObject.GetComponent<SpriteRenderer>())
        {
            sprite.enabled = true;
        }
    }

    private void showIngredientOverlay()
    {
        hideAllOverlays();
        ShowObject(IngredientOverlay);
        IngredientMenu ingredientMenu = IngredientOverlay.GetComponent<IngredientMenu>();
        ingredientMenu.refreshUI();
    }

    private void showRecipeOverlay()
    {
        hideAllOverlays();
        ShowObject(RecipeOverlay);
        RecipeMenu recipeMenu = RecipeOverlay.GetComponent<RecipeMenu>();
        recipeMenu.refreshUI();
    }

    public static void updateMoneyUI(int amt)
    {
        var root = GameObject.Find("NavigationMenu").GetComponent<UIDocument>().rootVisualElement;
        var moneyLabel = root.Q<Label>("MoneyLabel");
        moneyLabel.text = "Money: " + amt;
    }
}
