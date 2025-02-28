using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Singleton

    public GameObject inventoryPanel; // Панель инвентаря
    public Image[] itemSlots; // Ячейки-инвентарь (5 кнопок)
    private InventoryItem[] inventory = new InventoryItem[5]; // Массив предметов
    private bool isInventoryOpen = false;
    private int selectedItemIndex = -1;

    void Awake() // Метод Awake() для Singleton
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    } // ← Закрывающая скобка для Awake()

    void Start()
    {
        inventoryPanel.SetActive(false);
    } // ← Закрывающая скобка для Start()

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.SetActive(isInventoryOpen);
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && inventory[i] != null)
            {
                SelectItem(i);
            }
        }
    } // ← Закрывающая скобка для Update()

    public bool AddItem(InventoryItem newItem)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = newItem;
                itemSlots[i].sprite = newItem.icon;
                itemSlots[i].enabled = true;
                return true;
            }
        }
        Debug.Log("Инвентарь полон!");
        return false;
    } // ← Закрывающая скобка для AddItem()

    void SelectItem(int index)
    {
        selectedItemIndex = index;
        Debug.Log("Выбран предмет: " + inventory[index].itemName);

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].color = (i == index) ? Color.yellow : Color.white;
        }
    } // ← Закрывающая скобка для SelectItem()

    public void UseItem()
    {
        if (selectedItemIndex != -1 && inventory[selectedItemIndex] != null)
        {
            Debug.Log("Использован предмет: " + inventory[selectedItemIndex].itemName);
            inventory[selectedItemIndex] = null;
            itemSlots[selectedItemIndex].sprite = null;
            itemSlots[selectedItemIndex].enabled = false;
            selectedItemIndex = -1;
        }
    } // ← Закрывающая скобка для UseItem()
} // ← Финальная закрывающая скобка для класса
