using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillShowHide : MonoBehaviour
{
    public List<Windmill> allWindmills;

    public void ShowOnly(Windmill selected)
    {
        foreach (var windmill in allWindmills)
        {
            bool isSelected = windmill == selected;
            windmill.gameObject.SetActive(isSelected);
        }
    }

    // Optional: Methode zum Registrieren, falls du dynamisch Windmühlen spawnst
    public void Register(Windmill windmill)
    {
        if (!allWindmills.Contains(windmill))
            allWindmills.Add(windmill);
    }

}
