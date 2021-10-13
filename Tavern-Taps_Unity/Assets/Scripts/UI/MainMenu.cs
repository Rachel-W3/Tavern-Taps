using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private enum GAME_STATE
    {
        FARM, TAVERN, MAP
    }

    [SerializeField] private GameObject FarmUI;
    [SerializeField] private GameObject TavernUI;
    [SerializeField] private GameObject MapUI;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var FarmButton = root.Q<Button>("FarmButton");
        FarmButton.clicked += () => ChangeGameState(GAME_STATE.FARM);

        var TavernButton = root.Q<Button>("TavernButton");
        TavernButton.clicked += () => ChangeGameState(GAME_STATE.TAVERN);

        var MapButton = root.Q<Button>("MapButton");
        MapButton.clicked += () => ChangeGameState(GAME_STATE.MAP);

    }

    private void ChangeGameState(GAME_STATE state)
    {
        switch(state)
        {
            
            case GAME_STATE.FARM:
                ShowObject(FarmUI);
                HideObject(MapUI);
                HideObject(TavernUI);
                break;

            case GAME_STATE.TAVERN:
                ShowObject(TavernUI);
                HideObject(MapUI);
                HideObject(FarmUI);
                break;

            case GAME_STATE.MAP:
                ShowObject(MapUI);
                HideObject(FarmUI);
                HideObject(TavernUI);
                break;

            default:
                break;
        }

        Debug.Log(state);
    }

    private void HideObject(GameObject gameObject)
    {
        Renderer rend = gameObject.GetComponent<Renderer>();

        rend.enabled = false;        
    }

    private void ShowObject(GameObject gameObject)
    {
        Renderer rend = gameObject.GetComponent<Renderer>();

        rend.enabled = true;
    }
}
