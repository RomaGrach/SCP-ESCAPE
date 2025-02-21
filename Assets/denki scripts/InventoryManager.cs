using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel; // Панель инвентаря
    public Image[] itemSlots; // Ячейки для предметов
    private bool isInventoryOpen = false;
    private int selectedItemIndex = -1;
    void Start()
    {
        inventoryPanel.SetActive(false); // Инвентарь скрыт при старте
    }
    void Update()
    {
        // Открытие/закрытие инвентаря на клавишу B
        if (Input.GetKeyDown(KeyCode.B))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.SetActive(isInventoryOpen);
        }

        // Переключение предметов клавишами 1-5
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
        Debug.Log("Выбран предмет в слоте " + (index + 1));

        // Можно добавить визуальное выделение активного предмета
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].color = (i == index) ? Color.yellow : Color.white;
        }
    }
}

