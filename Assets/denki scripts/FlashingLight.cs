using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    public Light exitLight; // Присвой в инспекторе
    public float blinkInterval = 0.5f; // Интервал мигания

    private void Start()
    {
        InvokeRepeating(nameof(ToggleLight), blinkInterval, blinkInterval);
    }

    private void ToggleLight()
    {
        exitLight.enabled = !exitLight.enabled;
    }
}

