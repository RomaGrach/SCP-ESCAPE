using UnityEngine;
using UnityEngine.SceneManagement; // ��� ����������� ����� ��� ������ ������ ����� ����

public class SCP409Pickup : MonoBehaviour
{
    public InventoryItem scp409Item; // ������ � �������� SCP-409
    public GameObject gameOverPanel; // ������ ����� ���� (��������� � ����������)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventoryManager.instance.HasItem("��������")) // ��������� ������� ��������
            {
                bool added = InventoryManager.instance.AddItem(scp409Item);
                if (added)
                {
                    Debug.Log("����� �������� SCP-409 ���������.");
                    Destroy(gameObject); // ������� SCP-409 ����� �������
                }
                else
                {
                    Debug.Log("��������� �����, SCP-409 �� ��������.");
                }
            }
            else
            {
                Debug.Log("����� �������� SCP-409 ��� ������! ����� ����.");
                gameOverPanel.SetActive(true); // �������� ������ ����� ����
                Time.timeScale = 0; // ������������� ����
            }
        }
    }
}
