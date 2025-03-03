using UnityEngine;
using UnityEngine.UI;

public class SCP012Effect : MonoBehaviour
{
    public float pullForce = 2f; // ���� ����������
    public AudioSource scpMusic; // ������ SCP-012
    public GameObject gameOverPanel; // ������ "����� ����"

    private Transform player;
    private bool isPlayerNear = false;

    void Start()
    {
        gameOverPanel.SetActive(false); // �������� ������ ��� ������
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isPlayerNear)
        {
            // ������������ ������ � SCP-012
            Vector3 direction = (transform.position - player.position).normalized;
            player.position += direction * pullForce * Time.deltaTime;

            // ��������� ��������� ������ � ����������� �� ����������
            float distance = Vector3.Distance(player.position, transform.position);
            scpMusic.volume = Mathf.Clamp01(1f - (distance / 10f)); // ��� �����, ��� ������
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            scpMusic.Play(); // �������� ������
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            scpMusic.Stop(); // ������������� ������, ���� ����� �����
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameOverPanel.SetActive(true); // ���������� ������ "����� ����"
            Time.timeScale = 0f; // ������������� �����
        }
    }
}

