using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel; // ������ ���������
    public Image[] itemSlots; // ������ ��� ���������
    private bool isInventoryOpen = false;
    private int selectedItemIndex = -1;
    void Start()
    {
        inventoryPanel.SetActive(false); // ��������� ����� ��� ������
    }
    void Update()
    {
        // ��������/�������� ��������� �� ������� B
        if (Input.GetKeyDown(KeyCode.B))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.SetActive(isInventoryOpen);
        }

        // ������������ ��������� ��������� 1-5
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectItem(i);
            }
        }
    }
    void SelectItem(int index)
    {
        selectedItemIndex = index;
        Debug.Log("������ ������� � ����� " + (index + 1));

        // ����� �������� ���������� ��������� ��������� ��������
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].color = (i == index) ? Color.yellow : Color.white;
        }
    }
}

