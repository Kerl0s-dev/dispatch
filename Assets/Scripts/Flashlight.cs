using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light flashlight;

    bool isOn = false;

    public float batteryLife = 100f;
    public float intensity = 5f;
    public float batteryDrainRate = 1f;

    bool hasBattery => batteryLife > 0f;

    void Start()
    {
        flashlight = GetComponent<Light>();
        flashlight.intensity = 0f;
    }

    void Update()
    {
        if (InputManager.Flashlight)
            isOn = !isOn;

        if (InputManager.Refill)
        {
            batteryLife = 100f;
            Debug.Log("Flashlight battery reffilled.");
        }

        if (!hasBattery)
        {
            isOn = false;
            Debug.Log("Flashlight battery dead.");
        }

        // Diminuer l’intensité max selon la batterie restante
        float currentMaxIntensity = intensity * Mathf.Clamp01(batteryLife / 100f);

        if (isOn)
        {
            flashlight.intensity = Mathf.Lerp(flashlight.intensity, currentMaxIntensity, Time.deltaTime * 5f);
            batteryLife -= Time.deltaTime * batteryDrainRate;
        }
        else
        {
            flashlight.intensity = Mathf.Lerp(flashlight.intensity, 0f, Time.deltaTime * 5f);
        }
    }
}