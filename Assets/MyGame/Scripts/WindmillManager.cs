using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WindmillManager : MonoBehaviour
{
    [SerializeField] Windmill[] windmills;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject _wallGoal;
    [SerializeField] Button reset;
    [SerializeField] GameObject endGameButton;

    private ColorGoalScript _cgsa;
    public Color32 windmillColor = new Color32(0, 0, 0, 255);

    private Windmill currentSelectedWindmill;
    private bool allWindmillsLocked = false;

    private void Start()
    {
        _cgsa = GameObject.FindObjectOfType<ColorGoalScript>();
        _wallGoal.GetComponent<Renderer>().material.color = _cgsa._goalColor;

        if (windmills.Length == 0 || wall == null)
        {
            Debug.LogError("WindmillManager: Keine Windmühlen oder Farbwand zugewiesen!");
            return;
        }

        reset.gameObject.SetActive(false);
        currentSelectedWindmill = windmills[0];
        currentSelectedWindmill.SelectWindmill();

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        UpdateWallColor();
        CheckIfAllLocked();
    }

    public void ResetScene()
    {
        windmillColor = new Color32(0, 0, 0, 255);

        foreach (var windmill in windmills)
        {
            windmill.ResetWindmill();
        }

        currentSelectedWindmill = windmills[0];
        currentSelectedWindmill.SelectWindmill();
    }

    private void UpdateWallColor()
    {
        CombineLightSpeed();

        if (wall != null)
        {
            wall.GetComponent<Renderer>().material.color = windmillColor;
        }
    }

    private void CombineLightSpeed()
    {
        if (windmills.Length > 0)
            windmillColor.r = (byte)windmills[0].GetCurrentSpeed();
        if (windmills.Length > 1)
            windmillColor.g = (byte)windmills[1].GetCurrentSpeed();
        if (windmills.Length > 2)
            windmillColor.b = (byte)windmills[2].GetCurrentSpeed();
    }
    public void LockAllExcept(Windmill clickedWindmill)
    {
        if (clickedWindmill == currentSelectedWindmill)
        {
            //Sperret die Mill wenn nochmal geclicked
            clickedWindmill.ToggleRotationMode();
            clickedWindmill.isWindmillSelected = false;
            currentSelectedWindmill = null;
        }
        else
        {
            //Sperrt alle außer this
            foreach (var windmill in windmills)
            {
                if (windmill == clickedWindmill)
                {
                    windmill.isWindmillSelected = true;
                    windmill.rotor.constRotationSpeed = -1f;
                    windmill.SelectWindmill();
                    currentSelectedWindmill = windmill;
                }
                else
                {
                    windmill.isWindmillSelected = false;
                    windmill.rotor.constRotationSpeed = windmill.rotor.currentSpeed;
                }
            }
        }
    }
    private void CheckIfAllLocked()
    {
        if (allWindmillsLocked)
            return;

        bool allLocked = true;
        foreach (var windmill in windmills)
        {
            if (!windmill.IsWindmillLocked())
            {
                allLocked = false;
                break;
            }
        }
        if (allLocked)
        {
            allWindmillsLocked = true;
            reset.gameObject.SetActive(true);
            endGameButton.SetActive(true);
        }
    }
    public void LoadEndScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
