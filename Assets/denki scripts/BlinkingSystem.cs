using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class BlinkingSystem : MonoBehaviour
{
    public Image blinkImage; // Перетащи Image в инспекторе
    public float blinkInterval = 5f; // Интервал моргания
    public float blinkDuration = 0.2f; // Длительность моргания

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkInterval)
        {
            StartCoroutine(Blink());
            timer = 0f; // Сброс таймера
        }
    }

    private IEnumerator Blink()
    {
        // Затемнение
        for (float t = 0; t < blinkDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / blinkDuration);
            blinkImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f); // Задержка с закрытыми глазами

        // Осветление
        for (float t = 0; t < blinkDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / blinkDuration);
            blinkImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
