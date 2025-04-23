using UnityEngine;
using UnityEngine.UI;

public class Windmill : MonoBehaviour
{
    private enum WindmillColors { RED, GREEN, BLUE };

    [SerializeField] private WindmillColors color;
    [SerializeField] private RotorHub rotor;
    [SerializeField] private Light lampLight; 
    [SerializeField] private Slider speedSlider;

    [SerializeField] private bool isWindmillSelected = false;
    private const float MAX_LIGHT_INTENSITY = 1f; // Maximum lamp brightness

    private void Start()
    {
        if (!lampLight || !rotor || !speedSlider)
        {
            Debug.LogWarning("Windmill: Nicht alle Referenzen sind gesetzt.");
            return;
        }

        ToggleLamp();
        SetLampColor(color);
    }

    private void Update()
    {
        UpdateUI();
        UpdateLightIntensity();
        rotor.RotateRotor(isWindmillSelected);
    }


    public void ToggleRotationMode()
    {
        isWindmillSelected = false;
        rotor.constRotationSpeed = rotor.currentSpeed;
    }

    public int GetCurrentSpeed()
    {
        return rotor.GetCurrentSpeed();
    }

    private void ToggleLamp()
    {
        if (lampLight != null)
        {
            lampLight.enabled = !lampLight.enabled;
        }
    }

    private void UpdateLightIntensity()
    {
        // Control light intensity based on speed
        if (lampLight != null)
        {
            lampLight.intensity = Mathf.Lerp(0f, MAX_LIGHT_INTENSITY, speedSlider.value / 255f);
        }
    }

    private void SetLampColor(WindmillColors windmillColor)
    {
        switch (windmillColor)
        {
            case WindmillColors.RED:
                lampLight.color = Color.red;
                break;
            case WindmillColors.GREEN:
                lampLight.color = Color.green;
                break;
            case WindmillColors.BLUE:
                lampLight.color = Color.blue;
                break;
        }
    }

    public void SelectWindmill()
    {
        isWindmillSelected = true;

        if (!lampLight.isActiveAndEnabled)
        {
            lampLight.enabled = true;
        }
    }

    public void ResetWindmill()
    {
        rotor.constRotationSpeed = -1;
        isWindmillSelected = false;
        rotor.currentSpeed = 0;
        speedSlider.value = 0;
        ToggleLamp();
    }

    public bool IsWindmillLocked()
    {
        return !isWindmillSelected && rotor.constRotationSpeed != -1;
    }

    private void UpdateUI()
    {
        if (isWindmillSelected && speedSlider != null)
        {
            speedSlider.value = Mathf.Round(rotor.currentSpeed);
        }
    }
}
