using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManagerScript : MonoBehaviour
{
    [Header("Windmills")]
    public GameObject[] objectsWithScripts;

    [Header("Ui")]
    public Selectable[] uiElementsToDisable;

    [Header("Colour")]
    public GameObject colourCanvas;
    public Color _goalColour;
    public Color[] _colorsArray;

    void Start()
    {
        foreach (GameObject obj in objectsWithScripts)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script == this) continue; //failsafe wenn Script is Self
                script.enabled = false;
            }
        }

        foreach (Selectable ui in uiElementsToDisable)
        {
            ui.interactable = false;
        }
    }

    public void ActivateEverything()
    {
        colourCanvas.SetActive(true);
        GameObject caller = EventSystem.current.currentSelectedGameObject;
        if (caller != null)
        {
            caller.SetActive(false);
        }

    }

    public void SelectColorGoal(int a)
    {
        foreach (GameObject obj in objectsWithScripts)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script == this) continue; //failsafe wenn Script is Self
                script.enabled = true;
            }
        }

        foreach (Selectable ui in uiElementsToDisable)
        {
            ui.interactable = true;
        }

        colourCanvas.SetActive(false);

        _goalColour = _colorsArray[a];

    }
    void Update()
    {
        
    }
}
