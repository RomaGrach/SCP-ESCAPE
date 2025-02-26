using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkingSystem : MonoBehaviour
{
    public enum BlinkType
    {
        Fade,   // Плавное затемнение/осветление
        Flash   // Мгновенное переключение
    }

    [Header("Настройки моргания")]
    public bool blinkingEnabled = true; // Глобальное включение/выключение моргания
    public BlinkType blinkType = BlinkType.Fade; // Выбор способа моргания
    public Image blinkImage; // Перетащите Image в инспекторе
    public float blinkInterval = 5f; // Интервал между морганиями
    public float blinkDuration = 0.2f; // Длительность анимации затемнения/осветления (для Fade)
    public float flashDuration = 0.05f; // Длительность "вспышки" (для Flash)

    [Header("Дополнительные настройки для SCP-173")]
    public float blackPeakDelay = 0.2f; // Задержка в пик черного экрана (gap для перемещения SCP-173)

    public static bool isBlinking = false; // Флаг моргания, доступный из других скриптов

    private float timer;

    void Update()
    {
        if (!blinkingEnabled)
            return;

        timer += Time.deltaTime;
        if (timer >= blinkInterval && !isBlinking)
        {
            StartCoroutine(Blink());
            timer = 0f;
        }
    }

    private IEnumerator Blink()
    {
        isBlinking = true;

        if (blinkType == BlinkType.Fade)
        {
            // Затемнение экрана
            for (float t = 0; t < blinkDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(0, 1, t / blinkDuration);
                blinkImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }

            // Задержка на пике черного экрана (gap для перемещения SCP-173)
            yield return new WaitForSeconds(blackPeakDelay);

            // Осветление экрана
            for (float t = 0; t < blinkDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1, 0, t / blinkDuration);
                blinkImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }
        else if (blinkType == BlinkType.Flash)
        {
            // Мгновенное моргание
            blinkImage.color = new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(flashDuration);
            blinkImage.color = new Color(0, 0, 0, 0);
        }

        isBlinking = false;
    }
}
