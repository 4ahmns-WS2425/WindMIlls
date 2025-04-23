using UnityEngine;
using UnityEngine.UI;

public class WindmillManager : MonoBehaviour
{
    [SerializeField] private Windmill[] windmills;
    [SerializeField] private GameObject wall;
    [SerializeField] private Button reset;

    private int index = 0;
    private Material wallMaterial;
    private Color32 windmillColor = new Color32(0, 0, 0, 255);


    private void Start()
    {
        if (windmills.Length == 0 || wall == null)
        {
            Debug.LogError("WindmillManager: Keine Windmühlen oder Farbwand zugewiesen!");
            return;
        }
        reset.gameObject.SetActive(false);
        windmills[index].SelectWindmill();
        wallMaterial = wall.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        UpdateWallColor();
        TrySelectNextWindmill();
    }


    public void ResetScene()
    {
        index = 0;
        windmillColor = new Color32(0, 0, 0, 255);

        for (int i = 0; i < windmills.Length; i++)
        {
            windmills[i].ResetWindmill();
        }

        windmills[index].SelectWindmill();
    }

    private void UpdateWallColor()
    {
        CombineLightSpeed();

        if (wallMaterial != null)
        {
            wallMaterial.color = windmillColor;
        }
    }

    private void TrySelectNextWindmill()
    {
        if (index >= windmills.Length)
        {
            reset.gameObject.SetActive(true);

            return; //Early Return/Guard Clause
        }

        reset.gameObject.SetActive(false);

        if (windmills[index].IsWindmillLocked())
        {
            index++;

            if (index < windmills.Length)
            {
                windmills[index].SelectWindmill();
            }
        }
    }

    private void CombineLightSpeed()
    {
        switch (index)
        {
            case 0:
                windmillColor.r = (byte)windmills[index].GetCurrentSpeed();
                break;
            case 1:
                windmillColor.g = (byte)windmills[index].GetCurrentSpeed();
                break;
            case 2:
                windmillColor.b = (byte)windmills[index].GetCurrentSpeed();
                break;
        }
    }
}
