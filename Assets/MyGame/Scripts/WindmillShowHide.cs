using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    public void ShowAll()
    {
        foreach (var windmill in allWindmills)
        {
            windmill.gameObject.SetActive(true);
        }
    }

}
