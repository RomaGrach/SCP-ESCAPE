using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem itemData; // ������ �� ������ ��������

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"������ {other.name} ����� � ������� {gameObject.name}");

        if (other.CompareTag("Player")) // ���������, ��� �� �����
        {
            Debug.Log("����� �������� ��������!");

            bool added = InventoryManager.instance.AddItem(itemData);
            if (added)
            {
                Debug.Log("������� �������� � ���������!");
                Destroy(gameObject); // ������� ������� ����� �������
            }
            else
            {
                Debug.Log("��������� �����, ������� �� ��������.");
            }
        }
    }
}
