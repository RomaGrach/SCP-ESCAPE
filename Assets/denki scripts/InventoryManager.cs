using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Singleton

    public GameObject inventoryPanel; // Панель инвентаря
    public Image[] itemSlots; // Ячейки-инвентаря
    private InventoryItem[] inventory = new InventoryItem[10]; // 10 предметов
    private bool isInventoryOpen = false;
    private int selectedItemIndex = -1;

    public Transform dropPoint; // Точка выброса предмета (назначь в Unity)
    public GameObject itemPrefab; // Префаб выбрасываемого предмета

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.SetActive(isInventoryOpen);
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if ((i < 9 && Input.GetKeyDown(KeyCode.Alpha1 + i)) || (i == 9 && Input.GetKeyDown(KeyCode.Alpha0)))
            {
                if (inventory[i] != null)
                {
                    SelectItem(i);
                }
            }
        }

        // Выбросить предмет при нажатии Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
    }

    public bool AddItem(InventoryItem newItem)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = newItem;
                itemSlots[i].sprite = newItem.icon;
                itemSlots[i].enabled = true;

                Debug.Log($"Предмет {newItem.itemName} добавлен в слот {i}.");
                return true;
            }
        }
        Debug.Log("Инвентарь полон!");
        return false;
    }

    void SelectItem(int index)
    {
        selectedItemIndex = index;
        Debug.Log("Выбран предмет: " + inventory[index].itemName);

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].color = (i == index) ? Color.yellow : Color.white;
        }
    }
    public bool HasItem(string itemName)
    {
        foreach (var item in inventory)
        {
            if (item != null && item.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }

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
    }

    public void DropItem()
    {
        if (selectedItemIndex == -1 || inventory[selectedItemIndex] == null)
        {
            Debug.Log("Нет выбранного предмета для выброса!");
            return;
        }

        Debug.Log("Выброшен предмет: " + inventory[selectedItemIndex].itemName);

        // Определяем позицию выброса перед игроком
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 dropPosition = playerTransform.position + playerTransform.forward * 1.5f; // 1.5 метра впереди

        // Создаём выброшенный предмет перед игроком
        GameObject droppedItem = Instantiate(itemPrefab, dropPosition, Quaternion.identity);
        droppedItem.GetComponent<ItemPickup>().itemData = inventory[selectedItemIndex];

        // Очищаем слот в инвентаре
        inventory[selectedItemIndex] = null;
        itemSlots[selectedItemIndex].sprite = null;
        itemSlots[selectedItemIndex].enabled = false;

        selectedItemIndex = -1;
    }

}
