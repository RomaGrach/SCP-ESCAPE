using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkingSystem : MonoBehaviour
{
    public enum BlinkType
    {
        Fade,   // ������� ����������/����������
        Flash   // ���������� ������������
    }

    [Header("��������� ��������")]
    public bool blinkingEnabled = true; // ���������� ���������/���������� ��������
    public BlinkType blinkType = BlinkType.Fade; // ����� ������� ��������
    public Image blinkImage; // ���������� Image � ����������
    public float blinkInterval = 5f; // �������� ����� ����������
    public float blinkDuration = 0.2f; // ������������ �������� ����������/���������� (��� Fade)
    public float flashDuration = 0.05f; // ������������ "�������" (��� Flash)

    [Header("�������������� ��������� ��� SCP-173")]
    public float blackPeakDelay = 0.2f; // �������� � ��� ������� ������ (gap ��� ����������� SCP-173)

    public static bool isBlinking = false; // ���� ��������, ��������� �� ������ ��������

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
            // ���������� ������
            for (float t = 0; t < blinkDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(0, 1, t / blinkDuration);
                blinkImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }

            // �������� �� ���� ������� ������ (gap ��� ����������� SCP-173)
            yield return new WaitForSeconds(blackPeakDelay);

            // ���������� ������
            for (float t = 0; t < blinkDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1, 0, t / blinkDuration);
                blinkImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }
        else if (blinkType == BlinkType.Flash)
        {
            // ���������� ��������
            blinkImage.color = new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(flashDuration);
            blinkImage.color = new Color(0, 0, 0, 0);
        }

        isBlinking = false;
    }
}
